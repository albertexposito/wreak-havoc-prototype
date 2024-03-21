using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlotContainer : MonoBehaviour
{

    private PlayerSlotUI[] _playerSlots;

    private void Awake()
    {
        InitializePlayerSlots();
    }

    private void InitializePlayerSlots()
    {
        _playerSlots = GetComponentsInChildren<PlayerSlotUI>();

        for(int i = 0; i < _playerSlots.Length; i++)
            _playerSlots[i].Initialize(i);
        
    }

    public void AddPlayer(IPlayerIdentity player)
    {
        _playerSlots[player.PlayerIndex].SetPlayerToSlot(player);
    }

    public void RemovePlayer(IPlayerIdentity player, int oldIndex)
    {
        _playerSlots[oldIndex].RemovePlayerFromSlot(player);
    }

}
