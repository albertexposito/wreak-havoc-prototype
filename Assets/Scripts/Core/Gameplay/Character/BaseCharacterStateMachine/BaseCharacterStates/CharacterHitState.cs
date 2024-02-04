using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHitState : BaseCharacterState
{

    private BaseCharacterController _charController;
    private HealthHandler _healthHandler;

    public CharacterHitState(BaseCharacterStateMachine stateMachine, BasePlayerCharacter baseCharacter) : base(stateMachine, baseCharacter)
    {
        _stateName = "HITSTUNNED";

        _charController = baseCharacter.CharacterController;
        _healthHandler = baseCharacter.HealthHandler;
    }

    public override void UpdateStateLogic()
    {

        _charController.PerformMovement(Vector2.zero, Vector2.zero);

        if(_charController.CurrentSpeed <= _charController.MaxSpeed / 2)
            _stateMachine.ChangeState(_stateMachine.NeutralState);

    }

    public override void OnDamageTaken(DamageData damageData)
    {

        Debug.Log($"{_stateMachine.gameObject.name} received damage of: {damageData.damageAmount}, hitDirection: {damageData.hitdirection}");

        Vector3 knockbackForce = damageData.hitdirection * damageData.knockbackForce;
        
        if(Utils.GetXZMagnitudeFromVector(knockbackForce) > 1)
        {
            _charController.SetVelocity(knockbackForce);
            _stateMachine.ChangeState(_stateMachine.HitStunnedState);
        }

        _healthHandler.DealDamage(damageData);


    }

}
