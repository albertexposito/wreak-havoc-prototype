using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


/// <summary>
/// This class holds the information for the player input.
/// The player prefab is not the character itself, it's this prefab,
/// that holds the information about the player
/// </summary>
public class PlayerInputHandler : MonoBehaviour
{
    private GameManager _gameManager;

    public PlayerInput PlayerInput { get => _playerInput; }
    private PlayerInput _playerInput;

    private InputAction _joinGameAction;
    private InputAction _leaveGameAction;

    public event Action<PlayerInputHandler> OnPlayerJoinedActionPerformed;
    public event Action<PlayerInputHandler> OnPlayerLeaveActionPerformed;

    // GAMEPLAY INPUT ACTIONS
    // TODO

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        InitializeInputActions(null);
        
    }

    private void Start()
    {
        _gameManager = GameManager.Instance;
    }

    public void InitializeInputActions(LocalPlayer localPlayer)
    {
        Debug.Log("LocalPlayerInitialized!");

        _joinGameAction = _playerInput.actions.FindAction("Join");
        _leaveGameAction = _playerInput.actions.FindAction("Leave");

        _joinGameAction.performed += PlayerJoinGame;
        _leaveGameAction.performed += PlayerLeaveGame;

    }

    private void PlayerJoinGame(InputAction.CallbackContext obj)
    {
        OnPlayerJoinedActionPerformed?.Invoke(this);
    }

    private void PlayerLeaveGame(InputAction.CallbackContext obj)
    {
        OnPlayerLeaveActionPerformed?.Invoke(this);
    }

}

