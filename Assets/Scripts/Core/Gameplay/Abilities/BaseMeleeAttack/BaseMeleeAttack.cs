using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;

public class BaseMeleeAttack : MonoBehaviour
{

    private enum AttackState
    {
        NOT_ATTACKING = 0,
        ATTACKING = 1,
        COMBO_WINDOW = 2
    }

    public bool Debugging = true;

    public UnityEvent<BaseMeleeAttack> OnAttackStarted; 
    public UnityEvent<DamageData> OnDamageDealt; 

    [SerializeField] private Transform _attackOverlapBoxOrigin;

    public float AttackTime = 0.35f; // in seconds
    public float AttackSpeed = 1;    // Coeficient
    // TODO ACTIVE HITBOX TIME

    [Range(0, 1)]
    public float ComboWindowPerc = 0.5f;

    [Range(0, 1)]
    public float HitboxActivationPerc = 0.5f;

    public float CurrentAttackTime { get; private set; } = 0;
    public float TotalAttackTime { get; private set; } = 0;

    private bool _attacking = false;
    private bool _hitboxActivated = false;
    private bool _activeComboWindow = false;
    private bool _comboFollowup = false;

    // Damage properties
    public float attackRangeX = 1;
    public float attackRangeY = 1;

    public LayerMask attackLayer;

    private BasePlayerCharacter _character;
    private bool _initialized;

    public void Initialize(BasePlayerCharacter character)
    {
        if (_initialized)
            return;

        _initialized = true;
        _character = character;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (_attacking)
        {
            CurrentAttackTime += Time.fixedDeltaTime;

            //Debug.Log($"[Melee Attack] - {CurrentAttackTime} | Hitbox activated");


            if (CurrentAttackTime >= TotalAttackTime * HitboxActivationPerc && !_hitboxActivated)
            {
                _hitboxActivated = true;
                CheckAttackHits();
                //Debug.Log($"[Melee Attack] - {CurrentAttackTime} | Hitbox activated");

            }

            if (!_activeComboWindow && CurrentAttackTime >= TotalAttackTime - TotalAttackTime * (1 - ComboWindowPerc))
            {
                _activeComboWindow = true;
                //Debug.Log($"[Melee Attack] - {CurrentAttackTime} | Combo Window activated");
            }

            if (CurrentAttackTime >= TotalAttackTime)
            {
                _hitboxActivated = false;

                if (_comboFollowup)
                    StartAttack();
                else
                    EndAttack();

                //Debug.Log($"[Melee Attack] - {CurrentAttackTime} | Attack ended");
            }
        }
    }

    public void TryPerformAttack()
    {
        if (!_attacking)
        {
            StartAttack();
        }

        if (_activeComboWindow && !_comboFollowup)
        {
            _comboFollowup = true;
        }

    }

    private void StartAttack()
    {

        _attacking = true;
        _activeComboWindow = false;
        _comboFollowup = false;

        CurrentAttackTime = 0;
        TotalAttackTime = AttackTime / AttackSpeed;
        OnAttackStarted.Invoke(this);


        //Debug.Log($"[Melee Attack] - {CurrentAttackTime} | Attack started");

    }

    private void CheckAttackHits()
    {

        Collider[] hits = Physics.OverlapBox(GetOverlapBoxCenterPoint(), GetOverlapBoxHalfExtents(), transform.rotation);

        foreach(Collider hitCollider in hits)
            if(hitCollider.TryGetComponent(out DamageableElement damageable))
            {
                if (damageable.gameObject == _character.gameObject)
                    continue;

                Vector3 directionVector = Utils.GetXZDirectionVector(transform.position, hitCollider.transform.position);

                DamageData damageData = new DamageData(
                    DamageType.MELEE,
                    4, 40,
                    // hitCollider.ClosestPoint(_character.transform.position) + Vector3.one, // THIS SHOULD WORK BUT DOESN'T
                    hitCollider.transform.position + directionVector * 0.5f,
                    directionVector,
                    _character,
                    damageable
                );
                
                damageable.DealDamage(damageData);
                OnDamageDealt?.Invoke(damageData);

            }
        

    }

    private Vector3 GetOverlapBoxCenterPoint()
    {
        Vector3 overlapBoxOrigin = transform.position;

        if (_attackOverlapBoxOrigin != null)
            overlapBoxOrigin = _attackOverlapBoxOrigin.position;

        return overlapBoxOrigin;
    }

    private Vector3 GetOverlapBoxHalfExtents() => new Vector3(attackRangeX / 2, 0.5f, attackRangeY / 2);

    public void EndAttack()
    {
        _activeComboWindow = false;
        _attacking = false;
        _hitboxActivated = false;
    }


    [SerializeField] private Collider _testingCollider;
    private void OnDrawGizmos()
    {
        if(Debugging)
            Gizmos.DrawCube(
                GetOverlapBoxCenterPoint(),
                GetOverlapBoxHalfExtents() * 2
            );


        //if (_testingCollider != null)
        //{

        //    Vector3 position = _testingCollider.ClosestPoint(_character.transform.position);

        //    Gizmos.DrawSphere( position, 0.3f);

        //}
    }




}
