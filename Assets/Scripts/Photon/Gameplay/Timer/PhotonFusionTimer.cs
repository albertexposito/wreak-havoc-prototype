using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonFusionTimer : BaseTimer
{

    protected TickTimer _tickTimer;
    protected NetworkRunner _runner;

    public PhotonFusionTimer(NetworkRunner runner) : base()
    {
        _runner = runner;
    }

    public override bool Expired()
    {
        bool expired = _tickTimer.Expired(_runner);
        if (expired)
            ResetTimer();

        return expired;
    }

    public override bool ExpiredOrNotRunning()
    {
        return _tickTimer.Expired(_runner);
    }

    public override bool IsRunning()
    {
        return _tickTimer.IsRunning;
    }

    public override float RemainingTime()
    {
        return _tickTimer.RemainingTime(_runner) ?? -1;
    }

    public override void ResetTimer()
    {
        _tickTimer = TickTimer.None;
    }

    public override void StartTimer(float time)
    {
        _tickTimer = TickTimer.CreateFromSeconds(_runner, time);
    }

}
