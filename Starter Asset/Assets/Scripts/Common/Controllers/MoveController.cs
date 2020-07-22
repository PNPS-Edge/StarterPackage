using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class for MoveController
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class MoveController : MonoBehaviour
{
    #region Fields

    /// <summary>
    /// Field for the rigidbody
    /// </summary>
    private Rigidbody2D rigid;

    #endregion Fields

    #region Properties

    /// <summary>
    /// Gets or sets the movement direction
    /// </summary>
    public Vector2 MoveDirection;

    /// <summary>
    /// Gets or sets the movement speed
    /// </summary>
    public float MoveSpeed;

    #endregion Properties

    #region Functions

    /// <summary>
    /// Awake fires when object is instanciated
    /// </summary>
    private void Awake()
    {
        
    }

    /// <summary>
    /// Start fires on the first frame
    /// </summary>
    private void Start()
    {
        this.rigid = GetComponent<Rigidbody2D>();
        this.rigid.velocity = MoveDirection * MoveSpeed;
    }

    /// <summary>
    /// Update fires at each frame
    /// Use for visual purpose
    /// </summary>
    private void Update()
    {
    }

    #endregion Functions
}
