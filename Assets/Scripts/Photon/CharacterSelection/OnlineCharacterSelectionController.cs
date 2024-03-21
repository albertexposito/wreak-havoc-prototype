using Fusion;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnlineCharacterSelectionController : NetworkBehaviour
{

    //public event Action<PlayerRef> OnPlayerJoinedGameSession
    //{
    //    add { _netRunnerCharSelectionCallbacks.OnPlayerJoinedCallback += value; }
    //    remove { _netRunnerCharSelectionCallbacks.OnPlayerJoinedCallback -= value; }
    //}

    //public event Action<PlayerRef> OnPlayerLeftGameSession
    //{
    //    add { _netRunnerCharSelectionCallbacks.OnPlayerJoinedCallback += value; }
    //    remove { _netRunnerCharSelectionCallbacks.OnPlayerJoinedCallback -= value; }
    //}

    //[SerializeField] private NetworkRunnerManager _networkRunnerManager;
    ////[SerializeField] private GameManager _gameManager;

    //private NetRunnerCharacterSelectionCallbacks _netRunnerCharSelectionCallbacks;

    //void Awake()
    //{
    //    _netRunnerCharSelectionCallbacks = new NetRunnerCharacterSelectionCallbacks();
    //    _networkRunnerManager.AddNetworkRunnerCallbacks(_netRunnerCharSelectionCallbacks);

    //    // _gameManager.CreateGame(Game.GameType.ONLINE, null);
    //}

    public void StartGame()
    {
        if (!Runner.IsServer)
        {
            Debug.LogError("Clients are not allowed to start the game!");
            return;
        }

        Runner.SessionInfo.IsOpen = false;
        Runner.LoadScene("OnlineGameSetup");
    }

    public void DisconnectFromServer()
    {
        Runner.Shutdown(false, ShutdownReason.GameClosed);
        SceneManager.LoadScene("OnlineLobby");
    }

    private void OnDestroy()
    {
        //_networkRunnerManager.RemoveNetworkRunnerCallbacks(_netRunnerCharSelectionCallbacks);
    }
}
