using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterNeutralState : BaseCharacterState
{

    private LocalPlayerGameplayInputHandler _inputHandler;
    private HealthHandler _healthHandler;
    private BaseCharacterController _charController;

    private BaseRangedAttack _rangedAttack;
    private BaseMeleeAttack _meleeAttack;
    private DashAbility _dashAbility;

    public CharacterNeutralState(BaseCharacterStateMachine stateMachine, BasePlayerCharacter baseCharacter) : base(stateMachine, baseCharacter)
    {

        _stateName = "NEUTRAL";

        _healthHandler = baseCharacter.HealthHandler;
        _inputHandler = baseCharacter.PlayerInputHandler;
        _charController = baseCharacter.CharacterController;
        _rangedAttack = baseCharacter.RangedAttackController;
        _meleeAttack = baseCharacter.MeleeAttackController;
        _dashAbility = baseCharacter.DashAbility;
    }

    public override void OnStateEnter()
    {
        base.OnStateEnter();

    }

    public override void UpdateStateLogic()
    {

        LocalPlayerGameplayInputData inputData = _inputHandler.GameplayInputData;

        _charController.PerformMovement(inputData.movementInput, inputData.rotationInput);

        if (inputData.DashInput)
            _dashAbility.TryPerformAbility();

        if (inputData.FireInput)
            _meleeAttack.TryPerformAttack();

        if (inputData.SpecialInput)
            _rangedAttack.PerformAttack();
        
    }

    public override void OnDamageTaken(DamageData damageData)
    {

        Debug.Log($"{_stateMachine.gameObject.name} received damage of: {damageData.damageAmount}, hitDirection: {damageData.hitdirection}");

        Vector3 knockbackForce = damageData.hitdirection * damageData.knockbackForce;

        if (Utils.GetXZMagnitudeFromVector(knockbackForce) > 1)
        {
            _charController.SetVelocity(knockbackForce);
            _stateMachine.ChangeState(_stateMachine.HitStunnedState);
        }

        _healthHandler.DealDamage(damageData);
    }

}
