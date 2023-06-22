using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoadPrefs : MonoBehaviour
{
    [Header("Volume Setting")]
    [SerializeField] private TMP_Text volumeTextValue;
    [SerializeField] private Slider volumeSlider;

    [Header("Graphics Settings")]
    [SerializeField] private Slider brightnessSlider;
    [SerializeField] private TMP_Text brightnessTextValue;
    [SerializeField] private TMP_Dropdown qualityDropdown;
    [SerializeField] private Toggle fullscreenToggle;

    [Header("Gameplay Settings")]
    [SerializeField] private TMP_Text controllerSenTextValue;
    [SerializeField] private Slider controllerSenSlider;
    private int mainControllerSen;

    [Header("Toggle Settings")]
    [SerializeField] private Toggle invertYToggle;

    private void Start()
    {
        LoadPlayerPrefs();
    }

    private void LoadPlayerPrefs()
    {
        if (PlayerPrefs.HasKey("masterVolume"))
        {
            float localVolume = PlayerPrefs.GetFloat("masterVolume");
            volumeTextValue.text = localVolume.ToString("0.0");
            volumeSlider.value = localVolume;
            AudioListener.volume = localVolume;
        }

        if (PlayerPrefs.HasKey("masterQuality"))
        {
            int localQuality = PlayerPrefs.GetInt("masterQuality");
            qualityDropdown.value = localQuality;
            QualitySettings.SetQualityLevel(localQuality);
        }

        if (PlayerPrefs.HasKey("masterFullscreen"))
        {
            int localFullscreen = PlayerPrefs.GetInt("masterFullscreen");
            fullscreenToggle.isOn = localFullscreen == 1;
            Screen.fullScreen = fullscreenToggle.isOn;
        }

        if (PlayerPrefs.HasKey("masterBrightness"))
        {
            float localBrightness = PlayerPrefs.GetFloat("masterBrightness");
            brightnessTextValue.text = localBrightness.ToString("0.0");
            brightnessSlider.value = localBrightness;
        }

        if (PlayerPrefs.HasKey("masterSen"))
        {
            float localSensitivity = PlayerPrefs.GetFloat("masterSen");
            controllerSenTextValue.text = localSensitivity.ToString("0");
            controllerSenSlider.value = localSensitivity;
            mainControllerSen = Mathf.RoundToInt(localSensitivity);
        }

        if (PlayerPrefs.HasKey("masterInvertY"))
        {
            invertYToggle.isOn = PlayerPrefs.GetInt("masterInvertY") == 1;
        }
    }
}