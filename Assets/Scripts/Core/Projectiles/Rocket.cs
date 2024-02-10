using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : BaseProjectileLogic
{

    protected BasePlayerCharacter _instigator;
    protected Vector3 _direction;
    protected Rigidbody _rb;

    [Header("Base properties")]
    [SerializeField] private Transform _detectPosition;
    [SerializeField] private DamageableElement _damageableElement;

    [SerializeField] protected float _projectileInitialSpeed = 5; // units per second
    [SerializeField] protected float _projectileAcceleration = 15; // units per second 2
    private float _projectileSpeed;

    [SerializeField] protected float _lifeTime = 2; // seconds
    private float _aliveTime;

    [SerializeField] protected LayerMask _hitLayermask = 2; // seconds
    
    [Header("Effects")]
    [SerializeField] protected ParticleSystem _rocketSmoke; // units per second
    [SerializeField] protected Transform _rocketSmokePosition; // units per second
    [SerializeField] protected ParticleSystem _explosion; // seconds

    private void Awake()
    {

        _damageableElement.OnDamageTaken.AddListener(OnDamageTaken);

        _rocketSmoke.transform.SetParent(null);
        _explosion.transform.SetParent(null);
    }

    public override void OnProjectileSpawned(BasePlayerCharacter instigator, Rigidbody rb, Vector3 direction)
    {
        _instigator = instigator;
        _direction = direction;

        gameObject.SetActive(true);

        _rb = rb;

        _rocketSmoke.gameObject.SetActive(true);
        _rocketSmoke.transform.SetParent(_rocketSmokePosition);
        _rocketSmoke.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);


        _rocketSmoke.Clear();
        _rocketSmoke.Play();
        
        _aliveTime = 0;

        _projectileSpeed = _projectileInitialSpeed;

    }

    public override void UpdateProjectileLogic(float deltaTime)
    {

        Vector3 velocity = _direction * _projectileSpeed;
        _rb.velocity = velocity;


        _projectileSpeed += _projectileAcceleration * deltaTime;

        DetectCollision();

        _aliveTime += Time.fixedDeltaTime;

        if (_aliveTime > _lifeTime)
            DestroyProjectile();

    }

    protected virtual void DetectCollision()
    {
        Collider[] hits = Physics.OverlapSphere(_detectPosition.position, 0.5f, _hitLayermask);

        if (AreHitsValid(hits))
            OnProjectileHit(hits);

    }

    private void OnDamageTaken(DamageData damageData)
    {
        if(damageData.damageType == DamageType.MELEE)
        {
            _direction *= -1;
            _rb.rotation = Quaternion.Inverse(_rb.rotation);
        }
        else if (damageData.damageType == DamageType.RANGED)
        {
            DestroyProjectile();
        }
    }

    protected virtual void OnProjectileHit(Collider[] collider)
    {
        bool hasHitAnEntity = false;

        Collider[] hits = Physics.OverlapSphere(_detectPosition.position, 3f);

        foreach (Collider hit in hits)
        {

            if (hit.gameObject == transform.parent.gameObject)
                continue;

            Debug.Log($"[Rocket] - Rocket has hit {hit.gameObject.name}");

            if (hit.TryGetComponent(out DamageableElement damageable))
            {

                //if (!MustTheRocketHitDamageable(damageable))
                //    continue;

                DamageData damageData = new DamageData(
                    DamageType.RANGED,
                    2, // DAMAGE
                    15, // KNOCKBACK
                    _detectPosition.position,
                    Utils.GetXZDirectionVector(_detectPosition.position, damageable.transform.position),
                    _instigator,
                    damageable
                );


                damageable.DealDamage(damageData);
                hasHitAnEntity = true;
            }
        }

        DestroyProjectile();
    }

    // This should be common functionality for a Projectile, not just the rocket
    // 
    // Use this to prevent the spawner from get hit by its own projectile right after spawning it
    private bool AreHitsValid(Collider[] hits)
    {
        if(hits.Length == 0)
            return false;

        bool validHit = false;

        foreach(Collider hit in hits)
        {

            validHit = hit.gameObject != transform.parent.gameObject;

            if (validHit)
                break;
        }

        return validHit;
    }

    // This should be common functionality for a Projectile, not just the rocket
    // Use this to prevent the spawner from get hit by its own projectile right after spawning it
    private bool MustTheRocketHitDamageable(DamageableElement damageable)
    {
        bool mustHitDamageable = false;

        if (_aliveTime > 0.25f)
            mustHitDamageable = true;

        if (!mustHitDamageable)
        {
            BasePlayerCharacter character = damageable.GetComponent<BasePlayerCharacter>();

            mustHitDamageable = character != null ?
                character != _instigator :
                true;
        }


        return mustHitDamageable;
    }

    public override void DestroyProjectile()
    {
        _rocketSmoke.Stop();

        _explosion.transform.SetPositionAndRotation(transform.position + _direction * -0.2f, transform.rotation);
        _explosion.gameObject.SetActive(true);
        _explosion.Play();

        _rocketSmoke.transform.SetParent(null);

        gameObject.SetActive(false);

        _rb.velocity = Vector3.zero;

        OnProjectileDestroyed?.Invoke();
    }


}
