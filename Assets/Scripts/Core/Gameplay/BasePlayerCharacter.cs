using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;

public class BasePlayerCharacter : MonoBehaviour
{

    public string CharacterStateName { get => _characterStateMachine.CurrentStateName; }

    public UnityEvent<BasePlayerCharacter> OnPlayerDied;

    private BaseCharacterStateMachine _characterStateMachine;

    public CharacterModelHandler CharacterModelHandler { get => _characterModelHandler; }
    private CharacterModelHandler _characterModelHandler;

    public LocalPlayerGameplayInputHandler PlayerInputHandler { get => _playerInputHandler; }
    private LocalPlayerGameplayInputHandler _playerInputHandler;

    public BaseCharacterController CharacterController { get => _characterController; }
    private BaseCharacterController _characterController;

    public BaseRangedAttack RangedAttackController { get => _rangedAttackController; }
    private BaseRangedAttack _rangedAttackController;

    public BaseMeleeAttack MeleeAttackController { get => _meleeAttackController; }
    private BaseMeleeAttack _meleeAttackController;

    public DashAbility DashAbility { get => _dashAbility; }
    private DashAbility _dashAbility;

    public DamageableElement DamageableElement { get => _damageableElement; }
    private DamageableElement _damageableElement;

    public HealthHandler HealthHandler { get => _healthHandler; }
    private HealthHandler _healthHandler;

    private void Awake()
    {
        _characterController = GetComponent<BaseCharacterController>();
        _playerInputHandler = GetComponent<LocalPlayerGameplayInputHandler>();
        _characterModelHandler = GetComponentInChildren<CharacterModelHandler>();

        _damageableElement = GetComponent<DamageableElement>();
        _healthHandler = GetComponent<HealthHandler>();

        // ABILITIES
        _meleeAttackController = GetComponentInChildren<BaseMeleeAttack>();
        _rangedAttackController = GetComponentInChildren<BaseRangedAttack>();
        _dashAbility = GetComponent<DashAbility>();

        // STATE MACHINE
        _characterStateMachine = GetComponent<BaseCharacterStateMachine>();
        _characterStateMachine.InitializeStateMachine(this);
    }

    public void InitializePlayer(Player localPlayer)
    {
        if(localPlayer != null && localPlayer.PlayerInput)
            _playerInputHandler.Initialize(localPlayer.PlayerInput);

        _characterModelHandler.SetMeshColor(localPlayer != null ? localPlayer.PlayerIndex : 0);

        _rangedAttackController.Initialize(this);
        _meleeAttackController.Initialize(this);
        _dashAbility.Initialize(this);

        _damageableElement.OnDamageTaken.AddListener(OnDamageTaken);

        _healthHandler.OnEntityDied.AddListener(PlayerDied);

    }

    public void OnDamageTaken(DamageData damageData)
    {
        _characterStateMachine.HandleDamageTaken(damageData);
    }

    private void PlayerDied(DamageData damageData)
    {
        Debug.Log($"{gameObject.name} died");

        _characterStateMachine.ChangeState(_characterStateMachine.DeadState);

        OnPlayerDied?.Invoke(this);
    }

    public void SpawnPlayer(Transform respawnPoint, float spawnWaitTime = 0)
    {
        Debug.Log($"Respawning player: {gameObject.name}");

        StartCoroutine(SpawnPlayerCoroutine(respawnPoint, spawnWaitTime));
    }

    private IEnumerator SpawnPlayerCoroutine(Transform respawnPoint, float spawnWaitTime)
    {
        if(spawnWaitTime != 0)
            yield return new WaitForSeconds(spawnWaitTime);

        _characterStateMachine.ChangeState(_characterStateMachine.SpawningState);
        _characterController.SetPositionAndRotationFromTransform(respawnPoint);

    }

    public void ActivateCharacter()
    {
        _characterStateMachine.ChangeState(_characterStateMachine.NeutralState);
    }

    // This method is called before update and gets the input
    public void ProcessInput()
    {
        if(_playerInputHandler.Initialized)
            _playerInputHandler.ProcessInput();
    }

    // This method is in charge of updating the position of the character
    // regardless if its local or networked
    // It should ba calling a Character controller
    public void CharacterUpdate()
    {
        _characterStateMachine.UpdateStateMachineLogic();
    }

}
