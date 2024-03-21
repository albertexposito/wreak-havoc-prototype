using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayCameraHandler : MonoBehaviour
{

    private const float TARGET_GROUP_MEMBER_WEIGHT = 1;
    private const float TARGET_GROUP_MEMBER_RADIUS = 5;

    [SerializeField] private CinemachineVirtualCamera _virtualCamera;
    private CinemachineTargetGroup _targetGroup;

    [SerializeField] private GameObject _gameInitializerGameObject;
    private IGameInitializer _gameInitializer;

    private void Awake()
    {
        _targetGroup = GetComponent<CinemachineTargetGroup>();

        // Use the gameObject to find the component, apparently you cant use 
        // FindObjectOfType with an interface
        if(_gameInitializerGameObject == null)
        {
            Debug.LogError($"[ERROR][CAMERA] - Set the GameInitializer GameObject in the GameplayCameraHandler!");
            return;
        }

        _gameInitializer = _gameInitializerGameObject.GetComponent<IGameInitializer>();

        _gameInitializer.OnLevelLoaded += OnLevelLoaded;
        _gameInitializer.OnPlayersSpawned += OnPlayersSpawned;
    }

    public void AddTransform(Transform transform) => AddTransformToTargetGroup(transform);

    private void OnLevelLoaded(StageComponents stageComponents)
    {

        Transform stageCameraPosition = stageComponents.CameraCenterPosition;
        _virtualCamera.transform.SetPositionAndRotation(stageCameraPosition.position, stageCameraPosition.rotation);
        _virtualCamera.ForceCameraPosition(stageCameraPosition.position, stageCameraPosition.rotation);
    }

    private void OnPlayersSpawned(List<IPlayerIdentity> players)
    {
        foreach (Player player in players)
        {
            AddTransformToTargetGroup(player.transform);
            player.OnPlayerDied += OnPlayerDied;
        }
    }

    private void AddTransformToTargetGroup(Transform targetTransform)
    {
        _targetGroup.AddMember(targetTransform, TARGET_GROUP_MEMBER_WEIGHT, TARGET_GROUP_MEMBER_RADIUS);
    }

    private void OnPlayerDied(Player player)
    {
        // Remove the player from the camera group
        _targetGroup.RemoveMember(player.CurrentCharacter.transform);
    }

    private void OnDestroy()
    {
        _gameInitializer.OnPlayersSpawned -= OnPlayersSpawned;
    }
}
