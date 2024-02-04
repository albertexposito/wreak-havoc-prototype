using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


/// <summary>
/// This class detects all the devices from the Input System
/// and stores a reference to each PlayerInput Prefab
/// </summary>
public class InputDeviceController : MonoBehaviour
{

    // UNITY INPUT SYSTEM PLAYER MANAGER
    [SerializeField] private PlayerInputManager _playerInputManager;

    private List<PlayerInput> _currentLocalDevices;

    public event Action<PlayerInput> OnLocalDeviceDetected;
    public event Action<PlayerInput> OnLocalDeviceLost;

    [SerializeField] private LocalGameManager _localGameManager;

    private void Awake()
    {

        _localGameManager.InitManager();

        _currentLocalDevices = new List<PlayerInput>(ConstantValues.MAX_PLAYERS_PER_GAME);

        _playerInputManager.onPlayerJoined += OnDeviceDetected;
        _playerInputManager.onPlayerLeft += OnDeviceDisconnected;

    }

    /// <summary>
    /// Called when the PlayerInputManager detects a new device
    /// </summary>
    /// <param name="playerInput"></param>
    private void OnDeviceDetected(PlayerInput playerInput)
    {
        _currentLocalDevices.Add(playerInput);

        Debug.Log($"Device connected! | There are {_currentLocalDevices.Count} devices");

        OnLocalDeviceDetected?.Invoke(playerInput);

        if(_localGameManager != null)
            _localGameManager.AddPlayer(playerInput);
    }

    /// <summary>
    /// Called when the controller device is disconnected
    /// </summary>
    /// <param name="playerInput"></param>
    private void OnDeviceDisconnected(PlayerInput playerInput)
    {

        for (int i = _currentLocalDevices.Count - 1; i >= 0; i--)
            if (_currentLocalDevices[i] == playerInput)
                _currentLocalDevices.RemoveAt(i);
            
        Debug.Log($"Device disconected! | There are {_currentLocalDevices.Count} devices");

        OnLocalDeviceLost?.Invoke(playerInput);

    }

    private void OnDestroy()
    {
        _playerInputManager.DisableJoining();

        _playerInputManager.onPlayerJoined -= OnDeviceDetected;
        _playerInputManager.onPlayerLeft -= OnDeviceDisconnected;
    }

}
