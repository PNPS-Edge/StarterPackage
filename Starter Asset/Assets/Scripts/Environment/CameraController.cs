using System.Collections;
using UnityEngine;

/// <summary>
/// Class for CameraController
/// </summary>
public class CameraController : MonoBehaviour
{
    #region Fields

    /// <summary>
    /// Field for offset between Game Object and Camera
    /// </summary>
    private Vector3 offset;

    #endregion Fields

    #region Properties

    /// <summary>
    /// Gets or sets the game object to follow
    /// </summary>
    public Transform GameObjectToFollow;

    /// <summary>
    /// Gets or sets a value indicating whether X axis is locked
    /// </summary>
    public bool LockX;

    /// <summary>
    /// Gets or sets a value indicating whether Y axis is locked
    /// </summary>
    public bool LockY;

    /// <summary>
    /// Gets or sets a value indicating whether Z axis is locked
    /// </summary>
    public bool LockZ;

    #endregion Properties

    #region Functions

    /// <summary>
    /// Start fires on the first frame
    /// </summary>
    private void Start()
    {
        if (this.GameObjectToFollow != null)
        {
            // Determine the offset between object to follow and the camera
            offset = new Vector3
                (
                    LockX ? 0.0f : this.transform.position.x - this.GameObjectToFollow.position.x,
                    LockY ? 0.0f : this.transform.position.y - this.GameObjectToFollow.position.y,
                    LockZ ? 0.0f : this.transform.position.z - this.GameObjectToFollow.position.z
                );
        }
    }

    /// <summary>
    /// Update fires at each frame
    /// Use for visual purpose
    /// </summary>
    private void Update()
    {
        if (this.GameObjectToFollow != null)
        {
            // Determines the position of the camera
            transform.position = new Vector3
            (
                LockX ? this.transform.position.x + this.offset.x : this.GameObjectToFollow.position.x + this.offset.x,
                LockY ? this.transform.position.y + this.offset.x : this.GameObjectToFollow.position.y + this.offset.y,
                LockZ ? this.transform.position.z + this.offset.z : this.GameObjectToFollow.position.z + this.offset.z
            );
        }
    }

    #endregion Functions

    #region Methods

    /// <summary>
    /// Performs a camera shake
    /// </summary>
    /// <param name="duration">Duration of the shake</param>
    /// <param name="magnitude">Force of the shake</param>
    /// <returns>Nothing in a coroutine</returns>
    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPosition = transform.localPosition;

        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(2.5f, 2.7f) * magnitude;
            float y = Random.Range(2.55f, 2.65f) * magnitude;

            transform.localPosition = new Vector3(x, y, originalPosition.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPosition;
    }

    #endregion Methods 
}
