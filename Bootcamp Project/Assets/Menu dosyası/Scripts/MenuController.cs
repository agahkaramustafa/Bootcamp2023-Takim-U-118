using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuController : MonoBehaviour
{
    [Header("Volume Setting")]
    [SerializeField] private TMP_Text volumeTextValue;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private float defaultVolume = 1.0f;

    [Header("Gameplay Settings")]
    [SerializeField] private TMP_Text controllerSenTextValue;
    [SerializeField] private Slider controllerSenSlider;
    [SerializeField] private int defaultSen = 4;
    public int mainControllerSen = 4;

    [Header("Toggle Settings")]
    [SerializeField] private Toggle invertYToggle;

    [Header("Graphics Settings")]
    [SerializeField] private Slider brightnessSlider;
    [SerializeField] private TMP_Text brightnessTextValue;
    [SerializeField] private float defaultBrightness = 1;

    [Space(10)]
    [SerializeField] private TMP_Dropdown qualityDropdown;
    [SerializeField] private Toggle fullscreenToggle;

    private int _qualityLevel;
    private bool _isFullscreen;
    private float _brightnessLevel;

    [Header("Confirmation")]
    [SerializeField] private GameObject confirmationPrompt;

    [Header("Levels To Load")]
    public string newGameLevel;
    private string levelToLoad;
    [SerializeField] private GameObject noSavedGameDialog;

    [Header("Resolution Dropdown")]
    public TMP_Dropdown resolutionDropdown;
    private Resolution[] resolutions;

    private void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, _isFullscreen);
    }

    public void NewGameDialogYes()
    {
        SceneManager.LoadScene(newGameLevel);
    }

    public void LoadGameDialogYes()
    {
        if (PlayerPrefs.HasKey("SavedLevel"))
        {
            levelToLoad = PlayerPrefs.GetString("SavedLevel");
            SceneManager.LoadScene(levelToLoad);
        }
        else
        {
            noSavedGameDialog.SetActive(true);
        }
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        volumeTextValue.text = volume.ToString("0.0");
    }

    public void VolumeApply()
    {
        PlayerPrefs.SetFloat("masterVolume", AudioListener.volume);
        StartCoroutine(ConfirmationBox());
    }

    public void SetControllerSen(float sensitivity)
    {
        mainControllerSen = Mathf.RoundToInt(sensitivity);
        controllerSenTextValue.text = sensitivity.ToString("0");
    }

    public void GameApply()
    {
        PlayerPrefs.SetInt("masterInvertY", invertYToggle.isOn ? 1 : 0);
        PlayerPrefs.SetFloat("masterSen", mainControllerSen);
        StartCoroutine(ConfirmationBox());
    }

    public void SetBrightness(float brightness)
    {
        _brightnessLevel = brightness;
        brightnessTextValue.text = brightness.ToString("0.0");
    }

    public void GraphicsApply()
    {
        PlayerPrefs.SetInt("masterQuality", qualityDropdown.value);
        PlayerPrefs.SetFloat("masterBrightness", _brightnessLevel);
        PlayerPrefs.SetInt("masterFullscreen", fullscreenToggle.isOn ? 1 : 0);
        StartCoroutine(ConfirmationBox());
    }

    public void ResetButton(string setting)
    {
        switch (setting)
        {
            case "Audio":
                volumeSlider.value = defaultVolume;
                break;
            case "Gameplay":
                controllerSenSlider.value = defaultSen;
                break;
            case "Graphics":
                brightnessSlider.value = defaultBrightness;
                qualityDropdown.value = QualitySettings.GetQualityLevel();
                fullscreenToggle.isOn = Screen.fullScreen;
                break;
        }
    }

    private IEnumerator ConfirmationBox()
    {
        confirmationPrompt.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        confirmationPrompt.SetActive(false);
    }
}