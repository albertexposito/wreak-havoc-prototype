using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCharacterState
{
    protected BaseCharacterStateMachine _stateMachine;

    public string StateName { get => _stateName; }
    protected string _stateName;

    // Timespan that the state started
    private float _startTime;
    // private BasePlayerCharacter baseCharacter;

    public float StateTime { get => Time.time - _startTime; }

    protected BaseCharacterState(BaseCharacterStateMachine stateMachine, BasePlayerCharacter baseCharacter)
    {
        _stateMachine = stateMachine;
    }

    public virtual void OnStateEnter()
    {
        _startTime = Time.time;
    }

    public virtual void UpdateStateLogic()
    {

    }

    public virtual void OnDamageTaken(DamageData damageData)
    {

    }

    public virtual void OnStateExit()
    {
    
    }

}
