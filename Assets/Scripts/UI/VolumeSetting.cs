using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class VolumeSetting : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private Slider _audioSlider;
    [SerializeField] private Slider _SFXSlider;

    public void Start()
    {
        if (PlayerPrefs.HasKey("audioVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetSFXVolume();
            SetMusicVolume();
        }
        
    }
    public void SetMusicVolume()
    {
        float volume = _audioSlider.value;
        _audioMixer.SetFloat("Audio", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("audioVolume", volume);
    }

    public void SetSFXVolume()
    {
       float sfxVolume = _SFXSlider.value;
        _audioMixer.SetFloat("SFX", Mathf.Log10(sfxVolume)*20);
        PlayerPrefs.SetFloat("sfxVolume", sfxVolume);
    }

    public void LoadVolume()
    {
        _audioSlider.value = PlayerPrefs.GetFloat("audioVolume");
        _SFXSlider.value = PlayerPrefs.GetFloat("sfxVolume");

        SetSFXVolume();
        SetMusicVolume();
    }
}
