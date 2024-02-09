using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCharacterUI : MonoBehaviour
{

    private Player _player;
    private BasePlayerCharacter _character;

    [SerializeField] private TMP_Text _playerName;
    [SerializeField] private Image _healthBarContainer;

    [SerializeField] private Transform _livesContainer;
    private Image[] _remainingLives;

    [Header("Dashes")]
    [SerializeField] private Transform _dashContainer;
    private Image[] _remainingDashes;
    [SerializeField] private Color _dashAvailableColor;
    [SerializeField] private Color _dashUnavailableColor;

    public void SetPlayer(Player player)
    {

        _player = player;
        _character = player.CurrentCharacter;

        _playerName.text = _player.PlayerName != string.Empty ? _player.PlayerName : "UNNAMED";

        SetupHealthBar();

        SetupLivesContainer(player);
        SetupDashContainer();

        gameObject.SetActive(true);

        Debug.Log($"[PlayerCharacterUI] - Setting up player: {player.PlayerName} | _character: {(_character == null ? "null" : "NOT null")} | _player: {(_player == null ? "null" : "NOT null")}");
    }

    private void SetupLivesContainer(Player player)
    {
        _remainingLives = _livesContainer.GetComponentsInChildren<Image>();

        Color lifeColor = LocalGameManager.Instance.GetColorByIndex(player.PlayerIndex);
        foreach (Image life in _remainingLives)
            life.color = lifeColor;

        UpdateLivesContainer(player.CurrentLives, player);
        player.OnPlayerLivesChange += UpdateLivesContainer;
    }

    private void SetupHealthBar()
    {
        _character.HealthHandler.OnHealthChanged.AddListener(UpdateHealthBar);
        UpdateHealthBar(_character.HealthHandler.CurrentHealth, _character.HealthHandler.MaxHealth);
    }

    private void SetupDashContainer()
    {
        _remainingDashes = _dashContainer.GetComponentsInChildren<Image>();

        _character.DashAbility.OnDashPerformed += UpdateDashContainer;
        _character.DashAbility.OnDashRestored += UpdateDashContainer;

        UpdateDashContainer(_character.DashAbility.CurrentAvailableDashes);
    }

    private void UpdateHealthBar(int currentHP, int maxHP)
    {
        float hpPercentage = (float)currentHP / (float)maxHP;
        _healthBarContainer.fillAmount = hpPercentage;
    }

    private void UpdateLivesContainer(int remainingLives, Player player)
    {
        for (int i = 0; i < _remainingLives.Length; i++)
            _remainingLives[i].gameObject.SetActive( i <= remainingLives - 1 );
    }

    private void UpdateDashContainer(int currentDashes)
    {
        for (int i = 0; i < _remainingLives.Length; i++)
            _remainingDashes[i].color = (i <= currentDashes - 1 ? _dashAvailableColor : _dashUnavailableColor);
    }

    public void HidePlayerUI()
    {
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        Debug.Log($"[PlayerCharacterUI][Destroy] - _character: {(_character == null ? "null" : "NOT null")} | _player: {(_player == null ? "null" : "NOT null")}");

        if (_character != null)
        {
            _character.HealthHandler.OnHealthChanged.RemoveListener(UpdateHealthBar);

            _character.DashAbility.OnDashPerformed -= UpdateDashContainer;
            _character.DashAbility.OnDashRestored -= UpdateDashContainer;

        }
        
        if(_player != null)
            _player.OnPlayerLivesChange -= UpdateLivesContainer;
        
    }
}
