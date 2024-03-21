using Fusion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class PhotonSessionInfoController : NetworkBehaviour, IPlayerJoined, IPlayerLeft
{

    public Action<PhotonPlayerIdentity> OnPlayerJoined;
    public Action<PhotonPlayerIdentity> OnPlayerLeft;

    private NetworkRunnerManager _networkRunnerManager;

    [Networked]
    [Capacity(ConstantValues.MAX_PLAYERS_PER_GAME)]
    private NetworkArray<PlayerRef> PlayerRefArray => default;

    public Dictionary<PlayerRef, PhotonPlayerIdentity> PlayerIdentityByPlayerRef { get => _localPlayerIdentityByPlayerRef; }
    private Dictionary<PlayerRef, PhotonPlayerIdentity> _localPlayerIdentityByPlayerRef;

    public PhotonPlayerIdentity LocalPlayerIdentity => _localPlayerIdentity;
    private PhotonPlayerIdentity _localPlayerIdentity;

    public bool IsServer { get => Runner != null && Runner.IsServer; }

    void Start()
    {
        _localPlayerIdentityByPlayerRef = new Dictionary<PlayerRef, PhotonPlayerIdentity>(ConstantValues.MAX_PLAYERS_PER_GAME);
        _networkRunnerManager = NetworkRunnerManager.Instance;


    }

    public override void Spawned()
    {
        UpdateLocalPlayerDictionary();

        // This will persist through scenes as long as the game session doesn't change
        // This GameObject will have the PlayerIdentities as children
        // MUST BE DESTROYED MANUALLY WHEN LEAVING THE GAME SESSION
        Runner.MakeDontDestroyOnLoad(this.gameObject);
    }

    /// <summary>
    /// Called when a player joins the game (also called for the client that is joining)
    /// </summary>
    /// <param name="player"></param>
    public void PlayerJoined(PlayerRef player)
    {

        Debug.Log($"[PHOTON][CUSTOM] - Player Joined! {player}");

        if (Runner.IsServer)
            SpawnNetworkIdentity(player);
        
    }

    public void PlayerLeft(PlayerRef player)
    {
        Debug.Log("Player left!");

    }

    // ONLY EXECUTED IF SERVER!
    private void SpawnNetworkIdentity(PlayerRef playerRef)
    {

        int playerSlot = GetAvailableSlot();

        // Setting the playerRef to the Networked playerRefByIndex Dictionary
        PlayerRefArray.Set(playerSlot, playerRef);

        // Spawning the network identity object and setting it to the playerRef
        PhotonPlayerIdentity identity = Runner.Spawn (_networkRunnerManager.PlayerIdentityPrefab, inputAuthority: playerRef);
        identity.transform.SetParent (transform);
        identity.InitializeNetworkIdentity(playerSlot);

        Runner.SetPlayerObject(playerRef, identity.Object);

        // Should I wait for the next tick?
        RPC_NotifyPlayerJoined(playerRef);

    }


    [Rpc(RpcSources.StateAuthority, RpcTargets.All, HostMode = RpcHostMode.SourceIsServer)]
    private void RPC_NotifyPlayerJoined(PlayerRef playerRefJoining)
    {
        UpdateLocalPlayerDictionary();
        // DebugLocalPlayersList();
    }

    private int GetAvailableSlot()
    {

        for (int i = 0; i < PlayerRefArray.Length; i++)
            if (!PlayerRefArray[i].IsRealPlayer)
                return i;

        return -1;
    }




    private void UpdateLocalPlayerDictionary()
    {
        foreach(PlayerRef playerRef in PlayerRefArray)
        {
            if (!playerRef.IsRealPlayer)
                continue;

            if (!_localPlayerIdentityByPlayerRef.ContainsKey(playerRef))
                GetPlayerIdentityAndAddPlayerToLocalPlayerDictionary(playerRef);

        }

        PlayerRef playerRefToRemove = default;

        foreach(var tuple in _localPlayerIdentityByPlayerRef)
        {
            if (!PlayerRefArray.Contains(tuple.Key))
                playerRefToRemove = tuple.Key;
            
        }

        if(_localPlayerIdentityByPlayerRef.ContainsKey(playerRefToRemove))
            _localPlayerIdentityByPlayerRef.Remove(playerRefToRemove);
    }

    private void GetPlayerIdentityAndAddPlayerToLocalPlayerDictionary(PlayerRef playerRef)
    {
        PhotonPlayerIdentity playerIdentity = Runner.GetPlayerObject(playerRef).GetComponent<PhotonPlayerIdentity>();

        if(playerIdentity == null)
        {
            Debug.LogError($"[PHOTON][ERROR] - The playerRef : {playerRef.AsIndex} does not have a PlayerIdentity!");
            return;
        }

        _localPlayerIdentityByPlayerRef.Add(playerRef, playerIdentity);

        if (playerIdentity.HasInputAuthority)
            _localPlayerIdentity = playerIdentity;

        OnPlayerJoined?.Invoke(playerIdentity);
    }

    private async void DebugLocalPlayersList()
    {
        await Task.Delay((int)(Runner.DeltaTime * 1000));

        Debug.Log($"[PHOTON][CUSTOM][PLAYER LIST] - New Player list !! | Player count: {_localPlayerIdentityByPlayerRef.Values.Count}");
        foreach (PhotonPlayerIdentity playerIdentity in _localPlayerIdentityByPlayerRef.Values)
        {
            Debug.Log($"[PHOTON][CUSTOM][PLAYER LIST] - Slot: {playerIdentity.PlayerIndex} | PlayerName: {playerIdentity.PlayerName}");
        }

    }

}
