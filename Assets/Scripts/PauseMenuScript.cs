using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PauseMenuScript : MonoBehaviour
{
    [SerializeField]
    private GameObject content;   

    void Start()
    {
        StartDifficulty();
        StartSounds();
        ChangePause(content.activeSelf);
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            ChangePause(!content.activeSelf);
        }
    }

    private void ChangePause(bool pause)
    {
        content.SetActive(pause);
        Time.timeScale = pause ? 0.0f : 1.0f;
        Cursor.lockState = pause ? CursorLockMode.None : CursorLockMode.Locked;
    }

    public void OnResumePressed()
    {
        ChangePause(false);
    }
    
    #region SoundsSettings
    [SerializeField]
    private AudioMixer audioMixer;
    [SerializeField]
    private Toggle muteAllToggle;
    [SerializeField]
    private Slider ambientSlider;
    [SerializeField]
    private Slider effectsSlider;
    [SerializeField]
    private Slider musicSlider;
    [SerializeField]
    private TMPro.TMP_Dropdown clipsDropdown;

    private MusicSoundsScript musicSoundsScript;


    private void StartSounds()
    {
        OnMuteAllChange(muteAllToggle.isOn);
        OnAmbientVolumeChange(ambientSlider.value);
        OnEffectsVolumeChange(effectsSlider.value);
        OnMusicVolumeChange(musicSlider.value);
        musicSoundsScript = GameObject.Find("MusicSounds").GetComponent<MusicSoundsScript>();
        clipsDropdown.options.Clear();
        foreach(var clip in musicSoundsScript.audioClips)
        {
            clipsDropdown.options.Add(new(clip.name));
        }
        clipsDropdown.value = musicSoundsScript.SelectedIndex = 0;
    }

    public void OnAmbientVolumeChange(float volume)
    {
        audioMixer.SetFloat("AmbientVolume", dBA(volume));
    }

    public void OnEffectsVolumeChange(float volume)
    {
        audioMixer.SetFloat("EffectsVolume", dBA(volume));
    }

    public void OnMusicVolumeChange(float volume)
    {
        audioMixer.SetFloat("MusicVolume", dBA(volume));
    }

    public void OnMuteAllChange(bool mute)
    {
        audioMixer.SetFloat("MasterVolume", mute ? -80 : 0);
    }

    public void OnClipDropDownChanged(int value)
    {
        musicSoundsScript.SelectedIndex = value;
    }
    /// <summary>
    /// Maps [0..1] Volume to [-80..+10] dB Acoustic
    /// </summary>
    /// <param name="volume"></param>
    /// <returns></returns>
    private float dBA(float volume) => -80f + 90f * volume;

    #endregion

    #region Difficulty settings
    [SerializeField]
    private Toggle compassToggle;
    [SerializeField]
    private Toggle radarToggle;
    [SerializeField]
    private Toggle hintsToggle;
    private void StartDifficulty()
    {
        OnCompassVisibleChanged(compassToggle.isOn);
        OnRadarVisibleChanged(radarToggle.isOn);
        OnHintsVisibleChanged(hintsToggle.isOn);
        if (GameState.CoinCost == 0f)
        {
            GameState.UpdateCoinCost();
        }
    }
    public void OnCompassVisibleChanged(bool value)
    {
        GameState.isCompassVisible = value;
    }

    public void OnRadarVisibleChanged(bool value)
    {
        GameState.isRadarVisible = value;
    }

    public void OnHintsVisibleChanged(bool value)
    {
        GameState.isHintsVisible = value;
    }

    #endregion
}
