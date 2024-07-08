using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundMixerManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider soundFXVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;

    private void Start()
    {
        // Gán các hàm cho sự kiện onValueChanged của các slider
        masterVolumeSlider.onValueChanged.AddListener(SetMasterVolume);
        soundFXVolumeSlider.onValueChanged.AddListener(SetSoundFXVolume);
        musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);

        // Tải lại các giá trị từ PlayerPrefs và gán cho các slider
        LoadVolumeSettings();
    }

    public void SetMasterVolume(float level)
    {
        audioMixer.SetFloat("masterVolume", Mathf.Log10(level) * 20);
        PlayerPrefs.SetFloat("masterVolume", level);
        PlayerPrefs.Save();
    }

    public void SetSoundFXVolume(float level)
    {
        audioMixer.SetFloat("soundFXVolume", Mathf.Log10(level) * 20);
        PlayerPrefs.SetFloat("soundFXVolume", level);
        PlayerPrefs.Save();
    }

    public void SetMusicVolume(float level)
    {
        audioMixer.SetFloat("musicVolume", Mathf.Log10(level) * 20);
        PlayerPrefs.SetFloat("musicVolume", level);
        PlayerPrefs.Save();
    }

    private void LoadVolumeSettings()
    {
        if (PlayerPrefs.HasKey("masterVolume"))
        {
            float masterVolume = PlayerPrefs.GetFloat("masterVolume");
            audioMixer.SetFloat("masterVolume", Mathf.Log10(masterVolume) * 20);
            masterVolumeSlider.value = masterVolume;
        }
        else
        {
            masterVolumeSlider.value = 1.0f; 
        }

        if (PlayerPrefs.HasKey("soundFXVolume"))
        {
            float soundFXVolume = PlayerPrefs.GetFloat("soundFXVolume");
            audioMixer.SetFloat("soundFXVolume", Mathf.Log10(soundFXVolume) * 20);
            soundFXVolumeSlider.value = soundFXVolume;
        }
        else
        {
            soundFXVolumeSlider.value = 1.0f;
        }

        if (PlayerPrefs.HasKey("musicVolume"))
        {
            float musicVolume = PlayerPrefs.GetFloat("musicVolume");
            audioMixer.SetFloat("musicVolume", Mathf.Log10(musicVolume) * 20);
            musicVolumeSlider.value = musicVolume;
        }
        else
        {
            musicVolumeSlider.value = 1.0f;
        }
    }
}
