using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class LocalPlayerController : MonoBehaviour
{

    //private InputDeviceController _inputDeviceController;

    //public event Action<Player> OnPlayerJoinedGame;
    //public event Action<Player, int> OnPlayerLeftGame;

    //private void Awake()
    //{
    //    _inputDeviceController = GetComponent<InputDeviceController>();

    //    _inputDeviceController.OnLocalDeviceDetected += PlayerJoinsGame;
    //}

    //private void PlayerJoinsGame(PlayerInput input)
    //{

    //    Player player = input.GetComponent<Player>();


    //    OnPlayerJoinedGame?.Invoke(player);
    //}

    //public event Action<Player> OnPlayerJoinedGame;
    //public event Action<Player, int> OnPlayerLeftGame;

    //private Player[] _currentPlayers;

    //private void Start()
    //{
    //    // Works on the same list as the game model
    //    _currentPlayers = GameManager.Instance.GamePlayers;
    //}

    //#region PUBLIC METHODS

    //public void PlayerJoinsGame(Player player)
    //{

    //    if (IsPlayerAlreadyInGame(player))
    //    {
    //        Debug.LogError($"The player is already in the game");
    //        return;
    //    }

    //    if (player == null)
    //    {
    //        Debug.LogError($"Couldn't find player with id: {player.id}");
    //        return;
    //    }

    //    if (player.playerIndex != -1)
    //    {
    //        Debug.LogError($"The player with id: {player.id} is already playing! | playerIndex: {player.playerIndex}");
    //        return;
    //    }

    //    int emptyGameSlotIndex = FindEmptyGameSlotIndex();

    //    if (emptyGameSlotIndex == -1)
    //    {
    //        Debug.LogError($"The player with id: {player.id} cannot join! there are no free slots!");
    //        return;
    //    }

    //    AssignPlayerToGame(player, emptyGameSlotIndex);

    //}

    //private void AssignPlayerToGame(Player newPlayer, int gameSlotIndex)
    //{
    //    //_gameLocalPlayers[gameSlotIndex] = newPlayer;
    //    newPlayer.SetIndex(gameSlotIndex);

    //    Debug.Log($"[LocalDevicesUseCase] - Player Assigned to index: {gameSlotIndex}");
    //    _currentPlayers[gameSlotIndex] = newPlayer;

    //    OnPlayerJoinedGame?.Invoke(newPlayer);
    //}


    //public void PlayerLeavesGame(Player player)
    //{

    //    if (player == null)
    //    {
    //        Debug.LogError($"Couldn't find player with id: {player.id}");
    //        return;
    //    }

    //    if (player.playerIndex == -1)
    //    {
    //        Debug.LogError($"The player with id: {player.id} is NOT playing! | playerIndex: {player.playerIndex}");
    //        return;
    //    }

    //    RemovePlayerFromGame(player);
    //}

    //private void RemovePlayerFromGame(Player playerToExit)
    //{
    //    int oldGameSlotIndex = playerToExit.playerIndex;

    //    playerToExit.SetIndex(-1);
    //    _currentPlayers[oldGameSlotIndex] = null;

    //    OnPlayerLeftGame?.Invoke(playerToExit, oldGameSlotIndex);

    //}

    //#endregion PUBLIC METHODS

    //#region UTILS

    //public int GetAmountOfPlayers()
    //{
    //    int amount = 0;

    //    for (int i = 0; i < _currentPlayers.Length; i++)
    //        if (_currentPlayers[i] != null)
    //            amount++;

    //    return amount;
    //}

    //private int FindEmptyGameSlotIndex()
    //{
    //    int emptySlotIndex = -1;

    //    for (int i = 0; i < _currentPlayers.Length; i++)
    //        if (_currentPlayers[i] == null)
    //        { emptySlotIndex = i; break; }

    //    return emptySlotIndex;
    //}


    //public T FindPlayerById<T>(string id) where T : Player
    //{
    //    Player playerToFind = null;

    //    foreach (Player player in _currentPlayers)
    //        if (player.id == id && player is T)
    //        { playerToFind = player; break; }

    //    return (T)playerToFind;
    //}

    //private bool IsPlayerAlreadyInGame(Player player)
    //{
    //    return _currentPlayers.Contains(player);
    //}

    //#endregion UTILS

}
