using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor.UIElements;
using UnityEngine;


/// <summary>
/// Class for SaveController
/// </summary>
public class SaveController : MonoBehaviour
{
    #region Fields

    /// <summary>
    /// Field for save display
    /// </summary>
    private GameObject saveDisplay;

    #endregion Fields

    #region Properties

    /// <summary>
    /// Gets or sets the instance of the save controller
    /// </summary>
    public static SaveController Instance = null;

    /// <summary>
    /// Gets or sets the save display
    /// </summary>
    [Header("Display")]
    public GameObject SaveDisplay;

    /// <summary>
    /// Gets or sets the tag of the UI container
    /// </summary>
    [TagSelector]
    public string UIContainerTag;

    /// <summary>
    /// Gets or sets the save state of the game
    /// </summary>
    [HideInInspector]
    public SaveState SaveState;

    /// <summary>
    /// Gets a value indicating whether the save controller is performing a save
    /// </summary>
    public bool IsSaving { get; internal set; }

    /// <summary>
    /// Gets or sets the event handler for save completion
    /// </summary>
    public event EventHandler SaveCompleted;

    #endregion Properties

    #region Constructors

    #endregion Constructors

    #region Functions

    /// <summary>
    /// Awake fires when object is instanciated
    /// </summary>
    private void Awake()
    {
        #region Persistance Pattern

        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        #endregion Persistance Pattern

        this.Load();
    }

    /// <summary>
    /// Launches the event of save complete
    /// </summary>
    protected virtual void CompleteSave()
    {
        SaveCompleted?.Invoke(this, null);
    }

    #endregion Functions

    #region Methods

    /// <summary>
    /// Performs the save of the file
    /// </summary>
    public void Save()
    {
        if (!this.IsSaving)
        {
            this.IsSaving = true;

            this.saveDisplay = Instantiate<GameObject>(SaveDisplay, Vector3.zero, Quaternion.identity);
            this.saveDisplay.transform.SetParent(GameObject.FindGameObjectWithTag(UIContainerTag).transform, false);

            using (FileStream fileStream = new FileStream(string.Format("{0}{1}", Application.persistentDataPath, "/Save.dat"), FileMode.OpenOrCreate))
            {
                try
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(fileStream, SaveState);
                    this.SaveState.Information.LastSaveTime = DateTime.Now;
                }
                catch (SerializationException exception)
                {
                    Debug.LogError(string.Format("There was an issue while serializing during save: {0}", exception.Message));
                }
                finally
                {
                    fileStream.Close();
                    Destroy(saveDisplay);
                    this.IsSaving = false;
                    this.CompleteSave();
                }
            }
        }
    }

    /// <summary>
    /// Performs the load of the file
    /// </summary>
    /// <returns>Persisted information</returns>
    public void Load()
    {
        try
        {
            using (FileStream fileStream = new FileStream(string.Format("{0}{1}", Application.persistentDataPath, "/Save.dat"), FileMode.Open))
            {
                try
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    SaveState = formatter.Deserialize(fileStream) as SaveState;
                }
                catch (SerializationException exception)
                {
                    Debug.LogError(string.Format("There was an issue while deserializing during save: {0}", exception.Message));
                }
                finally
                {
                    fileStream.Close();
                }
            }
        }
        catch (FileNotFoundException)
        {
            SaveState = new SaveState();
        }
    }

    #endregion Methods 
}
