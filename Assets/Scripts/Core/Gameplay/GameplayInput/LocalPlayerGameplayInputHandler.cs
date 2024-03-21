using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LocalPlayerGameplayInputHandler : MonoBehaviour
{

    public bool Initialized { get => _initialized; }
    private bool _initialized;

    public enum DeviceType { GAMEPAD = 0, KEYBOARD_MOUSE = 1 }

    public LocalPlayerGameplayInputData GameplayInputData { get => _playerInputData; }
    private LocalPlayerGameplayInputData _playerInputData;

    private PlayerInput _playerInput;

    private GameplayInputActions _gameplayInputActions;

    private BaseInputProcessor<Vector2> _rotationProcessor;

    private DeviceType _currentDevice;

    private void Awake()
    {
        _playerInputData = new LocalPlayerGameplayInputData();
    }

    public void Initialize(PlayerInput playerInput)
    {
        Debug.Log("[INPUT] - Initializing input for local player");

        _initialized = true;
        _playerInput = playerInput;

        InputActionMap actionMap = _playerInput.actions.FindActionMap("Player");
        _gameplayInputActions = new GameplayInputActions(actionMap);

        playerInput.SwitchCurrentActionMap(actionMap.name);

        CheckDeviceAndSetUpProcessors();
    }

    private void CheckDeviceAndSetUpProcessors()
    {

        Debug.Log($"[DEVICE] - Setting device for {gameObject.name}, device type: {_playerInput.currentControlScheme}");

        if(_playerInput.currentControlScheme == "Keyboard&Mouse")
        {
            _currentDevice = DeviceType.KEYBOARD_MOUSE;
            _rotationProcessor = new MouseRotationProcessor(transform, Camera.main);
        }
        else if (_playerInput.currentControlScheme == "Gamepad")
        {
            _currentDevice = DeviceType.KEYBOARD_MOUSE;
            _rotationProcessor = new GamepadRotationProcessor();
        }
    }

    /// <summary>
    /// Processes the local input and updates the LocalPlayerGameplayInputData with the current input values
    /// TODO - It can use a processor to get the values in different ways, for example look direction with
    /// Joystick or mouse pointer.
    /// </summary>
    public void ProcessInput()
    {

        _playerInputData.movementInput = _gameplayInputActions.moveAction.ReadValue<Vector2>();

        _playerInputData.rotationInput = _rotationProcessor.ProcessInput(_gameplayInputActions.rotateAction);

        _playerInputData.DashInput = _gameplayInputActions.dashAction.ReadValue<float>() > 0.1f;
        _playerInputData.FireInput = _gameplayInputActions.fireAction.ReadValue<float>() > 0.1f;
        _playerInputData.SpecialInput = _gameplayInputActions.specialAction.ReadValue<float>() > 0.1f;

    }



}


/// <summary>
/// Helper class to detect when an input has been pressed
/// </summary>
public class InputActionHandler
{
    private InputAction _action;


    private bool _active;

    public bool IsActive(bool consumeInput)
    {
        bool isActive = _active;

        if (consumeInput)
            _active = false;

        return isActive;
    }

    public InputActionHandler(InputAction action)
    {
        _action = action;
        _action.performed += SetInputState;
        _action.canceled += SetInputState;
    }

    private void SetInputState(InputAction.CallbackContext obj)
    {
        // Debug.Log($"{obj.action.name} - {obj.phase}");

        switch (obj.phase)
        {
            case InputActionPhase.Performed: _active = true; break;
                //case InputActionPhase.Canceled: _active = false; break;
        }
    }
}

public class GameplayInputActions
{

    private readonly InputActionMap _gameplayActionMap;

    public readonly InputAction moveAction;
    public readonly InputAction rotateAction;
    public readonly InputAction dashAction;
    public readonly InputAction fireAction;
    public readonly InputAction specialAction;

    public void EnableGameplayActions() => _gameplayActionMap.Enable();
    public void DisableGameplayActions() => _gameplayActionMap.Disable();

    public GameplayInputActions(InputActionMap gameplayActionMap)
    {
        _gameplayActionMap = gameplayActionMap;

        moveAction = gameplayActionMap.FindAction("Move");
        rotateAction = gameplayActionMap.FindAction("Rotate");
        dashAction = gameplayActionMap.FindAction("Dash");
        fireAction = gameplayActionMap.FindAction("Fire");
        specialAction = gameplayActionMap.FindAction("Special");

    }


}
