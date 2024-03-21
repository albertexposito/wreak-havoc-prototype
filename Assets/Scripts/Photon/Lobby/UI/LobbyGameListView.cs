using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyGameListView : OnlineLobbyView
{

    private PhotonLobbyController _lobbyController;
    

    [SerializeField] private GameSessionInfoListItem _gameSessionInfoPrefab;
    private List<GameSessionInfoListItem> _gameSessionInfoList;

    [SerializeField] private Transform _gameListContainer;

    [SerializeField] private Button _createGameButton;

    //private PhotonLobbyPresenter _lobbyPresenter;

    //private PhotonGameSessionController _gameSessionController;

    public override void IntializeOnlineLobbyView(OnlineMultiplayerLobbyUIHandler uiHandler)
    {
        base.IntializeOnlineLobbyView(uiHandler);

        //_lobbyController = GlobalManager.Instance.GetDependency<PhotonLobbyController>();
        //_lobbyPresenter = GlobalManager.Instance.GetDependency<PhotonLobbyPresenter>();
        _lobbyController = FindObjectOfType<PhotonLobbyController>();

        //_lobbyController.OnLobbySessionInfoUpdate += UpdateGameList;
        _lobbyController.OnLobbySessionInfoUpdate.AddListener(UpdateGameList);
        InitializeGameList();

        //_lobbyPresenter.GameSessionInfoUIList.BindToProperty(UpdateGameList);

        _createGameButton.onClick.AddListener(OnCreateGameButtonClicked);
    }


    private void InitializeGameList()
    {
        int itemCount = 10;

        _gameSessionInfoList = new List<GameSessionInfoListItem>(itemCount);

        for(int i = 0; i < itemCount; i++)
        {
            GameSessionInfoListItem gameSessionInfoItem = Instantiate(_gameSessionInfoPrefab, _gameListContainer);
            _gameSessionInfoList.Add(gameSessionInfoItem);
            gameSessionInfoItem.Initialize(JoinGame);
        }
    }

    public override void ShowView()
    {
        base.ShowView();

        // TODO show loading screen
    }

    private void UpdateGameList(NetworkRunner runner, List<SessionInfo> UIItemList)
    {
        Debug.Log($"Updating game list, there are {UIItemList.Count} games available");

        for (int i = 0; i < _gameSessionInfoList.Count; i++)
        {
            if (i + 1 <= UIItemList.Count)
                _gameSessionInfoList[i].SetInformation(UIItemList[i]);
            else
                _gameSessionInfoList[i].ClearInformation();
        }
    }


    private void JoinGame(SessionInfo gameSessionInfo)
    {
        _lobbyController.JoinGameSession(gameSessionInfo);
    }

    private void OnCreateGameButtonClicked()
    {
        _uiHandler.ChangeState(MultiplayerLobbyUIState.CREATE_GAME);
    }

    private void OnDestroy()
    {
        _lobbyController.OnLobbySessionInfoUpdate.RemoveListener(UpdateGameList);
    }

}
