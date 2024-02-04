using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAbility : MonoBehaviour
{

    private BaseCharacterController _charController;
    private LocalPlayerGameplayInputHandler _inputHandler;

    //private BasePlayerCharacter _character;
    private bool _initialized;

    [SerializeField] private float _dashSpeed = 30;

    [SerializeField] private float _dashTime = 0.2f;
    private float _dashCooldownTimer;


    // Cooldown
    [SerializeField] private int _maxDashes = 3;
    private int _currentAvailableDashes;
    [SerializeField] private float _dashRechargeTime = 2;
    private float _dashRechargeTimer;

    private bool _dashing;

    public void Initialize(BasePlayerCharacter character)
    {
        if (_initialized)
            return;

        _initialized = true;

        _charController = character.CharacterController;
        _inputHandler = character.PlayerInputHandler;

        _currentAvailableDashes = _maxDashes;
    }

    private void FixedUpdate()
    {
        RechargeTimerUpdate();
        DashCooldownTimerUpdate();
    }

    public virtual void TryPerformAbility()
    {
        if (_currentAvailableDashes > 0 && _dashCooldownTimer <= 0)
        {
            Vector3 movementInput = _inputHandler.GameplayInputData.movementInput;
            Dash(movementInput);
        }
    }

    // TODO: Consider updating this through a character state?
    private void Dash(Vector3 movementDirection)
    {
        Vector3 movementDirectionXZ = Utils.GetXZVectorFromInputVector(movementDirection);

        if (movementDirectionXZ.magnitude < 0.1)
            movementDirectionXZ = _charController.CharacterForwardDirection;

        _charController.SetVelocity(movementDirectionXZ * _dashSpeed);

        // Setting up cooldowns
        _currentAvailableDashes--;
        _dashCooldownTimer = _dashTime;

    }

    private void RechargeTimerUpdate()
    {

        if (_currentAvailableDashes >= _maxDashes)
            return;

        _dashRechargeTimer += Time.fixedDeltaTime;

        if(_dashRechargeTimer >= _dashRechargeTime)
        {
            _currentAvailableDashes++;

            if (_currentAvailableDashes == _maxDashes)
                _dashRechargeTimer = 0;
            else
                _dashRechargeTimer -= _dashRechargeTime;

        }
        
    }

    private void DashCooldownTimerUpdate()
    {
        if (_dashCooldownTimer <= 0)
            return;

        _dashCooldownTimer -= Time.fixedDeltaTime;

        if (_dashCooldownTimer <= 0)
            _dashCooldownTimer = 0;
    }

}
