using Fusion;
using Fusion.Sockets;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "NETWORK RUNNER MANAGER", menuName = "Managers/Network Runner Manager")]
public class NetworkRunnerManager : ScriptableObjectSingleton<NetworkRunnerManager>
{

    [SerializeField] private NetworkRunner _networkRunnerPrefab;

    public string PlayerNickname => _playerNickname;
    private string _playerNickname;

    public PhotonPlayerIdentity PlayerIdentityPrefab => _playerIdentityPrefab;
    [SerializeField] private PhotonPlayerIdentity _playerIdentityPrefab;

    public NetworkRunner NetworkRunner { get => _networkRunner; }
    private NetworkRunner _networkRunner;

    public NetworkEvents NetworkEvents { get => _networkEvents; }
    private NetworkEvents _networkEvents;

    private bool _initialized = false;


    public void InitManager()
    {
        if (_initialized)
            return;

        InitializeSingleton();

        TrySpawnNetworkRunner();

        _initialized = true;

    }

    #region START GAME

    /// <summary>
    /// Creates or joins a game room
    /// </summary>
    /// <param name="mode"></param>
    /// <param name="sessionName"></param>
    public async Task<bool> StartGameSession(GameMode mode, string sessionName)
    {
        //OnStartedRunnerConnection?.Invoke();

        //_networkRunner.AddCallbacks(this);
        // _networkRunner.ProvideInput = true;

        const string SCENE_NAME = "OnlineCharacterSelection";

        //SceneRef characterSelectionScene = _networkRunner.SceneManager.GetSceneRef(SCENE_NAME);
        SceneRef characterSelectionScene = SceneRef.FromIndex(6);

        var startGameArgs = new StartGameArgs()
        {
            GameMode = mode,
            SessionName = sessionName,
            PlayerCount = ConstantValues.MAX_PLAYERS_PER_GAME,
            Address = NetAddress.Any(),

            SceneManager = _networkRunner.GetComponent<INetworkSceneManager>(),
            //ObjectPool = _networkRunner.GetComponent<ObjectPoolingManager>(),

            Scene = characterSelectionScene,

        };


        Debug.Log($"[PHOTON][CUSTOM][START_GAME] - Starting game session");
        StartGameResult startGameResult = await _networkRunner.StartGame(startGameArgs);

        if (startGameResult.Ok)
        {

        }
        else
        {
            Debug.LogError($"Failed to start: {startGameResult.ShutdownReason}");
        }

        return startGameResult.Ok;

    }

    #endregion START GAME

    #region NETWORK CALLBACKS

    public void AddNetworkRunnerCallbacks(INetworkRunnerCallbacks newNetRunnerCallbacks)
    {
        _networkRunner.AddCallbacks(newNetRunnerCallbacks);
    }

    public void RemoveNetworkRunnerCallbacks(INetworkRunnerCallbacks netRunnerCallbacksToRemove)
    {
        _networkRunner.RemoveCallbacks(netRunnerCallbacksToRemove);
    }

    #endregion NETWORK CALLBACKS

    public void SetNickname(string nickName)
    {
        _playerNickname = nickName;

        Debug.Log($"[NICKNAME] - setting nickname: {PlayerNickname}");
    }

    public bool TrySpawnNetworkRunner()
    {
        if (_networkRunner == null)
        {
            _networkRunner = Instantiate(_networkRunnerPrefab);
            DontDestroyOnLoad(_networkRunner.gameObject);

            _networkEvents = _networkRunner.GetComponent<NetworkEvents>();

            return true;
        }

        return false;
    }

    public bool DestroyNetworkRunner()
    {
        if (_networkRunner != null)
        {
            _networkRunner.Shutdown(true, ShutdownReason.Ok);
            _networkRunner = null;
            _networkEvents = null;

            return true;
        }

        return false;
    }

    private void OnDisable()
    {
        _initialized = false;
    }

}
