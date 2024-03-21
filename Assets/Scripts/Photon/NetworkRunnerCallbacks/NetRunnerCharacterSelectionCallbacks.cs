//using Fusion;
//using Fusion.Sockets;
//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class NetRunnerCharacterSelectionCallbacks : INetworkRunnerCallbacks
//{

//    public event Action<PlayerRef> OnHostLoadsScene;
//    public event Action<PlayerRef> OnPlayerJoinedCallback;
//    public event Action<PlayerRef> OnPlayerLeftCallback;

//    #region IMPLEMENTED METHODS

//    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
//    {
//        OnPlayerJoinedCallback?.Invoke(player);
//    }

//    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
//    {
//        OnPlayerLeftCallback?.Invoke(player);
//    }



//    public void OnSceneLoadDone(NetworkRunner runner)
//    {
//        Debug.Log("SCENE LOAD FINISHED!");
//    }

//    public void OnSceneLoadStart(NetworkRunner runner)
//    {
        
//    }


//    public void OnInput(NetworkRunner runner, NetworkInput input)
//    {
        
//    }

//    #endregion IMPLEMENTED METHODS


//    #region NOT IMPLEMENTED METHODS

//    public void OnConnectedToServer(NetworkRunner runner)
//    {

//    }

//    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
//    {

//    }

//    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
//    {

//    }

//    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
//    {

//    }

//    public void OnDisconnectedFromServer(NetworkRunner runner)
//    {

//    }

//    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
//    {

//    }



//    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
//    {

//    }



//    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data)
//    {

//    }


//    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
//    {

//    }

//    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
//    {

//    }

//    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
//    {

//    }

//    public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
//    {

//    }

//    public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
//    {

//    }

//    public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason)
//    {

//    }

//    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data)
//    {

//    }

//    public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress)
//    {

//    }

//    #endregion NOT IMPLEMENTED METHODS



//}
