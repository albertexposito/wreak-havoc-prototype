using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUIHandler : MonoBehaviour
{

    [SerializeField] private Button _localGameButton;
    [SerializeField] private Button _onlineGameButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _exitButton;

    private void Awake()
    {
        _localGameButton.onClick.AddListener(LoadLocalGameScene);
        _onlineGameButton.onClick.AddListener(LoadOnlineGameLobby);
        _settingsButton.onClick.AddListener(OpenSettingsMenu);
        _exitButton.onClick.AddListener(ExitGame);
    }


    private void LoadLocalGameScene()
    {
        SceneManager.LoadScene(ConstantValues.LOCAL_CHARACTER_SELECTION_SCENE_NAME);
    }


    private void LoadOnlineGameLobby()
    {
        SceneManager.LoadScene(ConstantValues.PHOTON_LOBBY_SCENE_NAME);
    }

    private void OpenSettingsMenu()
    {
        SceneManager.LoadScene(ConstantValues.QUALITY_SETTINGS_SCENE_NAME);
    }


    private void ExitGame()
    {
        Application.Quit();
    }
}
