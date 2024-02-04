using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseProjectileLogic : MonoBehaviour
{

    public Action OnProjectileDestroyed;

    public abstract void OnProjectileSpawned(BasePlayerCharacter instigator, Rigidbody rb, Vector3 direction);

    public abstract void UpdateProjectileLogic(float deltaTime);

    public abstract void DestroyProjectile();

}
