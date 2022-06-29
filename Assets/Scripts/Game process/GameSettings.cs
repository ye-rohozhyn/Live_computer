using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameSettings : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown graphics;
    [SerializeField] private AudioSource[] allSoundSources;
    [SerializeField] private AudioSource[] allMusicSources;
    [SerializeField] private TMP_InputField soundInputField;
    [SerializeField] private Slider soundSlider;
    [SerializeField] private TMP_InputField musicInputField;
    [SerializeField] private Slider musicSlider;

    private float soundMultiplier = 1;
    private float musicMultiplier = 1;

    private List<float> allSoundVolume = new List<float>();
    private List<float> allMusicVolume = new List<float>();

    private void Start()
    {
        graphics.value = PlayerPrefs.GetInt("Graphics index");
        SetQuality(graphics.value);
        soundMultiplier = PlayerPrefs.GetFloat("Sound multiplier", 1);
        musicMultiplier = PlayerPrefs.GetFloat("Music multiplier", 1);

        soundInputField.text = (soundMultiplier * 100).ToString();
        musicInputField.text = (musicMultiplier * 100).ToString();

        soundSlider.value = soundMultiplier;
        musicSlider.value = musicMultiplier;

        soundInputField.onEndEdit.AddListener(EndEditSoundTextField);
        musicInputField.onEndEdit.AddListener(EndEditMusicTextField);
        soundSlider.onValueChanged.AddListener(SoundSliderChanged);
        musicSlider.onValueChanged.AddListener(MusicSliderChanged);

        foreach (AudioSource soundSource in allSoundSources)
        {
            allSoundVolume.Add(soundSource.volume);
            soundSource.volume *= soundMultiplier;
        }
        foreach (AudioSource musicSource in allMusicSources)
        {
            allMusicVolume.Add(musicSource.volume);
            musicSource.volume *= musicMultiplier;
        }
    }

    private void EndEditSoundTextField(string arg)
    {
        try
        {
            float value = Convert.ToInt32(arg);

            if (value > 100) value = 100;
            else if (value < 0) value = 0;

            value = Mathf.Floor(value);

            soundInputField.text = value.ToString();
            soundSlider.value = value / 100;
        }
        catch
        {
            soundInputField.text = (soundMultiplier * 100).ToString();
            soundSlider.value = soundMultiplier;
        }
    }

    private void EndEditMusicTextField(string arg)
    {
        try
        {
            float value = Convert.ToInt32(arg);

            if (value > 100) value = 100;
            else if (value < 0) value = 0;

            value = Mathf.Floor(value);

            musicInputField.text = value.ToString();
            musicSlider.value = value / 100;
        }
        catch
        {
            musicInputField.text = (musicMultiplier * 100).ToString();
        }
    }

    private void SoundSliderChanged(float value)
    {
        soundInputField.text = Mathf.Floor(value * 100).ToString();
    }

    private void MusicSliderChanged(float value)
    {
        musicInputField.text = Mathf.Floor(value * 100).ToString();
    }

    public void ApplyButton()
    {
        int graphicsIndex = graphics.value;

        PlayerPrefs.SetInt("Graphics index", graphicsIndex);

        soundMultiplier = float.Parse(soundInputField.text) / 100;
        musicMultiplier = float.Parse(musicInputField.text) / 100;

        PlayerPrefs.SetFloat("Sound multiplier", soundMultiplier);
        PlayerPrefs.SetFloat("Music multiplier", musicMultiplier);

        for (int i = 0; i < allSoundSources.Length; i++)
            allSoundSources[i].volume = allSoundVolume[i] * soundMultiplier;

        for (int i = 0; i < allMusicSources.Length; i++)
            allMusicSources[i].volume = allMusicVolume[i] * musicMultiplier;

        SetQuality(graphicsIndex);
    }

    private void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
}
