using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LocalTimerTester : MonoBehaviour
{
    #region UI

    public TMP_Text _timerText;
    public TMP_InputField _waitTimeInput;
    public Button _startTimer;
    public Button _stopTimer;

    #endregion UI
    private BaseTimer _timer;


    private void Awake()
    {
        _timer = new LocalTimer();
    }

    public void StartTimer()
    {
        if (float.TryParse(_waitTimeInput.text, out float time))
        {
            Debug.Log("Time starts!");
            _timer.StartTimer(time);
        }
        else
            Debug.LogError($"Invalid input! Set a valid float number | {_waitTimeInput.text}");
    }

    public void StopTimer()
    {
        _timer.ResetTimer();
    }

    private void Update()
    {
        if(_timer.IsRunning() && !_timer.Expired())
        {
            _timerText.text = Mathf.Ceil(_timer.RemainingTime()).ToString();
        }
    }

}
