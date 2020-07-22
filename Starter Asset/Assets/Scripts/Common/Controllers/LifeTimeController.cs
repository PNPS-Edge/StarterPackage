using UnityEngine;

/// <summary>
/// Class for LifeTimeController
/// </summary>
public class LifeTimeController : MonoBehaviour
{
    #region Fields

    /// <summary>
    /// Field for elapsed time
    /// </summary>
    private float elapsed;

    #endregion Fields

    #region Properties

    /// <summary>
    /// Gets or sets the life time
    /// </summary>
    public float LifeTime;

    #endregion Properties

    #region Functions

    /// <summary>
    /// Start fires on the first frame
    /// </summary>
    private void Start()
    {
        elapsed = 0.0f;
    }

    /// <summary>
    /// Update fires at each frame
    /// Use for visual purpose
    /// </summary>
    private void Update()
    {
        elapsed += Time.deltaTime;

        if (elapsed > LifeTime)
        {
            Destroy(gameObject);
        }
    }

    #endregion Functions
}
