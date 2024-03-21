using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PhotonCharacterSelectionUIHandler : MonoBehaviour
{
    private PhotonSessionInfoController _photonSessionInfoController;
    [SerializeField] private Button _startGameButton;

    private PlayerSlotContainer _playerSlotContainer;

    private void Awake()
    {
        _playerSlotContainer = GetComponentInChildren<PlayerSlotContainer>();
        _photonSessionInfoController = FindObjectOfType<PhotonSessionInfoController>();

        _photonSessionInfoController.OnPlayerJoined += UpdatePlayerJoinedSlotUI;


        //foreach (PhotonPlayerIdentity player in _photonSessionInfoController.Players)
        //    UpdatePlayerJoinedSlotUI(player);

    }

    private void UpdatePlayerJoinedSlotUI(PhotonPlayerIdentity playerThatJoinedGame)
    {
        if (playerThatJoinedGame == null)
            return;

        _playerSlotContainer.AddPlayer(playerThatJoinedGame);
    }

    private void UpdatePlayerLeftSlotUI(Player playerThatLeftGame, int oldIndex)
    {
        _playerSlotContainer.RemovePlayer(playerThatLeftGame, oldIndex);
    }

    private void OpenQualitySettingsScene()
    {
        SceneManager.LoadScene(ConstantValues.QUALITY_SETTINGS_SCENE_NAME);
    }


    private void OnDestroy()
    {

    }
}
