using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlineLobbyView : BaseView
{

    [SerializeField] protected MultiplayerLobbyUIState _viewState;
    protected OnlineMultiplayerLobbyUIHandler _uiHandler;

    public virtual void IntializeOnlineLobbyView(OnlineMultiplayerLobbyUIHandler uiHandler)
    {
        _uiHandler = uiHandler;
        _uiHandler.OnMPLobbyUIStateChange += OnViewStateChanged;
    }

    protected virtual void OnViewStateChanged(MultiplayerLobbyUIState oldState, MultiplayerLobbyUIState newState)
    {
        if (newState == _viewState)
            ShowView();
        else
            HideView();
    }

}
