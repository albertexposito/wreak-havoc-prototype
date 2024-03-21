using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayCameraController : MonoBehaviour
{

    // TODO: Get the IGameInitializer interface
    [SerializeField] private PhotonGameInitializer _gameInitializer;

    [SerializeField] private GameplayCameraHandler _cameraHandler;

    private void Awake()
    {
        _gameInitializer.OnPlayerSpawned += OnCharacterSpawned;
    }

    private void OnCharacterSpawned(IPlayerIdentity player)
    {
        if(player.CurrentCharacter == null)
        {
            StartCoroutine(WaitUntilCharacterIsNotNull(player));
            return;
        }

        Transform transform = player.CurrentCharacter.transform;
        AddTransformToCameraHandler(transform);
    }

    private IEnumerator WaitUntilCharacterIsNotNull(IPlayerIdentity player)
    {
        yield return new WaitUntil(() => player.CurrentCharacter != null);

        OnCharacterSpawned(player);
    }

    public void AddTransformToCameraHandler(Transform transformToAdd)
    {
        _cameraHandler.AddTransform(transformToAdd);
    }
    
    public void RemoveTransformFromCameraHandler(Transform transformToAdd)
    {
        // TODO create remove transform logic.
    }

    private void OnDestroy()
    {
        _gameInitializer.OnPlayerSpawned -= OnCharacterSpawned;
    }

}
