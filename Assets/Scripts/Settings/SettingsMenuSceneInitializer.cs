using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsMenuSceneInitializer : MonoBehaviour
{
    [SerializeField] private LocalGameManager _gameManager;


    private void Awake()
    {
        _gameManager.InitManager();
    }

    private void Start()
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(_gameManager.DEFAULT_STAGE_NAME, LoadSceneMode.Additive);
        ao.completed += OnLevelLoadedCallback;
    }

    private void OnLevelLoadedCallback(AsyncOperation obj)
    {
        Scene loadedScene = SceneManager.GetSceneByName(_gameManager.DEFAULT_STAGE_NAME);
        SceneManager.SetActiveScene(loadedScene);
    }
}
