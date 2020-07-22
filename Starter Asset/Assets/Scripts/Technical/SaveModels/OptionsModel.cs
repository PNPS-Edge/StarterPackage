using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class OptionsModel
{
    /// <summary>
    /// Gets or sets the music volume
    /// </summary>
    public float MusicVolume { get; set; }

    /// <summary>
    /// Gets or sets the SFX volume
    /// </summary>
    public float SFXVolume { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="OptionsModel"/> class
    /// </summary>
    public OptionsModel()
    {
        this.MusicVolume = 1;
        this.SFXVolume = 1;
    }
}
