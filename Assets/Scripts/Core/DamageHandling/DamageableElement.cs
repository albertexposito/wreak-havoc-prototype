using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DamageableElement : MonoBehaviour
{

    public UnityEvent<DamageData> OnDamageTaken;
    public UnityEvent OnEntityKilledInstantly;

    private void Awake()
    {
        OnDamageTaken = new UnityEvent<DamageData>();
    }

    public void DealDamage(DamageData damageData)
    {
        OnDamageTaken?.Invoke(damageData);
    }

}
