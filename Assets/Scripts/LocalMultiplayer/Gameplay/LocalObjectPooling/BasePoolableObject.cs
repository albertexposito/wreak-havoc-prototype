using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePoolableObject : MonoBehaviour
{

    private bool _enabled;

    private BaseObjectPool _pool;

    public void Initialize(BaseObjectPool pool)
    {
        _pool = pool;
    }

    public bool IsAvailable()
    {
        return !_enabled;
    }

    public void Spawn()
    {
        _enabled = true;
    }

    public void Despawn()
    {
        _enabled = false;
        _pool.DespawnObject(this);
    }

}
