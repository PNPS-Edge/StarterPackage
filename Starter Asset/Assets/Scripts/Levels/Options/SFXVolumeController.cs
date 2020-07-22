using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class for SFXVolumeController
/// </summary>
public class SFXVolumeController : MonoBehaviour
{
    #region Functions

    private void OnEnable()
    {
        Slider me = GetComponent<Slider>();
        me.value = SaveController.Instance.SaveState.Options.SFXVolume;
    }

    #endregion Functions
}