using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalGameTestingUiInitializer : MonoBehaviour
{

    [SerializeField] LocalGameInitializer _gameInitializer;
    private StateTestingUI _stateTestingUI;

    private void Awake()
    {
        _stateTestingUI = GetComponent<StateTestingUI>();
        _gameInitializer.OnPlayersSpawned += AddCharacters;
    }

    private void AddCharacters(List<Player> players)
    {
        foreach (Player player in players)
            _stateTestingUI.AddCharacter(player.CurrentCharacter);   
    }

    private void OnDestroy()
    {
        _gameInitializer.OnPlayersSpawned -= AddCharacters;

    }
}
