using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PhotonInputHandler : MonoBehaviour
{

    private NetworkEvents _networkEvents;

    private LocalPlayerGameplayInputHandler _playerGameplayInputHandler;

    private void Awake()
    {
        _playerGameplayInputHandler = GetComponent<LocalPlayerGameplayInputHandler>();
    }

    private void Start()
    {
        _networkEvents = NetworkRunnerManager.Instance.NetworkEvents;
        _networkEvents.OnInput.AddListener(OnInput);

        PlayerInput playerInput = GetComponent<PlayerInput>();
        _playerGameplayInputHandler.Initialize(playerInput);
    }

    private void Update()
    {
        _playerGameplayInputHandler.ProcessInput();
    }

    private void OnInput(NetworkRunner runner, NetworkInput input)
    {
        if (_playerGameplayInputHandler == null)
            return;

        PhotonGameplayInputData photonInputData = ConvertLocalInputDataToPhotonInputData( _playerGameplayInputHandler.GameplayInputData);

        input.Set(photonInputData);

        //Debug.Log($"[FUSION][INPUT][LOCAL] | {photonInputData}");
    }

    /// <summary>
    /// TODO: REfactor into an abstraction.
    /// </summary>
    private PhotonGameplayInputData ConvertLocalInputDataToPhotonInputData(LocalPlayerGameplayInputData localInputData)
    {
        PhotonGameplayInputData photonInputData = new PhotonGameplayInputData();
    
        photonInputData.rotationInput = localInputData.rotationInput;
        photonInputData.movementInput = localInputData.movementInput;
        photonInputData.FireInput = localInputData.FireInput;
        photonInputData.SpecialInput = localInputData.SpecialInput;
        photonInputData.DashInput = localInputData.DashInput;
    
        return photonInputData;
    }

}
