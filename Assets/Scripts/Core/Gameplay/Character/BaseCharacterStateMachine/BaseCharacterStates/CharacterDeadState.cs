using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDeadState : BaseCharacterState
{

    private BaseCharacterController _characterController;
    private CharacterModelHandler _charModelHandler;

    public CharacterDeadState(BaseCharacterStateMachine stateMachine, BasePlayerCharacter baseCharacter) : base(stateMachine, baseCharacter)
    {
        _stateName = "DEAD";

        _characterController = baseCharacter.CharacterController;
        _charModelHandler = baseCharacter.CharacterModelHandler;
    }

    public override void OnStateEnter()
    {
        base.OnStateEnter();

        _characterController.DisableCharacterController();
        _charModelHandler.DisableCharacterVisuals();
    }


    public override void OnStateExit()
    {
        base.OnStateExit();

        _characterController.EnableCharacterController();
        _charModelHandler.EnableCharacterVisuals();
    }
}
