using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonCharacterSpawner : SimulationBehaviour
{

    [SerializeField] private GameObject _playerCharacterPrefab;

    public NetworkObject SpawnCharacter(SpawnPoint spawnPoint, PhotonPlayerIdentity playerIdentity)
    {

        Debug.Log($"Spawning character at spawn point: {spawnPoint.name} for player: {playerIdentity.Object.InputAuthority}");

        NetworkObject character = Runner.Spawn(
            _playerCharacterPrefab,
            spawnPoint.transform.position,
            spawnPoint.transform.rotation,
            playerIdentity.PlayerRef
        );

        return character;

    }
}
