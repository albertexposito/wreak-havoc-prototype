using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OnlineCharacterSelectionInputHandler : MonoBehaviour
{

    public PlayerInput PlayerInput { get => _playerInput; }
    private PlayerInput _playerInput;

    private InputAction _toggleReadyStateAction;

    private PhotonSessionInfoController _onlineSessionInfoController;
    private PhotonPlayerIdentity _localPlayerIdentity;

    private void Awake()
    {
        _onlineSessionInfoController = FindObjectOfType<PhotonSessionInfoController>();
        _playerInput = GetComponent<PlayerInput>();

        InitializeInputActions();
    }

    public void InitializeInputActions()
    {
        _toggleReadyStateAction = _playerInput.actions.FindAction("ToggleReadyState");

        _toggleReadyStateAction.performed += PlayerToggleReadyStatePerformed;
    }

    private void PlayerToggleReadyStatePerformed(InputAction.CallbackContext obj)
    {
        if(_localPlayerIdentity == null)
            _localPlayerIdentity = _onlineSessionInfoController.LocalPlayerIdentity;

        _localPlayerIdentity.RPC_TogglePlayerReady();
    }


    private void OnDestroy()
    {
        _toggleReadyStateAction.performed -= PlayerToggleReadyStatePerformed;
    }

}
