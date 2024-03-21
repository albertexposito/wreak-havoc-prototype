using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameInitializer
{

    public event Action<StageComponents> OnLevelLoaded;
    public event Action<List<IPlayerIdentity>> OnPlayersSpawned;

}
