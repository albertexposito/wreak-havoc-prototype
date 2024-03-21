using Fusion;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PhotonGameplayController : NetworkBehaviour
{

    private PhotonSessionInfoController _sessionInfoController;
    private List<PhotonPlayerIdentity> _playerList;

    public override void Spawned()
    {
        _sessionInfoController = FindObjectOfType<PhotonSessionInfoController>();
        _playerList = _sessionInfoController.PlayerIdentityByPlayerRef.Values.ToList();
    }

    public override void FixedUpdateNetwork()
    {

        if (Runner.IsServer)
            ServerLogic();

    }

    private void ServerLogic()
    {
        ProcessPlayerInputs();
    }


    private void ProcessPlayerInputs()
    {
        foreach (PhotonPlayerIdentity player in _playerList)
        {
            if(player.CurrentCharacter != null)
                player.GetInputForPlayerIdentityAndPassToCharacter();
        }
    }
}
