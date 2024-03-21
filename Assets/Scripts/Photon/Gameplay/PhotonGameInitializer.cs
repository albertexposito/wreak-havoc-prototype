using Fusion;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PhotonGameInitializer : NetworkBehaviour, IGameInitializer
{

    public event Action<StageComponents> OnLevelLoaded;
    public event Action<List<IPlayerIdentity>> OnPlayersSpawned;

    public event Action<IPlayerIdentity> OnPlayerSpawned;

    private PhotonSessionInfoController _sessionInfo;

    private StageComponents _stageComponents;
    private PhotonCharacterSpawner _characterSpawner;
    private PhotonGameplayController _gameplayManager;

    private int _playerCount = 0;

    private void Awake()
    {
        if (NetworkRunnerManager.Instance == null)
            SceneManager.LoadScene(ConstantValues.PHOTON_CHARACTER_SELECTION_SCENE_NAME);
    }

    public override void Spawned()
    {
        _sessionInfo = FindObjectOfType<PhotonSessionInfoController>();

        _characterSpawner = FindObjectOfType<PhotonCharacterSpawner>();
        _gameplayManager = FindObjectOfType<PhotonGameplayController>();

        LoadGameStage();
    }

    private void LoadGameStage()
    {
        // TODO GameController.GetStage
        // Right now theres only one stage.

        string stageName = "Battlefield";

        AsyncOperation ao = SceneManager.LoadSceneAsync(stageName, LoadSceneMode.Additive);

        ao.completed += LevelLoaded;
    }

    private void LevelLoaded(AsyncOperation ao)
    {
        Debug.Log("Level Loaded");

        _stageComponents = FindObjectOfType<StageComponents>();
        
        if(Runner.IsServer)
            Runner.AddGlobal(_characterSpawner);

        RPC_LevelLoadedForClient();

        OnLevelLoaded?.Invoke(_stageComponents);

    }



    private void StartGame()
    {
        SpawnPlayers();
    }

    /// <summary>
    /// Spawns the players, this is only executed by the host / server.
    /// </summary>
    private void SpawnPlayers()
    {
        // Security check
        if (!Runner.IsServer)
            return;

        foreach (PhotonPlayerIdentity player in _sessionInfo.PlayerIdentityByPlayerRef.Values)
        {
            if (player != null)
            {
                NetworkObject characterNetObject = _characterSpawner.SpawnCharacter(
                    _stageComponents.PlayerSpawnPoints[player.PlayerIndex],
                    player
                );

                RPC_BroadcastCharacterSpawned(player, characterNetObject);

            }
        }
    }



    #region RPCs

    /// <summary>
    /// Function called from the clients (including the host)
    /// Only executed in the host
    /// </summary>
    /// <param name="player"></param>
    [Rpc(sources: RpcSources.All, targets: RpcTargets.StateAuthority)]
    private void RPC_LevelLoadedForClient(RpcInfo info = default)
    {
        // Security check
        if (!Runner.IsServer)
            return;

        Debug.Log($"[PHOTON][PLAYER_READY] Player: {info.Source.PlayerId} is ready");
        _playerCount++;

        if (Runner.ActivePlayers.Count() == _playerCount)
        {
            Debug.Log("[PHOTON][PLAYER_READY] Spawning Players!");
            StartGame();
        }
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="identity"></param>
    [Rpc(sources: RpcSources.StateAuthority, targets: RpcTargets.All, HostMode = RpcHostMode.SourceIsServer)]
    private void RPC_BroadcastCharacterSpawned(PhotonPlayerIdentity identity, NetworkObject characterNetworkObject)
    {
        identity.SetCharacter(characterNetworkObject);
        OnPlayerSpawned?.Invoke(identity);
    }

    #endregion RPCs

}
