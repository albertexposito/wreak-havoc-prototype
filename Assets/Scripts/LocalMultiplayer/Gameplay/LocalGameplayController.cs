using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    STARTING = 0,
    COUNTDOWN = 1,
    PLAYING = 2,
    FINISHED = 3,
}

public class LocalGameplayController : MonoBehaviour
{

    public bool _gameRunning = false;

    private GameState _gameState = GameState.STARTING;

    private BaseTimer _gameTimer;

    private const float COUNTDOWN_TIME = 3f;
    private const float GAME_TIME = 10;

    private void Awake()
    {
        _gameTimer = new LocalTimer();
    }


    #region GAME UPDATE

    private void Update()
    {
        OnInput();
    }

    private void FixedUpdate()
    {

        switch (_gameState)
        {
            case GameState.COUNTDOWN:
                CharacterLogicUpdate();
                if (_gameTimer.IsRunning() && _gameTimer.Expired())
                    StartGame();
                break;

            case GameState.PLAYING:
                CharacterLogicUpdate();
                break;

            case GameState.FINISHED:

                break;
        }

    }

    public virtual void OnInput()
    {
        foreach (Player player in LocalGameManager.Instance.Players)
            if (player.CurrentCharacter != null)
                player.CurrentCharacter.ProcessInput();
    }

    public virtual void CharacterLogicUpdate()
    {
        foreach (Player player in LocalGameManager.Instance.Players)
            if (player.CurrentCharacter != null)
                player.CurrentCharacter.CharacterUpdate();
    }

    #endregion GAME UPDATE

    public void StartCountdown()
    {
        _gameState = GameState.COUNTDOWN;
        _gameTimer.StartTimer(COUNTDOWN_TIME);

        Debug.Log($"[GAMEPLAY CONTROLLER] - Starting COUNTDOWN!");

    }

    public void StartGame()
    {
        _gameState = GameState.PLAYING;
        _gameTimer.StartTimer(GAME_TIME);

        foreach (Player player in LocalGameManager.Instance.Players)
            if (player.CurrentCharacter != null)
                player.CurrentCharacter.ActivateCharacter();

        Debug.Log($"[GAMEPLAY CONTROLLER] - The game has STARTED!");

    }

    private void CheckGameTimer()
    {
        if (_gameTimer.IsRunning() && _gameTimer.Expired())
            FinishGame();

    }

    private void FinishGame()
    {
        Debug.Log($"[GAMEPLAY CONTROLLER] - The game has FINISHED!");
        _gameTimer.ResetTimer();
    }



}
