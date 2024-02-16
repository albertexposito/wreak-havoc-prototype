using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsUIHandler : MonoBehaviour
{

    [SerializeField] private TMP_Dropdown _resolutionDropDown;
    [SerializeField] private TMP_Dropdown _qualitySettingsDropDown;
    [SerializeField] private Button _applySettingsButton;
    [SerializeField] private Button _returnButton;

    private List<Resolution> _filteredResolutions;

    private Resolution _currentResolution;
    private int _currentQualitySettings;

    private Resolution _resolutionToApply;
    private int _qualitySettingsToApply;

    public void Start()
    {
        ValidateResolutionAndQualitySetButtonInteractivity();
        _applySettingsButton.onClick.AddListener(ApplyResolutionAndQuality);
        _returnButton.onClick.AddListener(ReturnToMainMenu);

        SetupResolutionDropdown();
        SetupQualityDropDown();
    }

    private void SetupResolutionDropdown()
    {
        _currentResolution = Screen.currentResolution;

        Resolution[] resolutions = Screen.resolutions;

        RefreshRate currentRefreshRate = _currentResolution.refreshRateRatio;
        //int currentRefreshRate = _currentResolution.refreshRate;

        _filteredResolutions = new List<Resolution>();
        List<string> resolutionNames = new List<string>();
        int currentResolutionIndex = -1;

        for (int i = 0; i < resolutions.Length; i++)
        {
            Resolution resolution = resolutions[i];

            if (resolution.refreshRateRatio.value == currentRefreshRate.value)
            {
                _filteredResolutions.Add(resolution);
                resolutionNames.Add(resolution.ToString());

                if(IsSameResolution(resolution, _currentResolution))
                    currentResolutionIndex = i;

            }
        }

        // Dropdown initialization

        _resolutionDropDown.ClearOptions();
        _resolutionDropDown.AddOptions(resolutionNames);
        _resolutionDropDown.value = currentResolutionIndex;

        _resolutionDropDown.onValueChanged.AddListener(OnResolutionDropdownChanged);

    }

    private bool IsSameResolution(Resolution res1, Resolution res2) => res1.width == res2.width && res1.height == res2.height;

    private void SetupQualityDropDown()
    {
        _currentQualitySettings = QualitySettings.GetQualityLevel();

        List<string> qualityLevelNames = QualitySettings.names.ToList();

        _qualitySettingsDropDown.ClearOptions();
        _qualitySettingsDropDown.AddOptions(qualityLevelNames);
        _qualitySettingsDropDown.value = _currentQualitySettings;
        _qualitySettingsDropDown.onValueChanged.AddListener(OnQualitySettingsDropdownChanged);

    }

    private void OnQualitySettingsDropdownChanged(int index)
    {
        _qualitySettingsToApply = index;
        ValidateResolutionAndQualitySetButtonInteractivity();
    }


    private void OnResolutionDropdownChanged(int index)
    {
        _resolutionToApply = _filteredResolutions[index];
        ValidateResolutionAndQualitySetButtonInteractivity();
    }

    private void ValidateResolutionAndQualitySetButtonInteractivity()
    {
        _applySettingsButton.interactable = _currentQualitySettings != _qualitySettingsToApply || !IsSameResolution(_currentResolution, _resolutionToApply);
    }

    private void ApplyResolutionAndQuality()
    {

        if(_currentQualitySettings != _qualitySettingsToApply)
        {
            QualitySettings.SetQualityLevel(_qualitySettingsToApply, true);
            _currentQualitySettings = _qualitySettingsToApply;
        }

        if(!IsSameResolution(_currentResolution, _resolutionToApply))
        {
            Screen.SetResolution(_resolutionToApply.width, _resolutionToApply.height, FullScreenMode.FullScreenWindow, _resolutionToApply.refreshRateRatio);
            _currentResolution = _resolutionToApply;
        }

        ValidateResolutionAndQualitySetButtonInteractivity();

    }

    private void ReturnToMainMenu()
    {
        SceneManager.LoadScene(ConstantValues.LOCAL_CHARACTER_SELECTION_SCENE_NAME);
    }

}
