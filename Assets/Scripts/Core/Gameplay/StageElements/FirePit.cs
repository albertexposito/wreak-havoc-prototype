using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(OnDamageableTrigger))]
[RequireComponent(typeof(Collider))]
public class FirePit : MonoBehaviour
{

    [SerializeField] private int _damage = 3;
    [SerializeField] private float _damageTime = 1;

    [SerializeField] private ParticleSystem _fireParticleSystem;
    [SerializeField] private Collider _collider;

    private bool _firePitEnabled;
    private Dictionary<DamageableElement, float> _elements;
    private OnDamageableTrigger _triggerEnter;

    private void Awake()
    {
        _elements = new Dictionary<DamageableElement, float>(4);

        _triggerEnter = GetComponent<OnDamageableTrigger>();

        _triggerEnter.OnDamageableEnterTrigger.AddListener(OnDamageableEnter);
        _triggerEnter.OnDamageableExitTrigger.AddListener(OnDamageableExit);

        ActivateFirePit();

    }

    private void FixedUpdate()
    {
        if (_firePitEnabled)
        {
            // Foreach element in the dictionary
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
        Debug.Log($"[FirePit] - FirePit enabled!");

        _firePitEnabled = true;

        _fireParticleSystem.gameObject.SetActive( true );
        _fireParticleSystem.Play();
        _collider.enabled = true;
    }

    public void DeactivateFirePit()
    {
        Debug.Log($"[FirePit] - FirePit disabled!");


        _firePitEnabled = false;

        _fireParticleSystem.Stop();
        _collider.enabled = false;
        _elements.Clear();
    }

    private void OnDamageableEnter(DamageableElement damageable)
    {
        Debug.Log($"[FirePit] - DamageableEnter, name: {damageable.gameObject.name}");

        //DealDamageToElement(damageable);
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
