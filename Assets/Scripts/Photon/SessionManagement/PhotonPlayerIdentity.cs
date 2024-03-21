using Fusion;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// This class contains all the information of the Photon character
/// from the player index to the character it has selected
/// </summary>
public class PhotonPlayerIdentity : NetworkBehaviour, IPlayerIdentity
{
    public event Action<string> OnNameChanged;
    public event Action<bool> OnReadyStateChanged;
    public event Action<int, IPlayerIdentity> OnPlayerLivesChange;

    public string PlayerName => CharacterName.ToString();
    public int PlayerIndex => CharacterIndex;
    public bool IsReady => NetworkedIsPlayerReady;

    public PlayerRef PlayerRef => Object.InputAuthority;
    public int CurrentLives => throw new NotImplementedException();
    public BasePlayerCharacter CurrentCharacter { get; private set; }


    #region NETWORK VARIABLES

    private ChangeDetector _changes;

    [Networked] public int CharacterIndex { get; set; }
    [Networked] public NetworkString<_32> CharacterName { get; set; }
    [Networked] public NetworkBool NetworkedIsPlayerReady { get; set; }
    [Networked] public NetworkObject CharacterNetworkObject { get; set; }


    #endregion NETWORK VARIABLES

    public void InitializeNetworkIdentity(int index)
    {
        CharacterIndex = index;
    }

    public override void Spawned()
    {
        _changes = GetChangeDetector(ChangeDetector.Source.SimulationState);

        Debug.Log($"[PHOTON][CUSTOM] - PlayerIdentity spawned, has input authority: {Object.HasInputAuthority}");

        if (Object.HasInputAuthority)
        {
            RPC_SetNickname(NetworkRunnerManager.Instance.PlayerNickname);
            Debug.Log($"[PHOTON][CUSTOM] - Setting new name for PlayerIdentity: {CharacterName}");
        }


    }

    public void SetCharacter(BasePlayerCharacter character)
    {
        Debug.Log($"[PHOTON][CUSTOM][CHARACTER_SETUP] - Calling the WRONG Method!");

    }

    public void SetCharacter(NetworkObject characterNetworkObject)
    {
        Debug.Log($"[PHOTON][CUSTOM][CHARACTER_SETUP] - Calling the RIGHT Method!");
        CharacterNetworkObject = characterNetworkObject;

        Runner.SetIsSimulated(characterNetworkObject, Runner.IsServer);

        CurrentCharacter = characterNetworkObject.GetComponent<BasePlayerCharacter>();
    }


    public void GetInputForPlayerIdentityAndPassToCharacter()
    {
        if (!Runner.IsServer)
            return;


        if(Runner.TryGetInputForPlayer(Object.InputAuthority, out PhotonGameplayInputData retrievedInputData))
        {
            LocalPlayerGameplayInputData convertedInputData = retrievedInputData.ConvertToLocalGameplayPlayerInputData();

            CurrentCharacter.SetInput(convertedInputData);

            CurrentCharacter.CharacterUpdate();
        }

    }

    #region RPCs


    [Rpc(sources: RpcSources.InputAuthority, targets: RpcTargets.StateAuthority)]
    private void RPC_SetNickname(NetworkString<_32> playerNickname, RpcInfo rpcInfo = default)
    {
        // Change the player's name
        CharacterName = playerNickname;

        // Broadcast it to the rest of the players
        RPC_BroadcastNicknameChange(rpcInfo.Source);
    }

    [Rpc(sources: RpcSources.StateAuthority, targets: RpcTargets.All, HostMode = RpcHostMode.SourceIsServer)]
    private void RPC_BroadcastNicknameChange(PlayerRef playerRefWithNewNickname)
    {
        OnNameChanged?.Invoke(CharacterName.ToString());
    }

    // // // // // // // // // // // // 

    [Rpc(sources: RpcSources.InputAuthority, targets: RpcTargets.StateAuthority)]
    public void RPC_TogglePlayerReady(RpcInfo rpcInfo = default)
    {
        // Change the player's name
        NetworkedIsPlayerReady = !NetworkedIsPlayerReady;

        // Broadcast it to the rest of the players
        RPC_BroadcastPlayerReadyChange(rpcInfo.Source);
    }

    [Rpc(sources: RpcSources.StateAuthority, targets: RpcTargets.All, HostMode = RpcHostMode.SourceIsServer)]
    private void RPC_BroadcastPlayerReadyChange(PlayerRef playerRefWithNewNickname)
    {
        OnReadyStateChanged?.Invoke(IsReady);
    }

    #endregion RPCs

}
