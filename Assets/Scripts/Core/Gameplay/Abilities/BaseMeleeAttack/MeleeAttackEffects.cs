using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

public class MeleeAttackEffects : MonoBehaviour
{


    [SerializeField] private ParticleSystem _hitEffect;

    [SerializeField] private ParticleSystem _swordTrail;

    private float _previousTrailTime = -1;
    [SerializeField] private float TRAIL_COMBO_TIME = 0.5f;

    private Vector3 _currentScale = Vector3.one;


    private void Awake()
    {
        BaseMeleeAttack meleeAttack = GetComponent<BaseMeleeAttack>();

        meleeAttack.OnAttackStarted.AddListener(ActivateTrail);
        meleeAttack.OnDamageDealt.AddListener(PlayHitEffect);
    }

    private void Start()
    {
        // Do this so the position of the effect is not tied to the player
        _hitEffect.transform.SetParent(null);
    }

    public void ActivateTrail(BaseMeleeAttack meleeAttack)
    {

        if (Time.time - _previousTrailTime <= TRAIL_COMBO_TIME)
            _currentScale.x *= -1;
        else
            _currentScale.x = 1;

        transform.localScale = _currentScale;

        _previousTrailTime = Time.time;

        _swordTrail.Clear();
        _swordTrail.Play();

    }

    private void PlayHitEffect(DamageData damageData)
    {
        Debug.Log("[MeleeAttackEffects] - Playing hit effect!");

        _hitEffect.transform.position = damageData.receiver.transform.position + damageData.hitdirection + Vector3.up;

        _hitEffect.gameObject.SetActive(true);
        _hitEffect.Clear();
        _hitEffect.Play();


    }



    // TRAIL TEST
    //[SerializeField] private float _particleCounterTime = 0.2f;
    //private float _counter;

    //private void FixedUpdate()
    //{
    //    _counter += Time.fixedDeltaTime;

    //    _particleCounterTime = Mathf.Clamp(_particleCounterTime, 0.1f, 20);

    //    if (_counter > _particleCounterTime)
    //    {
    //        ActivateTrail();

    //        _counter = 0;
    //    }
    //}

}
