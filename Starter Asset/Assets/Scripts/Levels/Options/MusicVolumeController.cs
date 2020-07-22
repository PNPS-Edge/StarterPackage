using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class for MainVolumeController
/// </summary>
public class MusicVolumeController : MonoBehaviour
{
    #region Functions

    private void OnEnable()
    {
        Slider me = GetComponent<Slider>();
        me.value = SaveController.Instance.SaveState.Options.MusicVolume;
    }

    #endregion Functions
}
