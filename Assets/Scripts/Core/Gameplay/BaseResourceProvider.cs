using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseResourceProvider : ScriptableObject
{
    public Game.GameType GameType { get => _gameType; }
    [SerializeField] private Game.GameType _gameType;

    public abstract BaseTimer GetTimer();

}
