using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIRocketSpawner : MonoBehaviour
{
    private BaseRangedAttack _rangedAttack;

    private YieldInstruction _waitForSeconds;

    private void Awake()
    {
        _waitForSeconds = new WaitForSeconds(3);

        BasePlayerCharacter _aiCharacter = GetComponent<BasePlayerCharacter>();
        _rangedAttack = _aiCharacter.RangedAttackController;
    }

    private void Start()
    {
        StartCoroutine(SpawnRockets());   
    }


    private IEnumerator SpawnRockets()
    {
        while (gameObject.activeSelf)
        {
            _rangedAttack.PerformAttack();

            yield return _waitForSeconds;
        }
    }

}
