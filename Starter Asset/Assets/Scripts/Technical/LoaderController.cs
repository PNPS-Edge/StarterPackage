using UnityEngine;

/// <summary>
/// Class for LoaderController
/// </summary>
public class LoaderController : MonoBehaviour
{
    #region Properties

    /// <summary>
    /// Gets or sets the game controller
    /// </summary>
    public GameObject gameController;

    /// <summary>
    /// Gets or sets the sound controller
    /// </summary>
    public GameObject soundController;

    #endregion Properties

    #region Functions

    /// <summary>
    /// Awake fires when object is instanciated
    /// </summary>
    private void Awake()
    {
        if (GameController.Instance == null)
        {
            Instantiate(gameController);
        }

        if (SoundController.Instance == null)
        {
            Instantiate(soundController);
        }
    }

    #endregion Functions
}
