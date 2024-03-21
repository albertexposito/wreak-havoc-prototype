using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LocalCharacterSpawner : MonoBehaviour
{

    [SerializeField] private TestingPlayerInputInitializer _playersTestingPrefab;

    [SerializeField] private BasePlayerCharacter _playerCharacterPrefab;

    public void InstantiatePlayers(List<IPlayerIdentity> players/*, SpawnPoint[] spawnPoints*/)
    {
        foreach (IPlayerIdentity player in players)
        {
            BasePlayerCharacter playerChar = InstantiateCharacter(/*spawnPoints[player.playerIndex]*/);
            player.SetCharacter(playerChar);
        }
    }

    public BasePlayerCharacter InstantiateCharacter(/*SpawnPoint spawnPoint*/)
    {
        Debug.Log("Spawning Character!");

        BasePlayerCharacter playerCharacter = Instantiate(_playerCharacterPrefab/*, spawnPoint.transform.position, spawnPoint.transform.rotation*/);

        return playerCharacter;
    }


    /// <summary>
    /// Spawns characters (for testing purposes)
    /// </summary>
    /// <param name="spawnPoints"></param>
    public void InstantiateTestingCharacters(/*SpawnPoint[] spawnPoints*/)
    {

        Debug.Log("Spawning Testing Characters!");
        
        Player playerKeyboard = Instantiate(_playersTestingPrefab.PlayerKeyboard);
        BasePlayerCharacter testCharacter1 = InstantiateCharacter(/*spawnPoints[0]*/);
        playerKeyboard.SetCharacter(testCharacter1);

        Player playerGamepad = Instantiate(_playersTestingPrefab.PlayerGamepad);
        BasePlayerCharacter testCharacter2 = InstantiateCharacter(/*spawnPoints[1]*/);
        playerGamepad.SetCharacter(testCharacter2);

        LocalGameManager.Instance.AddPlayer(playerKeyboard.GetComponent<PlayerInput>());
        LocalGameManager.Instance.AddPlayer(playerGamepad.GetComponent<PlayerInput>());
    }

}
