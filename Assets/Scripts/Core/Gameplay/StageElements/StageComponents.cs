using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageComponents : MonoBehaviour
{

    public Transform CameraCenterPosition { get => _cameraPosition; }
    [SerializeField] private Transform _cameraPosition;

    public SpawnPoint[] PlayerSpawnPoints { get => _playerSpawnPoints; }
    [SerializeField] private SpawnPoint[] _playerSpawnPoints;

}
