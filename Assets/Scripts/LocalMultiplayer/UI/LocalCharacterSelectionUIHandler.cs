using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalCharacterSelectionUIHandler : MonoBehaviour
{

    [SerializeField] private Button _startGameButton;
    [SerializeField] private Button _exitGameButton;

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

        _startGameButton.onClick.AddListener(LocalGameManager.Instance.BeginGamePreparation);
    }

    private void UpdatePlayerJoinedSlotUI(Player playerThatJoinedGame)
    {
        _playerSlotContainer.AddPlayer(playerThatJoinedGame);
    }

    private void UpdatePlayerLeftSlotUI(Player playerThatLeftGame, int oldIndex)
    {
        _playerSlotContainer.RemovePlayer(playerThatLeftGame, oldIndex);
    }


    private void OnDestroy()
    {
        _startGameButton.onClick.RemoveListener(LocalGameManager.Instance.BeginGamePreparation);

        _localGameManager.OnPlayerJoinedGame -= UpdatePlayerJoinedSlotUI;
    }
}
