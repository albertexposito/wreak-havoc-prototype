using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public string id;

    public string PlayerName { get => _playerName; set => _playerName = value; }
    private string _playerName;

    public int PlayerIndex { get => _playerIndex; set => _playerIndex = value; }
    private int _playerIndex = -1;
    
    public PlayerInput PlayerInput { get => _playerInput; }
    private PlayerInput _playerInput;

    public BasePlayerCharacter CurrentCharacter { get => _currentCharacter; }
    private BasePlayerCharacter _currentCharacter;

    public int CurrentLives { get => _lives; }
    public event Action<int> OnLivesChange;
    private int _lives;
    

    public bool IsSamePlayerInput(PlayerInput playerInput) => _playerInput == playerInput;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
    }

    public void SetCharacter(BasePlayerCharacter character)
    {
        if(_currentCharacter != null)
        {
            Debug.LogWarning($"[Player] - The player '{PlayerName}' already has a character assigned");
            return;
        }


        _currentCharacter = character;

        character.InitializePlayer(this);
    }

    #region LIVE STOCKS METHODS
    public void SetLives(int lives)
    {
        _lives = lives;
        OnLivesChange?.Invoke(_lives);
    }
    public void LoseLive()
    {
        _lives--;
        OnLivesChange?.Invoke(_lives);
    }
    #endregion

}