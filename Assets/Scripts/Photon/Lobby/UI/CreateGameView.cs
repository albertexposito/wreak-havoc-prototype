using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateGameView : OnlineLobbyView
{

    [SerializeField] private TMP_InputField _gameNameInputField;
    [SerializeField] private Button _createGameButton;

    private PhotonLobbyController _lobbyController;
    //private PhotonGameSessionController _gameSessionController;

    public override void IntializeOnlineLobbyView(OnlineMultiplayerLobbyUIHandler uiHandler)
    {
        base.IntializeOnlineLobbyView(uiHandler);

        _createGameButton.onClick.AddListener(OnCreateGameButtonClickedCallback);

        _lobbyController = FindObjectOfType<PhotonLobbyController>();

    }

    private void OnCreateGameButtonClickedCallback()
    {
        string sessionName = _gameNameInputField.text;
        _lobbyController.CreateGameSession(sessionName);
    }

}
