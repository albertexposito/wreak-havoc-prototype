using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseRangedAttack : MonoBehaviour
{

    [SerializeField] private BaseProjectile _projectile;

    [SerializeField] private Transform _spawnPosition;

    private BasePlayerCharacter _character;
    private BaseObjectPool _projectilePool;
    
    private bool _initialized;

    private List<BaseProjectile> _currentProjectiles;

    [SerializeField] private float _cooldown = 0.35f;
    private float _remainingCooldown;

    public void Initialize(BasePlayerCharacter character)
    {
        if (_initialized)
            return;

        _initialized = true;
        _character = character;

        _currentProjectiles = new List<BaseProjectile>();
        _projectilePool = GetComponent<BaseObjectPool>();

        _projectilePool.InitializePool(_projectile.gameObject);
    }

    private void FixedUpdate()
    {
        if (_remainingCooldown >= 0)
            _remainingCooldown -= Time.fixedDeltaTime;
    }


    public virtual void PerformAttack()
    {
        if (_remainingCooldown >= 0)
            return;


        _remainingCooldown = _cooldown;

        //TODO: Get projectile from object pool
        //BaseProjectile projectile = Instantiate(_projectile, _spawnPosition.position, _spawnPosition.rotation);
        BasePoolableObject poolableProjectile = _projectilePool.GetAvailableObject();

        BaseProjectile projectile = poolableProjectile.GetComponent<BaseProjectile>();

        projectile.OnSpawn(_spawnPosition.position, _spawnPosition.rotation, _character, _spawnPosition.forward);
        
        _currentProjectiles.Add(projectile);

    }


}
