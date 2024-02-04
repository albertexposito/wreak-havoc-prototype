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
    private LocalGameInitializer _gameInitializer;

    private void Awake()
    {
        _targetGroup = GetComponent<CinemachineTargetGroup>();

        _gameInitializer = FindObjectOfType<LocalGameInitializer>();

        _gameInitializer.OnLevelLoaded += OnLevelLoaded;
        _gameInitializer.OnPlayersSpawned += OnPlayersSpawned;
    }

    private void OnLevelLoaded(StageComponents stageComponents)
    {

        Transform stageCameraPosition = stageComponents.CameraCenterPosition;
        _virtualCamera.transform.SetPositionAndRotation(stageCameraPosition.position, stageCameraPosition.rotation);
        _virtualCamera.ForceCameraPosition(stageCameraPosition.position, stageCameraPosition.rotation);
    }

    private void OnPlayersSpawned(List<Player> players)
    {
        foreach (Player player in players)
            _targetGroup.AddMember(player.CurrentCharacter.transform, TARGET_GROUP_MEMBER_WEIGHT, TARGET_GROUP_MEMBER_RADIUS);
    }

    private void OnDestroy()
    {
        _gameInitializer.OnPlayersSpawned -= OnPlayersSpawned;
    }
}
