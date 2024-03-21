using Fusion;
using ParrelSync;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlineCharacterSelectionInitializer : MonoBehaviour
{

    [SerializeField] private NetworkRunnerManager _networkRunnerManager;
    [SerializeField] private PhotonSessionInfoController _sessionInfo;

    private void Awake()
    {
        _networkRunnerManager.InitManager();
    }

    private void Start()
    {
        SessionInfo info = _networkRunnerManager.NetworkRunner.SessionInfo;
        Debug.Log($"[INITIALIZATION] - Session info");

        JoinGameSessionIfNotInOne();
    }

    #region EDITOR TESTING
    
    private void JoinGameSessionIfNotInOne()
    {
        SessionInfo info = _networkRunnerManager.NetworkRunner.SessionInfo;

        // Correct flow, player enter the selection scene while already in a game session
        if (info.IsValid)
            return;


        // Development flow, game started in selection scene, need to join a game session

        string sessionName = "DevSession";

        _networkRunnerManager.SetNickname(GetName());




        _networkRunnerManager.StartGameSession(GameMode.AutoHostOrClient, sessionName);
    
    }

    private string GetName()
    {
        string name = string.Empty;

        if (ClonesManager.IsClone())
            name = ClonesManager.GetArgument();
        else
            name = "HOST!";

        return name;
    }

    #endregion

}
