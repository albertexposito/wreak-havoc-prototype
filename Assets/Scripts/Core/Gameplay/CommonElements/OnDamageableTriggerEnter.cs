using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnDamageableTriggerEnter : MonoBehaviour
{

    public UnityEvent<DamageableElement> OnDamageableEnterTrigger;
    public UnityEvent<DamageableElement> OnDamageableExitTrigger;


    private void OnTriggerEnter(Collider other)
    {

        DamageableElement damageable = other.GetComponent<DamageableElement>();

        if (damageable != null)
            OnDamageableEnterTrigger?.Invoke(damageable);

    }

    
    private void OnTriggerExit(Collider other)
    {
        /// ON TRIGGER EXIT IS NOT CALLED BY DEFAULT IF THE GAME OBJECT IS DISABLED
        /// WHILE ON TRIGGER. CAREFUL !!!

        DamageableElement damageable = other.GetComponent<DamageableElement>();

        if (damageable != null)
            OnDamageableExitTrigger?.Invoke(damageable);

    }


}
