using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerIdentity
{
    // EVENTS
    public event Action<string> OnNameChanged;
    public event Action<bool> OnReadyStateChanged;
    public event Action<int, IPlayerIdentity> OnPlayerLivesChange;

    // GETTER PROPERTIES
    string PlayerName { get; }
    int PlayerIndex { get; }
    bool IsReady { get; }
    int CurrentLives { get; }


    // METHODS
    BasePlayerCharacter CurrentCharacter { get; }
    public void SetCharacter(BasePlayerCharacter character);

}
