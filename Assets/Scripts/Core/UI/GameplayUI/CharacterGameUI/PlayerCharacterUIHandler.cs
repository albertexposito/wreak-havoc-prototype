using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterUIHandler : MonoBehaviour
{

    private PlayerCharacterUI[] _characterUIArray;
    private LocalGameInitializer _gameInitializer;

    private void Awake()
    {
        _characterUIArray = GetComponentsInChildren<PlayerCharacterUI>();

        _gameInitializer = FindObjectOfType<LocalGameInitializer>();
        _gameInitializer.OnPlayersSpawned += OnPlayerCharactersSpawned;
    }

    private void OnPlayerCharactersSpawned(List<IPlayerIdentity> players)
    {
        
        for(int i = 0; i < _characterUIArray.Length; i++)
        {
            if(i <= players.Count - 1)
                _characterUIArray[i].SetPlayer(players[i]);
            else
                _characterUIArray[i].HidePlayerUI();
        }

    }

    private void OnDestroy()
    {
        _gameInitializer.OnPlayersSpawned -= OnPlayerCharactersSpawned;

    }
}
