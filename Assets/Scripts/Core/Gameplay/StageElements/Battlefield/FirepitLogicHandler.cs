using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirepitLogicHandler : MonoBehaviour
{

    [SerializeField] private FirePit[] _firePits;

    private Dictionary<FirePit, Coroutine> _firePitCoroutinesDictionary;

    [SerializeField] private AnimationCurve _timeSpeedCurve;
    private WaitUntil _waitUntilInstruction;

    private float _targetTime;

    private void Awake()
    {

        _firePits = GetComponentsInChildren<FirePit>();


        _firePitCoroutinesDictionary = new Dictionary<FirePit, Coroutine>(_firePits.Length);


        _waitUntilInstruction = new WaitUntil(() => Time.time >= _targetTime);





    }

    private void Start()
    {

        DisableAndInitializeAllFirePits();

        StartCoroutine(FirePitCycleStart());
    }

    private void DisableAndInitializeAllFirePits()
    {
        foreach (FirePit firePit in _firePits)
        {
            firePit.DeactivateFirePit();

            _firePitCoroutinesDictionary.Add(firePit, default);
        }
    }

    private IEnumerator FirePitCycleStart()
    {

        while (true) // TODO: Control this, dont use a while (true)
        {
            yield return CircularCycle();
            yield return AlternateBetweenEvenAndUnevenFirePits();
            yield return ActivateAllFirePits();
            yield return CircularCycle(false);
            yield return AlternateBetweenEvenAndUnevenFirePits();
            yield return ActivateAllFirePits();
        }

    }


    private IEnumerator CircularCycle(bool clockwise = true)
    {
        //Debug.Log("[Battlefield][FirepitLogicHandler] - CircularCycle Begin!");

        int index;
        float waitTime = 1.25f;
        float activeFirePitTime = 6;

        for(int i = 0; i < _firePits.Length; i++)
        {
            if (clockwise)
                index = i;
            else
                index = (_firePits.Length - i) % _firePits.Length;

            //Debug.Log($"[Battlefield][FirepitLogicHandler] - CircularCycle FirePit with index {index} activated!");

            ActivateFirePitForNSeconds(_firePits[index], activeFirePitTime);
            yield return SetCounter(waitTime);
        }

        yield return null;

    }

    private IEnumerator AlternateBetweenEvenAndUnevenFirePits(int repetitions = 3)
    {

        //Debug.LogError("[Battlefield][FirepitLogicHandler] - Even/Uneven Begin!");

        int currentRepetition = 0;
        float cycleTime = 5;

        while (currentRepetition < repetitions)
        {
            // EVENS
            for (int i = 0; i < _firePits.Length; i++)
                if(i % 2 == 0)
                    ActivateFirePit(_firePits[i]);
                else
                    _firePits[i].DeactivateFirePit();

            yield return SetCounter(cycleTime);

            // UNEVENS
            for (int i = 0; i < _firePits.Length; i++)
                if (i % 2 == 1)
                    ActivateFirePit(_firePits[i]);
                else
                    _firePits[i].DeactivateFirePit();

            yield return SetCounter(cycleTime);

            currentRepetition++;
        }

        

    }

    private IEnumerator ActivateAllFirePits()
    {
        //Debug.LogError("[Battlefield][FirepitLogicHandler] - All begin Begin!");

        foreach (FirePit firePit in _firePits)
            ActivateFirePit(firePit);

        yield return SetCounter(6);

        foreach (FirePit firePit in _firePits)
            firePit.DeactivateFirePit();

    }

    private void ActivateFirePit(FirePit firePit)
    {
        if (_firePitCoroutinesDictionary[firePit] != default)
            StopCoroutine(_firePitCoroutinesDictionary[firePit]);

        firePit.ActivateFirePit();
    }

    private void ActivateFirePitForNSeconds(FirePit firePit, float activateTime)
    {
        if (_firePitCoroutinesDictionary[firePit] != default)
            StopCoroutine(_firePitCoroutinesDictionary[firePit]);

        _firePitCoroutinesDictionary[firePit] = StartCoroutine(ActivateFirePitForNSecondsCoroutine(firePit, activateTime));
    }

    private IEnumerator ActivateFirePitForNSecondsCoroutine(FirePit firePit, float activateTime)
    {
        firePit.ActivateFirePit();
        yield return new WaitForSeconds(activateTime);
        firePit.DeactivateFirePit();
    }

    private IEnumerator SetCounter(float unscaledWaitTime)
    {
        float scaledWaitTime = unscaledWaitTime * _timeSpeedCurve.Evaluate(Time.timeSinceLevelLoad); // TODO have a way to acces a property GameTime
        _targetTime = Time.time + scaledWaitTime;

        yield return _waitUntilInstruction;

    }

}
