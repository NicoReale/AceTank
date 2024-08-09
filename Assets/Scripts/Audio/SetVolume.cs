using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{
    public AudioMixer[] mixers;
    public Slider musicVolumeSlider;
    public Slider soundEffectsSlider;

    private void Start()
    {
        if (musicVolumeSlider != null)
        {
            musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
            float audiolevel = Mathf.Log10(musicVolumeSlider.value + 0.00001f) * 20;
            mixers[0].SetFloat("SoundVolume", audiolevel);
        }
        if (soundEffectsSlider != null)
        {
            soundEffectsSlider.value = PlayerPrefs.GetFloat("FXVolume", 0.5f);
            float audiolevel = Mathf.Log10(soundEffectsSlider.value + 0.00001f) * 20;
            mixers[1].SetFloat("SoundVolume", audiolevel);
        }
    }

    public void SetMusicLevel()
    {
        float audiolevel = Mathf.Log10(musicVolumeSlider.value + 0.00001f) * 20;
        mixers[0].SetFloat("SoundVolume", audiolevel);
        PlayerPrefs.SetFloat("MusicVolume", musicVolumeSlider.value);
        PlayerPrefs.Save();
    }

    public void SetEffectsLevel()
    {
        float audiolevel = Mathf.Log10(soundEffectsSlider.value + 0.00001f) * 20;
        mixers[1].SetFloat("SoundVolume", audiolevel);
        PlayerPrefs.SetFloat("FXVolume", soundEffectsSlider.value);
        PlayerPrefs.Save();
    }

}
