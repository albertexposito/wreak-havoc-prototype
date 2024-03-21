using Fusion;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PhotonLobbyController : MonoBehaviour
{

    public event Action OnLobbyJoiningStarted;
    public event Action OnLobbyJoined;

    //public event Action<List<SessionInfo>> OnLobbySessionInfoUpdate
    //{
    //    add { _lobbyCallbacks.OnSessionListUpdatedCallback += value; }
    //    remove { _lobbyCallbacks.OnSessionListUpdatedCallback -= value; }   
    //}

    public UnityEvent<NetworkRunner, List<SessionInfo>> OnLobbySessionInfoUpdate
    { get => _networkRunnerManager.NetworkEvents.OnSessionListUpdate; }

    private NetworkRunnerManager _networkRunnerManager;

    //[SerializeField] private NetRunnerLobbyCallbacks _lobbyCallbacks;

    private void Awake()
    {
        _networkRunnerManager = NetworkRunnerManager.Instance;
        //InitializeLobbyCallbacks();
    }

    private void InitializeLobbyCallbacks()
    {
        //_lobbyCallbacks = new NetRunnerLobbyCallbacks();
        //_networkRunnerManager.AddNetworkRunnerCallbacks(_lobbyCallbacks);
    }

    /// <summary>
    /// Makes the current runner join the photon fusion lobby.
    /// </summary>
    /// <returns></returns>
    public async void JoinLobby()
    {
        Debug.Log("[PhotonLobbyUseCase -> JoinLobby] - Join Lobby started");

        OnLobbyJoiningStarted?.Invoke();

        string lobbyID = ConstantValues.LOBBY_DEFAULT_NAME;

        StartGameResult result = await _networkRunnerManager.NetworkRunner.JoinSessionLobby(SessionLobby.Custom, lobbyID);

        if (result.Ok)
            Debug.Log("[PhotonLobbyUseCase -> JoinLobby] - JoinLobby OK");
        else
            Debug.LogError($"[PhotonLobbyUseCase -> JoinLobby] - Unable to join lobby {lobbyID}");

        OnLobbyJoined?.Invoke();
        // return result.Ok;

    }

    // Creating a game as a host
    public async void CreateGameSession(string sessionName)
    {
        bool gameCreatedSuccessfully = await NetworkRunnerManager.Instance.StartGameSession(GameMode.Host, sessionName);
    }

    // Joining a game as a client
    public async void JoinGameSession(SessionInfo sessionInfo)
    {
        bool gameJoinedSuccessfully = await NetworkRunnerManager.Instance.StartGameSession(GameMode.Client, sessionInfo.Name);
    }

    //private void OnDestroy()
    //{
    //    _networkRunnerManager.RemoveNetworkRunnerCallbacks(_lobbyCallbacks);
    //}

}
