using UnityEngine;

/// <summary>
/// Class for GameController
/// </summary>
public class GameController : MonoBehaviour
{
    #region Properties

    /// <summary>
    /// Gets or sets the instance of the game controller
    /// </summary>
    public static GameController Instance = null;

    [Header("Game Objects")]

    /// <summary>
    /// Gets or sets the save display
    /// </summary>
    public GameObject SaveDisplay;

    [Header("Current Information")]
    /// <summary>
    /// Gets or sets the current scene
    /// </summary>
    public ScenesEnum CurrentScene;

    /// <summary>
    /// Gets or sets a value indicating whether the game is in pause
    /// </summary>
    public bool InPause;

    /// <summary>
    /// Gets or sets a value indicating whether the game is muted
    /// </summary>
    public bool IsMuted;

    /// <summary>
    /// Gets or sets a value indicating whether the game is saving
    /// </summary>
    public bool IsSaving;

    /// <summary>
    /// Gets or sets the current input mode
    /// </summary>
    public InputModeEnum CurrentInputMode;

    #endregion Properties

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

        this.CurrentInputMode = InputModeEnum.Keyboard;
    }

    #endregion Functions

    #region Methods

    /// <summary>
    /// Performs a game is over
    /// </summary>
    public void GameOver()
    {
        enabled = false;
    }

    /// <summary>
    /// Updates the current input mode if necessary
    /// </summary>
    /// <param name="mode">New Input Mode</param>
    public void UpdateInputMode(InputModeEnum mode)
    {
        if (this.CurrentInputMode != mode)
        {
            this.CurrentInputMode = mode;
        }
    }

    #endregion Methods 
}
