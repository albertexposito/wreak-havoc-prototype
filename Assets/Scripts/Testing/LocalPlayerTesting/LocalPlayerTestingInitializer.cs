using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class LocalPlayerTestingInitializer : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;

    [SerializeField] private PlayerInput _playerInput;
    
    [SerializeField] private Transform _fakeSpawnPoint1;
    [SerializeField] private Transform _fakeSpawnPoint2;

    [SerializeField] private BasePlayerCharacter _characterPrefab;

    [SerializeField] private StateTestingUI _stateTestingUI;

    private BasePlayerCharacter _character;
    private BasePlayerCharacter _AiCharacter;

    private float RESPAWN_TIME = 2;
    private YieldInstruction _respawnWaitTimeYieldInstruction;

    private float SPAWN_TIME = 2;
    private YieldInstruction _spawnWaitTimeYieldInstruction;

    private void Awake()
    {
        _gameManager.InitManager();

        _respawnWaitTimeYieldInstruction = new WaitForSeconds(RESPAWN_TIME);
        _spawnWaitTimeYieldInstruction = new WaitForSeconds(SPAWN_TIME);
    }


    private void InstantiateCharacter()
    {

        _character = Instantiate(_characterPrefab, _fakeSpawnPoint1.position, _fakeSpawnPoint1.rotation);
        _AiCharacter = Instantiate(_characterPrefab, _fakeSpawnPoint2.position, _fakeSpawnPoint2.rotation);

        _AiCharacter.AddComponent<AIRocketSpawner>();

        if(_stateTestingUI != null)
        {
            _stateTestingUI.AddCharacter( _character );
            _stateTestingUI.AddCharacter( _AiCharacter );
        }

    }

    private void Start()
    {
        InstantiateCharacter();

        _character.InitializePlayer(_playerInput.GetComponent<Player>());
        _AiCharacter.InitializePlayer(null);

        _character.OnPlayerDied.AddListener(RespawnPlayer);
        _AiCharacter.OnPlayerDied.AddListener(RespawnPlayer);

        StartCoroutine(SpawnPlayerCoroutine(_character));
        StartCoroutine(SpawnPlayerCoroutine(_AiCharacter));
    }

    private void RespawnPlayer(BasePlayerCharacter character) => StartCoroutine(SpawnPlayerCoroutine(character));


    private IEnumerator SpawnPlayerCoroutine(BasePlayerCharacter character, bool waitForRespawnTime = true)
    {
        if(waitForRespawnTime)
            yield return _respawnWaitTimeYieldInstruction;

        // TESTING PURPOSES, USE A DICTIONARY OR OTHER METHOD IN REAL CASE
        Transform spawnPoint = character == _character ? _fakeSpawnPoint1 : _fakeSpawnPoint2;

        character.SpawnPlayer(spawnPoint);

        //yield return _spawnWaitTimeYieldInstruction;

        character.ActivateCharacter();
    }

    public void FixedUpdate()
    {
        _character.ProcessInput();
        _AiCharacter.ProcessInput();

        _character.CharacterUpdate();
        _AiCharacter.CharacterUpdate();
    }

}
