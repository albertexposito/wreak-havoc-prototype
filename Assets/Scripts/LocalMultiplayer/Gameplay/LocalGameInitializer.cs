using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LocalGameInitializer : MonoBehaviour
{

    [SerializeField] private LocalGameManager _gameManager;

    private StageComponents _stageComponents;
    private LocalCharacterSpawner _localCharacterSpawner;
    private LocalGameplayController _localGameplayController;

    public event Action<StageComponents> OnLevelLoaded;
    public event Action<List<Player>> OnPlayersSpawned;

    private void Awake()
    {
        _gameManager.InitManager();

        _localCharacterSpawner = FindObjectOfType<LocalCharacterSpawner>();
        _localGameplayController = FindObjectOfType<LocalGameplayController>();

    }

    private void Start()
    {
        BeginGamePreparation();
    }

    public void BeginGamePreparation()
    {
        LoadGameStage();
    }

    private void LoadGameStage()
    {

        AsyncOperation ao = SceneManager.LoadSceneAsync(_gameManager.DEFAULT_STAGE_NAME, LoadSceneMode.Additive);

        ao.completed += OnLevelLoadedCallback;
    }

    private void OnLevelLoadedCallback(AsyncOperation ao)
    {
        Debug.Log("Level Loaded");

        // FIX SCENE LIGHTING
        Scene loadedScene = SceneManager.GetSceneByName(_gameManager.DEFAULT_STAGE_NAME);
        SceneManager.SetActiveScene(loadedScene);

        if (!GetStageComponents()) return;

        InstantiatePlayerCharacters();

        StartGame();

    }


    private void StartGame()
    {

        foreach (Player player in LocalGameManager.Instance.Players)
            player.CurrentCharacter.SpawnPlayer(_stageComponents.PlayerSpawnPoints[player.PlayerIndex].transform);

        _localGameplayController.StartCountdown();
    }

    private void FinishGame()
    {
        SceneManager.LoadScene(ConstantValues.LOCAL_GAME_RESULT_SCENE_NAME);
    }

    #region GAME LOADING STEPS

    private bool GetStageComponents()
    {
        _stageComponents = FindObjectOfType<StageComponents>();

        if (_stageComponents == null)
        {
            Debug.LogError($"[LocalGameInitializer] - the stage: {LocalGameManager.Instance.DEFAULT_STAGE_NAME} does not have StageComponents!");
            return false;
        }

        OnLevelLoaded?.Invoke(_stageComponents);

        return true;
    }

    private void InstantiatePlayerCharacters()
    {

        if (_gameManager.Players.Count > 0)
            // REGULAR BEHAVIOUR, PLAYERS WERE SET FROM THE CHARACTER SELECTION SCREEN
            _localCharacterSpawner.InstantiatePlayers(_gameManager.Players);
        
        else
            // TESTING, OPENING EDITOR DIRECTLY FROM THE SCENE
            _localCharacterSpawner.InstantiateTestingCharacters();

        foreach (Player player in _gameManager.Players)
        {
            player.SetLives(ConstantValues.DEFAULT_LIVES);
            player.CurrentCharacter.OnPlayerDied.AddListener(OnCharacterDied);
        }

        OnPlayersSpawned?.Invoke(_gameManager.Players);
    }

    private void OnCharacterDied(BasePlayerCharacter character)
    {
        Player player = _gameManager.GetPlayerFromCharacter(character);

        player.LoseLive();

        if (player.CurrentLives > 0)
            character.SpawnPlayer(_stageComponents.PlayerSpawnPoints[player.PlayerIndex].transform, ConstantValues.RESPAWN_TIME);

        CheckCharactersLivesAndFinishGame();

    }

    private void CheckCharactersLivesAndFinishGame()
    {
        int remainingPlayers = _gameManager.Players.Count;

        foreach(Player player in _gameManager.Players)
            if (player.CurrentLives == 0)
                remainingPlayers--;

        if (remainingPlayers == 1)
            FinishGame();

    }

    #endregion GAME LOADING STEPS
}