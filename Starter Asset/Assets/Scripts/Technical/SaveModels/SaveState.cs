using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class for SaveState
/// </summary>
[Serializable]
public class SaveState
{
    #region Properties

    /// <summary>
    /// Gets or sets the options
    /// </summary>
    public OptionsModel Options { get; set; }

    /// <summary>
    /// Gets or sets the information of the save
    /// </summary>
    public SaveInformationModel Information { get; set; }

    #endregion Properties

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="SaveState"/> class
    /// </summary>
    public SaveState()
    {
        this.Options = new OptionsModel();
        this.Information = new SaveInformationModel();
    }

    #endregion Constructors
}
