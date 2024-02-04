using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPlane : MonoBehaviour
{


    private DamageData _damageData;

    private void Awake()
    {
        _damageData = new DamageData();
        _damageData.damageAmount = 999;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out DamageableElement damageable))
        {
            damageable.DealDamage(_damageData);
        }
    }


}
