using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameLocalTimerTester : MonoBehaviour
{

    private GameManager _gameManager;
    private BaseTimer _countdownTimer;
    private BaseTimer _gameTimer;

    private void Start()
    {
        _gameManager = GameManager.Instance;
        //_countdownTimer = _gameManager.CountdownTimer;
        //_gameTimer = _gameManager.GameTimer;
    }

    private void Update()
    {
        if (_countdownTimer.IsRunning() && !_countdownTimer.Expired())
        {
            float countdownValue = Mathf.Ceil(_countdownTimer.RemainingTime());
            Debug.Log($"Game Starts in: {countdownValue}");
        }

        if (_gameTimer.IsRunning() && !_gameTimer.Expired())
        {
            float countdownValue = Mathf.Ceil(_gameTimer.RemainingTime());
            Debug.Log($"Game Finishes in: {countdownValue}");
        }
    }
}
