using Fusion;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PhotonStartGameUI : MonoBehaviour
{

    [SerializeField] private TMP_Text _startGameText;
    [SerializeField] private Button _startGameButton;

    private PhotonSessionInfoController _sessionInfoController;

    private void Awake()
    {
        _sessionInfoController = FindObjectOfType<PhotonSessionInfoController>();

        _sessionInfoController.OnPlayerJoined += SubscribeToReadyChanges;
    }

    private void Start()
    {
        SetUIState(false);
    }

    private void SubscribeToReadyChanges(PhotonPlayerIdentity identity)
    {
        identity.OnReadyStateChanged += OnReadyStateChanged;
    }

    private void OnReadyStateChanged(bool readyState)
    {
        bool allReady = true;

        foreach (KeyValuePair<PlayerRef, PhotonPlayerIdentity> playerIdentity in _sessionInfoController.PlayerIdentityByPlayerRef)
            if (playerIdentity.Key.IsRealPlayer && !playerIdentity.Value.IsReady)
            {
                allReady = false;
                break;
            }
                
        
        SetUIState(allReady);
    }

    private void SetUIState(bool allReady)
    {
        bool clientIsServer = _sessionInfoController.IsServer;

        _startGameButton.interactable = allReady && clientIsServer;

        if (allReady)
        {
            _startGameText.text = clientIsServer ? 
                "Press the start game button to start the game" :
                "Wait for the room host to start the game";
        }
        else
        {
            _startGameText.text = "Wait for all players to be ready";
        }
    }

    private void OnDestroy()
    {
        _sessionInfoController.OnPlayerJoined -= SubscribeToReadyChanges;

        foreach(PhotonPlayerIdentity playerIdentity in _sessionInfoController.PlayerIdentityByPlayerRef.Values)
            if(playerIdentity != null)
                playerIdentity.OnReadyStateChanged -= OnReadyStateChanged;
    }

}
