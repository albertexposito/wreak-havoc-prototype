using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class CharacterSpawningState : BaseCharacterState
{
    private LocalPlayerGameplayInputHandler _inputHandler;
    private HealthHandler _healthHandler;
    private BaseCharacterController _charController;
    private BasePlayerCharacter _playerCharacter;

    // The first time the character spawns it will be done by the game controller
    // The next time it will be done by the state.
    // TODO: consider adding a Respawn state
    private bool _spawnedOnce; 


    public CharacterSpawningState(BaseCharacterStateMachine stateMachine, BasePlayerCharacter baseCharacter) : base(stateMachine, baseCharacter)
    {
        _stateName = "SPAWNING";

        _inputHandler = baseCharacter.PlayerInputHandler;
        _healthHandler = baseCharacter.HealthHandler;
        _charController = baseCharacter.CharacterController;
        _playerCharacter = baseCharacter;
    }

    public override void OnStateEnter()
    {
        base.OnStateEnter();

        _healthHandler.RestoreHealth();
        _charController.ResetVelocity();

    }

    public override void UpdateStateLogic()
    {
        base.UpdateStateLogic();

        //LocalPlayerGameplayInputData inputData = _inputHandler.GameplayInputData;

        LocalPlayerGameplayInputData inputData = _inputHandler != null ? _inputHandler.GameplayInputData : _playerCharacter.InputData;


        _charController.PerformMovement(Vector3.zero, inputData.rotationInput);

        if (_spawnedOnce && StateTime > ConstantValues.SPAWN_TIME)
            _playerCharacter.ActivateCharacter();

    }

    public override void OnStateExit()
    {
        base.OnStateExit();

        _spawnedOnce = true;
    }
}
