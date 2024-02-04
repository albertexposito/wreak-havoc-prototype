using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


[CreateAssetMenu(fileName = "GAME MANAGER", menuName = "Managers/Game Manager")]

public class GameManager : ScriptableObjectSingleton<GameManager>, IManager
{

    public List<Player> GamePlayers { get => _gamePlayers; }
    private List<Player> _gamePlayers;

    public void InitManager()
    {
        InitializeSingleton();

        _gamePlayers = new List<Player>(4);
    }

}
