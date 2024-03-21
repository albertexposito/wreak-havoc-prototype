using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlineMultiplayerLobbyUIHandler : MonoBehaviour
{

    [SerializeField] private PhotonLobbyController _lobbyController;

    [SerializeField] private OnlineLobbyView[] _onlineLobbyViews;

    private MultiplayerLobbyUIState _currentState = MultiplayerLobbyUIState.DEFAULT;
    public event Action<MultiplayerLobbyUIState, MultiplayerLobbyUIState> OnMPLobbyUIStateChange; // <oldState, newState>

    private void Awake()
    {

        _lobbyController.OnLobbyJoiningStarted += OnLobbyJoiningStarted;
        _lobbyController.OnLobbyJoined += OnLobbyJoinedSuccessfully;

        InitializeViews();
    }

    private void InitializeViews()
    {
        foreach (OnlineLobbyView lobbyView in _onlineLobbyViews)
            lobbyView.IntializeOnlineLobbyView(this);
    }

    private void Start()
    {
        ChangeState(MultiplayerLobbyUIState.SELECT_NAME);
    }

    public void ChangeState(MultiplayerLobbyUIState newState)
    {
        if (newState == _currentState)
            return;

        MultiplayerLobbyUIState oldState = _currentState;

        _currentState = newState;

        OnMPLobbyUIStateChange?.Invoke(oldState, _currentState);
        Debug.Log($"OnlineMultiplayerLobbyUIHandler -> ChangeState | New State: {newState}");
    }

    private void OnLobbyJoiningStarted()
    {
        ChangeState(MultiplayerLobbyUIState.LOADING);
    }

    private void OnLobbyJoinedSuccessfully()
    {
        ChangeState(MultiplayerLobbyUIState.GAME_LIST);

    }




}

public enum MultiplayerLobbyUIState
{
    DEFAULT = -1,
    LOADING = 0,
    SELECT_NAME = 1,
    GAME_LIST = 2,
    CREATE_GAME = 3,
}

