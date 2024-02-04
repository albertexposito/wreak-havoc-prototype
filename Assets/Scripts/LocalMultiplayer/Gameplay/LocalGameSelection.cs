using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalGameSelection : MonoBehaviour
{

    [SerializeField] private GameManager _gameManager;
    [SerializeField] private LocalResourceProvider _localResourceProvider;

    private void Awake()
    {
        _gameManager.InitManager();
        SetupGame();
    }

    private void SetupGame()
    {
        // _gameManager.CreateGame(Game.GameType.LOCAL, _localResourceProvider);
    }

}
