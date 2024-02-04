using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BaseProjectile : MonoBehaviour
{

    private BaseProjectileLogic _projectileLogic;
    protected BasePoolableObject _poolableObject;

    protected Rigidbody _rb;
    protected Collider _collider;


    // Player that shot the projectile
    //protected BasePlayerCharacter _instigator;
    //protected Vector3 _direction;

    //[Header("Base properties")]
    //[SerializeField] protected float _projectileSpeed = 15; // units per second
    //[SerializeField] protected float _lifeTime = 4; // seconds
    //private float _remainingTime;


    //[SerializeField] private Transform _detectPosition;

    //[SerializeField] private GameObject _projectileModel;

    //private bool _active;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _projectileLogic = GetComponentInChildren<BaseProjectileLogic>();
        _poolableObject = GetComponent<BasePoolableObject>();
        _collider = GetComponent<Collider>();
        //_remainingTime = _lifeTime;

        _projectileLogic.OnProjectileDestroyed += Despawn;

    }

    public virtual void OnSpawn(Vector3 position, Quaternion rotation, BasePlayerCharacter instigator, Vector3 direction)
    {
        //_instigator = instigator;
        //_direction = direction;
        gameObject.SetActive(true);


        _rb.position = position;
        _rb.rotation = rotation;

        _collider.enabled = true;

        _poolableObject.Spawn();
        _projectileLogic.OnProjectileSpawned(instigator, _rb, direction);
        //StartCoroutine(EnableThingsAfterPhysicsUpdate(position,rotation));
    }

    //private IEnumerator EnableThingsAfterPhysicsUpdate(Vector3 position, Quaternion rotation)
    //{
    //    yield return new WaitForFixedUpdate();



    //}

    public virtual void Despawn()
    {
        // TODO Pool it!
        // Destroy(gameObject);
        _collider.enabled = false;
        _poolableObject.Despawn();
        gameObject.SetActive(false);
    }

    public virtual void FixedUpdate()
    {
        /*
        if (!_active)
            return;

        _rb.velocity = _direction * _projectileSpeed;

        DetectCollision();

        _remainingTime -= Time.fixedDeltaTime;

        if (_remainingTime <= 0)
            Despawn();
        */

        if(_projectileLogic.gameObject.activeSelf)
            _projectileLogic.UpdateProjectileLogic(Time.fixedDeltaTime);

    }

    private void OnDestroy()
    {
        _projectileLogic.OnProjectileDestroyed -= Despawn;

    }

    //protected virtual void DetectCollision()
    //{
    //    Collider[] hits = Physics.OverlapSphere(_detectPosition.position, 0.5f);

    //    if (hits.Length > 0)
    //        OnProjectileHit(hits);


    //}

    //protected virtual void OnProjectileHit(Collider[] collider)
    //{
    //    Collider[] hits = Physics.OverlapSphere(_detectPosition.position, 3f);

    //    foreach (Collider hit in hits)
    //    {
    //        if(hit.TryGetComponent(out DamageableElement damageable))
    //        {
    //            Debug.Log($"{damageable.gameObject.name} has a DamageableElement");

    //            DamageData damageData = new DamageData(
    //                2, // DAMAGE
    //                15, // KNOCKBACK
    //                Utils.GetXZDirectionVector(_detectPosition.position, damageable.transform.position),
    //                _instigator
    //            );

    //            damageable.DealDamage(damageData);
    //        }
    //    }

    //    DisableProjectile();
    //}


    //public virtual void DisableProjectile()
    //{
    //    //_active = false;
    //    //_projectileModel.SetActive(false);

    //    Invoke(nameof(Despawn), 3);
    //}



}
