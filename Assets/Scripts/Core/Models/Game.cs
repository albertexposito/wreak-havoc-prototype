using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Game
{
    public enum GameType { LOCAL = 0, ONLINE = 1 };

    public Player[] players;

    public Player winner;

    // TODO add stage (only 1 for the moment)

    public float GameDuration = 2; // How long the game will last (In seconds)

    public GameType gameType;

    public Game(GameType gameType)
    {   
        players = new Player[4];

        this.gameType = gameType;

    }

    public bool AddPlayer(Player player, int index)
    {
        if (players[index] != null)
        {
            Debug.LogError($"Trying to add player: {player.PlayerName} to slot: {index}, but it's not available");
            return false;
        }

        players[index] = player;
        return true;
    }

    public bool RemovePlayer(Player player)
    {
        int playerIndex = Array.IndexOf(players, player);

        if (playerIndex != -1)
        {
            Debug.LogError($"Trying to remove player: {player.PlayerName} from game, but it's playing");
            return false;
        }

        players[playerIndex] = null;
        return true;

    }


}


