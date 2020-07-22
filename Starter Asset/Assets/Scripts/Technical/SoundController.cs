using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// Class for SoundController
/// </summary>
public class SoundController : MonoBehaviour
{
    #region Fields

    /// <summary>
    /// Field for the minimum volume value
    /// </summary>
    private float minimumVolumeValue = 0.0001f;

    /// <summary>
    /// Field for the maximum volume value
    /// </summary>
    private float maximumVolumeValue = 1f;

    /// <summary>
    /// Field for the previous volume level
    /// </summary>
    private float previousVolume;

    #endregion Fields

    #region Properties

    /// <summary>
    /// Gets or sets the main mixer
    /// </summary>
    public AudioMixer MainMixer;

    /// <summary>
    /// Gets or sets the sound effects source
    /// </summary>
    public AudioSource SfxSource;

    /// <summary>
    /// Gets or sets the music source
    /// </summary>
    public AudioSource MusicAudioSource;

    /// <summary>
    /// Gets or sets the instance of the sound controller
    /// </summary>
    public static SoundController Instance = null;

    /// <summary>
    /// Gets or sets the minimum value of pitch for sfx
    /// </summary>
    public float lowPitchRange = .95f;

    /// <summary>
    /// Gets or sets the maximum value of pitch for sfx
    /// </summary>
    public float highPitchRange = 1.05f;

    /// <summary>
    /// Gets or sets a value indicating whether the main mixer is in transition
    /// </summary>
    public bool IsSoundInTransition;

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
    }

    /// <summary>
    /// Fires when object start in scene
    /// </summary>
    private void Start()
    {
        InitializeMixers(SaveController.Instance.SaveState.Options.MusicVolume, SaveController.Instance.SaveState.Options.SFXVolume);
    }

    /// <summary>
    /// Update fires at each frame
    /// Use for visual purpose
    /// </summary>
    private void Update()
    {
        if (GameController.Instance.IsMuted)
        {
            MusicAudioSource.volume = 0f;
        }
        else
        {
            if (GameController.Instance.InPause)
            {
                MusicAudioSource.volume = 0.15f;
            }
            else
            {
                MusicAudioSource.volume = 0.3f;
            }
        }
    }

    /// <summary>
    /// Initializes all the mixers
    /// </summary>
    /// <param name="musicVolume">Volume of the music</param>
    /// <param name="sfxVolume">Volume of the sfx</param>
    private void InitializeMixers(float musicVolume, float sfxVolume)
    {
        MainMixer.SetFloat("Music", Mathf.Log10(musicVolume) * 20);
        MainMixer.SetFloat("SFX", Mathf.Log10(sfxVolume) * 20);
    }

    /// <summary>
    /// Performs the fade of the sound on audio mixer
    /// </summary>
    /// <param name="mixer">Name of the mixer to applied</param>
    /// <param name="duration">Duration of the fade</param>
    /// <param name="targetVolume">Volume to reach</param>
    /// <returns>Coroutine</returns>
    private IEnumerator StartFade(string mixer, float duration, float targetVolume)
    {
        // Initializes the fade;
        float currentTime = 0;
        float currentVol;
        MainMixer.GetFloat(mixer, out currentVol);
        currentVol = Mathf.Pow(10, currentVol / 20);

        // Clamp the target value for audio mixer safety
        float targetValue = Mathf.Clamp(targetVolume, minimumVolumeValue, maximumVolumeValue);

        // Performs the fade
        while (currentTime < duration)
        {
            // Increment time
            currentTime += Time.deltaTime;

            // Defines the volume according to time
            float newVol = Mathf.Lerp(currentVol, targetValue, currentTime / duration);

            // Set the volume
            MainMixer.SetFloat(mixer, Mathf.Log10(newVol) * 20);

            yield return null;
        }

        IsSoundInTransition = false;

        yield break;
    }

    #endregion Functions

    #region Methods

    #region Options

    /// <summary>
    /// Set the volume value of the main volume
    /// </summary>
    /// <param name="volume">New volume value</param>
    public void SetMusicVolume(float volume)
    {
        MainMixer.SetFloat("Music", Mathf.Log10(volume) * 20);

        SaveController.Instance.SaveState.Options.MusicVolume = volume;

        previousVolume = volume;
    }

    /// <summary>
    /// Set the volume value of the SFX volume
    /// </summary>
    /// <param name="volume">New volume value</param>
    public void SetSFXVolume(float volume)
    {
        MainMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);

        SaveController.Instance.SaveState.Options.SFXVolume = volume;
    }

    #endregion Options

    #region Transitions

    /// <summary>
    /// Changes the main music
    /// </summary>
    /// <param name="clip">Main music of the stage</param>
    public void ChangeMusicClip(AudioClip clip)
    {
        IsSoundInTransition = true;
        StopAllCoroutines();
        MusicAudioSource.Stop();

        MainMixer.SetFloat("Music", previousVolume);
        MusicAudioSource.clip = clip;
        MusicAudioSource.Play();
    }

    /// <summary>
    /// Performs a fade in on the main volume
    /// </summary>
    /// <param name="duration">Duration of the fade in</param>
    public void FadeIn(float duration)
    {
        IsSoundInTransition = true;
        StartCoroutine(StartFade("Music", duration, previousVolume));
    }

    /// <summary>
    /// Performs a fade out on the main volume
    /// </summary>
    /// <param name="duration">Duration of the fade out</param>
    public void FadeOut(float duration)
    {
        IsSoundInTransition = true;
        MainMixer.GetFloat("Music", out previousVolume);
        StartCoroutine(StartFade("Music", duration, minimumVolumeValue));
    }

    #endregion Transitions

    #region SFX

    /// <summary>
    /// Plays a sfx sound
    /// </summary>
    /// <param name="clip">Audio clip to play</param>
    public void PlaySFX(AudioClip clip)
    {
        SfxSource.clip = clip;
        SfxSource.Play();
    }

    /// <summary>
    /// Plays a random sfx with a random pitch
    /// </summary>
    /// <param name="clips">Audio array to play for an effect</param>
    public void PlaySFXWithRandomPitch(params AudioClip[] clips)
    {
        int randomIndex = UnityEngine.Random.Range(0, clips.Length);
        float randomPitch = UnityEngine.Random.Range(lowPitchRange, highPitchRange);

        SfxSource.pitch = randomPitch;
        SfxSource.clip = clips[randomIndex];
        SfxSource.Play();
    }

    #endregion SFX

    #endregion Methods 
}
