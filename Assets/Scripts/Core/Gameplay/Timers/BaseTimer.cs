using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Abstract base Timer used for gameplay elements
/// </summary>
public abstract class BaseTimer
{

    public abstract bool IsRunning();

    public abstract bool Expired();

    public abstract bool ExpiredOrNotRunning();

    public abstract float RemainingTime();

    public abstract void StartTimer(float time);

    public abstract void ResetTimer();

}
