using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameResultInitializer : MonoBehaviour
{
    [SerializeField] private LocalGameManager _gameManager;

    [SerializeField] private CharacterModelHandler _characterModelHandler;
    [SerializeField] private TMP_Text _winnerName;
    [SerializeField] private Button _exitButton;

    private void Awake()
    {
        _gameManager.InitManager();

        _exitButton.onClick.AddListener(ReturnToSelectionScreen);
    }

    private void Start()
    {

        Player winner = _gameManager.GetWinnerPlayer();

        SetMeshColorForWinner(winner);
        _winnerName.text = winner.PlayerName;   
    }

    private void SetMeshColorForWinner(Player winner)
    {
        _characterModelHandler.SetMeshColor(winner.PlayerIndex);
    }

    private void ReturnToSelectionScreen()
    {
        SceneManager.LoadScene(ConstantValues.LOCAL_CHARACTER_SELECTION_SCENE_NAME);
    }

}
