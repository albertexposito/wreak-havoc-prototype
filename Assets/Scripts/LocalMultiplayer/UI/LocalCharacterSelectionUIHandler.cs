using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LocalCharacterSelectionUIHandler : MonoBehaviour
{

    [SerializeField] private Button _startGameButton;
    [SerializeField] private Button _openSettingsButton;

    private PlayerSlotContainer _playerSlotContainer;

    private LocalGameManager _localGameManager;

    private void Start()
    {
        _playerSlotContainer = GetComponentInChildren<PlayerSlotContainer>();

        _localGameManager = LocalGameManager.Instance;

        if(_localGameManager == null)
        {
            Debug.LogError($"[LocalCharacterSelectionUIHandler -> Start] - LocalGameManager is null");
            return;
        }

        _localGameManager.OnPlayerJoinedGame += UpdatePlayerJoinedSlotUI;

        foreach(Player player in _localGameManager.Players)
            UpdatePlayerJoinedSlotUI(player);

        CheckGameStateAndEnableStartButton();

        _startGameButton.onClick.AddListener(LocalGameManager.Instance.BeginGamePreparation);
        _openSettingsButton.onClick.AddListener(OpenQualitySettingsScene);
    }

    private void UpdatePlayerJoinedSlotUI(Player playerThatJoinedGame)
    {
        _playerSlotContainer.AddPlayer(playerThatJoinedGame);
        CheckGameStateAndEnableStartButton();
    }

    private void UpdatePlayerLeftSlotUI(Player playerThatLeftGame, int oldIndex)
    {
        _playerSlotContainer.RemovePlayer(playerThatLeftGame, oldIndex);
        CheckGameStateAndEnableStartButton();
    }

    private void OpenQualitySettingsScene()
    {
        SceneManager.LoadScene(ConstantValues.QUALITY_SETTINGS_SCENE_NAME);
    }

    private void CheckGameStateAndEnableStartButton()
    {
        bool enableButton = _localGameManager.Players.Count >= 2;

        _startGameButton.interactable = enableButton;
    }

    private void OnDestroy()
    {
        _startGameButton.onClick.RemoveListener(LocalGameManager.Instance.BeginGamePreparation);

        _localGameManager.OnPlayerJoinedGame -= UpdatePlayerJoinedSlotUI;
    }
}
