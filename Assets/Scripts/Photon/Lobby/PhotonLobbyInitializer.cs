using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonLobbyInitializer : MonoBehaviour
{
    [SerializeField] private NetworkRunnerManager _networkRunnerManager;

    void Awake()
    {
        _networkRunnerManager.InitManager();
    }

}
