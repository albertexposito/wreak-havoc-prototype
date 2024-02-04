using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalTimer : BaseTimer
{

    protected float _waitTime;
    protected float _startTime = -1;

    protected bool _isRunning;

    public override void StartTimer(float time)
    {
        _isRunning = true;
        _waitTime = time;
        _startTime = Time.fixedTime;
    }

    public override bool Expired()
    {
        return Time.time - _startTime > _waitTime + Time.fixedDeltaTime;
    }

    public override bool ExpiredOrNotRunning()
    {
        return !_isRunning || Expired();
    }

    public override float RemainingTime()
    {
        float remainingTime = _waitTime - (Time.time - _startTime);

        if(remainingTime < 0)
            remainingTime = 0;

        return remainingTime;
    }

    public override void ResetTimer()
    {
        _startTime = -1;
        _isRunning = false;
    }

    public override bool IsRunning()
    {
        return _isRunning;
    }
}
