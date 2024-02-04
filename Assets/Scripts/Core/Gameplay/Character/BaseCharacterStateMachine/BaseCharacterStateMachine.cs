using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BaseCharacterStateMachine : MonoBehaviour
{


    private BaseCharacterState _currentState;

    [Tooltip("Param1: The new state. Param2: The old state")]
    public UnityEvent<BaseCharacterState, BaseCharacterState> OnCharacterStateChanged;

    public string CurrentStateName { get => _currentState != null ? _currentState.StateName : "NO STATE!"; }

    #region STATES

    public BaseCharacterState NeutralState;
    public BaseCharacterState SpawningState;
    public BaseCharacterState HitStunnedState;
    public BaseCharacterState DeadState;

    #endregion STATES

    private void Awake()
    {

    }

    public void InitializeStateMachine(BasePlayerCharacter basePlayerCharacter)
    {
        SpawningState = new CharacterSpawningState(this, basePlayerCharacter);
        NeutralState = new CharacterNeutralState(this, basePlayerCharacter);
        HitStunnedState = new CharacterHitState(this, basePlayerCharacter);
        DeadState = new CharacterDeadState(this, basePlayerCharacter);
    }

    public void SetInitialState()
    {
        ChangeState(SpawningState);
    }

    public void UpdateStateMachineLogic()
    {
        if (_currentState == null)
            return;

        _currentState.UpdateStateLogic();
    }

    public void HandleDamageTaken(DamageData damageData)
    {
        if (_currentState == null)
            return;

        _currentState.OnDamageTaken(damageData);
    }

    public void ChangeState(BaseCharacterState newState)
    {
        BaseCharacterState oldState = _currentState;

        if (_currentState != null)
            _currentState.OnStateExit();

        _currentState = newState;

        _currentState.OnStateEnter();
        OnCharacterStateChanged?.Invoke(newState, oldState);
    }

}
