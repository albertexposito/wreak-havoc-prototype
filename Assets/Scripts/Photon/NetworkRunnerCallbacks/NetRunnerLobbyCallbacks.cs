//using Fusion;
//using Fusion.Sockets;
//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class NetRunnerLobbyCallbacks : MonoBehaviour, INetworkRunnerCallbacks
//{

//    public event Action<List<SessionInfo>> OnSessionListUpdatedCallback;

//    #region IMPLEMENTED METHODS


//    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
//    {
//        Debug.Log("NetRunnerLobbyCallback -> OnSessionListUpdated");
//        OnSessionListUpdatedCallback?.Invoke(sessionList);
//    }


//    public void OnInput(NetworkRunner runner, NetworkInput input)
//    {
//        //Debug.Log("Receiving input!");
//    }

//    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
//    {

//    }


//    public void OnSceneLoadStart(NetworkRunner runner)
//    {
        
//    }

//    public void OnSceneLoadDone(NetworkRunner runner)
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



//    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
//    {
        
//    }

//    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data)
//    {
        
//    }



//    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
//    {
        
//    }

//    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
//    {
        
//    }

//    #endregion NOT IMPLEMENTED METHODS

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

//}
