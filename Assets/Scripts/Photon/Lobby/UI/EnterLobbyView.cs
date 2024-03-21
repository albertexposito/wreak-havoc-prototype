using ParrelSync;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnterLobbyView : OnlineLobbyView
{

    [SerializeField] private TMP_InputField _playerNameInputField;
    [SerializeField] private Button _joinLobbyButton;

    [SerializeField] private PhotonLobbyController _lobbyController;

    private void Awake()
    {
        //_networkRunnerManager = GlobalManager.Instance.GetDependency<NetworkRunnerController>();
        //_lobbyController = GlobalManager.Instance.GetDependency<PhotonLobbyController>();

        _playerNameInputField.text = GetName();

    }

    private string GetName()
    {
        string name = string.Empty;

        if (ClonesManager.IsClone())
            name = ClonesManager.GetArgument();
        else
            name = "HOST!";

        return name;
    }

    public override void IntializeOnlineLobbyView(OnlineMultiplayerLobbyUIHandler uiHandler)
    {
        base.IntializeOnlineLobbyView(uiHandler);

        _joinLobbyButton.onClick.AddListener(TryJoinLobby);

    }

    private void TryJoinLobby()
    {
        if (IsInputNameValid())
        {
            // TODO: Don't set the name in the view!
            string playerNickname = _playerNameInputField.text;
            NetworkRunnerManager.Instance.SetNickname(playerNickname);
            _lobbyController.JoinLobby();

        }
    }


    private bool IsInputNameValid()
    {
        return _playerNameInputField.text != string.Empty;

    }

    private void OnDestroy()
    {
        _joinLobbyButton.onClick.RemoveAllListeners();
    }
}
