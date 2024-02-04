using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// This class Listens to the DeviceController and creates a LocalPlayer instance based on the device that has been created.
/// </summary>
public class LocalPlayerProvider : MonoBehaviour
{

    [SerializeField] private LocalPlayerController _playerController;

    [SerializeField] private InputDeviceController _inputDeviceController;
    private List<PlayerInputHandler> _localPlayerInputHandlers;

    private void Awake()
    {
        _localPlayerInputHandlers = new List<PlayerInputHandler>(4);

        _inputDeviceController.OnLocalDeviceDetected += InitializePlayer;
    }

    public void InitializePlayer(PlayerInput playerInput)
    {
        int playerId = _localPlayerInputHandlers.Count;
        //LocalPlayer newLocalPlayer = new LocalPlayer(playerId.ToString(), $"Player {playerId}", playerInput);

        PlayerInputHandler playerInputHandler = playerInput.GetComponent<PlayerInputHandler>();
        //playerInputHandler.InitializeInputActions(newLocalPlayer);

        playerInputHandler.OnPlayerJoinedActionPerformed += TryJoinGame;
        playerInputHandler.OnPlayerLeaveActionPerformed += TryLeaveGame;

        _localPlayerInputHandlers.Add(playerInputHandler);
    }

    public void TryJoinGame(PlayerInputHandler playerInputHandler)
    {
        //_playerController.PlayerJoinsGame(playerInputHandler.LocalPlayer);
    }

    public void TryLeaveGame(PlayerInputHandler playerInputHandler)
    {
        //_playerController.PlayerLeavesGame(playerInputHandler.LocalPlayer);
    }

    private void OnDestroy()
    {
        foreach(PlayerInputHandler playerInputHandler in _localPlayerInputHandlers)
        {
            playerInputHandler.OnPlayerJoinedActionPerformed -= TryJoinGame;
            playerInputHandler.OnPlayerLeaveActionPerformed -= TryLeaveGame;
        }
    }

}
