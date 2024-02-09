using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(OnDamageableTriggerEnter))]
[RequireComponent(typeof(Collider))]
public class FirePit : MonoBehaviour
{

    [SerializeField] private int _damage = 3;
    [SerializeField] private float _damageTime = 1;

    [SerializeField] private ParticleSystem _fireParticleSystem;
    [SerializeField] private Collider _collider;

    private bool _firePitEnabled;
    private Dictionary<DamageableElement, float> _elements;
    private OnDamageableTriggerEnter _triggerEnter;

    private void Awake()
    {
        _elements = new Dictionary<DamageableElement, float>(4);

        _triggerEnter = GetComponent<OnDamageableTriggerEnter>();

        _triggerEnter.OnDamageableEnterTrigger.AddListener(OnDamageableEnter);
        _triggerEnter.OnDamageableExitTrigger.AddListener(OnDamageableExit);

        ActivateFirePit();

    }

    private void FixedUpdate()
    {
        if (_firePitEnabled)
        {
            //foreach (DamageableElement damageable in _elements.Keys)
            for(int i = _elements.Keys.Count - 1; i >= 0; i--)
            {
                DamageableElement damageable = _elements.Keys.ElementAt(i);
                _elements[damageable] -= Time.fixedDeltaTime;

                if(_elements[damageable] <= 0)
                {
                    _elements[damageable] = _damageTime;
                    DealDamageToElement(damageable);
                }
            }
        }

    }

    public void ActivateFirePit()
    {
        _firePitEnabled = true;

        _fireParticleSystem.Play();
        _collider.enabled = true;
    }

    public void DeactivateFirePit()
    {
        _firePitEnabled = false;

        _fireParticleSystem.Stop();
        _collider.enabled = false;
        _elements.Clear();
    }

    private void OnDamageableEnter(DamageableElement damageable)
    {
        Debug.Log($"[FirePit] - DamageableEnter, name: {damageable.gameObject.name}");

        DealDamageToElement(damageable);
        _elements.Add(damageable, _damageTime);
    }
    
    private void OnDamageableExit(DamageableElement damageable)
    {
        _elements.Remove(damageable);
    }

    private void DealDamageToElement(DamageableElement damageable)
    {
        DamageData damageData = new DamageData(DamageType.ENVIRONMENT, _damage, 0, damageable.transform.position, Vector3.zero, null, damageable);
        damageable.DealDamage(damageData);
    }

}
