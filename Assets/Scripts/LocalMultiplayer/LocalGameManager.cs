using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;


[CreateAssetMenu(fileName = "LOCAL GAME MANAGER", menuName = "Managers/Local Game Manager")]
public class LocalGameManager : ScriptableObjectSingleton<LocalGameManager>, IManager
{

    [SerializeField] private Color[] _characterColors;

    public List<IPlayerIdentity> Players { get => _players; }
    private List<IPlayerIdentity> _players;

    private Player _winner;

    // This will have to change dynamically in the future
    public readonly string DEFAULT_STAGE_NAME = "Battlefield"; 

    #region EVENTS

    // SELECTION SCREEN GAME EVENTS
    public event Action<Player> OnPlayerJoinedGame;
    public event Action<Player, int> OnPlayerLeftGame;


    #endregion EVENTS

    public void InitManager()
    {
        if (IsInitialized)
            return;

        InitializeSingleton();

        _players = new List<IPlayerIdentity>(ConstantValues.MAX_PLAYERS_PER_GAME);
    }

    #region CHARACTER SELECTION METHODS

    public void AddPlayer(PlayerInput playerInput)
    {

        if (DoesPlayerAlreadyExist(playerInput))
        {
            Debug.Log("[LocalGameManager] - Player already in game!");
            return;
        }

        Player player = playerInput.GetComponent<Player>();

        player.PlayerIndex = _players.Count;
        player.PlayerName = $"Player {player.PlayerIndex}";
        _players.Add(player);

        //// Adding the player as a child of the Local Manager
        //// so they persist through the scene
        //player.transform.SetParent(transform);
        DontDestroyOnLoad(player);

        OnPlayerJoinedGame?.Invoke(player);
    }

    public void BeginGamePreparation()
    {
        _winner = null;

        SceneManager.LoadScene("LocalGameSetup", LoadSceneMode.Single);
    }

    #endregion CHARACTER SELECTION METHODS


    public void DestroyPlayerGameObjects()
    {
        foreach (Player player in _players)
            Destroy(player.gameObject);
    }


    #region UTILS

    private bool DoesPlayerAlreadyExist(PlayerInput playerInput)
    {
        bool playerFound = false;

        foreach (Player player in _players)
            if (player.IsSamePlayerInput(playerInput))
            {
                playerFound = true;
                break;
            }

        return playerFound;
    }

    public Player GetPlayerFromCharacter(BasePlayerCharacter character)
    {
        Player characterPlayer = null;

        foreach(Player player in _players)
            if(player.CurrentCharacter == character)
            {
                characterPlayer = player;
                break;
            }

        return characterPlayer;

    }

    public Player GetWinnerPlayer()
    {
        if (_winner != null)
            return _winner;

        foreach (Player player in _players)
            if (player.CurrentLives > 0)
            {
                _winner = player;
                break;
            }

        return _winner;
    }

    public Color GetColorByIndex(int index) => _characterColors[index];

    #endregion UTILS
}




//public class LocalGameManager : MonoBehaviour
//{
//    public static LocalGameManager Instance;

//    // ATTRIBUTES
//    [SerializeField] private InputDeviceController _inputDeviceController;

//    private LocalGameplayController _localGameplayController;

//    public List<Player> Players { get => _players; }
//    private List<Player> _players;

//    #region EVENTS

//    // SELECTION SCREEN GAME EVENTS
//    public event Action<Player> OnPlayerJoinedGame;
//    public event Action<Player, int> OnPlayerLeftGame;

//    // LOADING GAME EVENTS
//    public event Action<StageComponents> OnStageLoaded;

//    #endregion EVENTS

//    #region CHARACTER SELECTION METHODS
//    private void Awake()
//    {
//        bool success = InitializeSingleton();

//        if (!success) // This is not the first game manager
//            return;

//        // We want this element to persist between the
//        // local character selection and local game scene
//        DontDestroyOnLoad(gameObject);

//        _players = new List<Player>(_inputDeviceController.MAX_PLAYERS);

//        if(_inputDeviceController != null)
//            _inputDeviceController.OnLocalDeviceDetected += OnPlayerAdded;
//    }

//    private void OnPlayerAdded(PlayerInput playerInput)
//    {
//        Player player = playerInput.GetComponent<Player>();

//        player.SetIndex(_players.Count);
//        _players.Add(player);

//        // Adding the player as a child of the Local Manager
//        // so they persist through the scene
//        player.transform.SetParent(transform);

//        OnPlayerJoinedGame?.Invoke(player);
//    }

//    public void LoadGameScene()
//    {

//        _inputDeviceController.DisablePlayerJoin();

//        // TODO : Get the scene name dynamically
//        SceneManager.LoadScene("LocalGameSetup", LoadSceneMode.Single);
//    }

//    #endregion CHARACTER SELECTION METHODS


//    #region GAME SCENE METHODS

//    public void StartGame()
//    {
//        // TODO - Clean this
//        _localGameplayController = FindObjectOfType<LocalGameplayController>();
//        _localGameplayController.StartCountdown();
//    }

//    public void FinishGame()
//    {
//        // TODO - show Score screen and go back to character selection screen
//    }

//    #endregion GAME SCENE METHODS

//    #region UTILS

//    private bool InitializeSingleton()
//    {

//        if (Instance == null)
//        {
//            Debug.Log($"[Local Game Manager] - Initializing singleton");
//            Instance = this;
//            return true;
//        }
//        else
//        {
//            Destroy(this.gameObject);
//            Debug.LogError($"LocalGameManager duplicated in gameObject: {gameObject}");
//            return false;
//        }
//    }

//    #endregion UTILS
//}
