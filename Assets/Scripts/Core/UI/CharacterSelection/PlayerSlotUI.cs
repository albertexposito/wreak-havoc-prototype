using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerSlotUI : MonoBehaviour
{

    [SerializeField] private GameObject availableSlotGO;
    [SerializeField] private GameObject unavailableSlotGO;

    [SerializeField] private Image _slotContainer;

    [SerializeField] private TMP_Text _playerSlotTitle;
    [SerializeField] private TMP_Text _playerName;

    // Using an GameObject is a simpler approach than a Toggle Component
    [SerializeField] private GameObject _readyToggle; 

    private int _slotIndex;
    IPlayerIdentity _playerIdentity;

    [Header("Visual configuration")]
    [SerializeField] private Color _readyColor;
    private Color _standardColor;

    public void Initialize(int index)
    {
        _slotIndex = index;

        _playerSlotTitle.text = $"Player {_slotIndex + 1}";

        availableSlotGO.SetActive(true);
        unavailableSlotGO.SetActive(false);

        _standardColor = _slotContainer.color;

        SetReadyState(false);
        
    }

    public void SetPlayerToSlot(IPlayerIdentity newPlayer)
    {
        // This shouldn't be called, but is controlled anyway
        if(_playerIdentity != null)
            RemovePlayerFromSlot(_playerIdentity);

        availableSlotGO.SetActive(false);
        unavailableSlotGO.SetActive(true);

        //InputDevice device = newPlayer.playerInput.GetDevice<InputDevice>();
        //_deviceName.text = device.displayName;
        
        SetNameToPlayerSlot(newPlayer.PlayerName);
        SetReadyState(newPlayer.IsReady);

        _playerIdentity = newPlayer;
        _playerIdentity.OnNameChanged += SetNameToPlayerSlot;
        _playerIdentity.OnReadyStateChanged += SetReadyState;
    }

    public void RemovePlayerFromSlot(IPlayerIdentity playerToRemove)
    {
        _playerSlotTitle.text = $"Player {_slotIndex + 1}";

        availableSlotGO.SetActive(true);
        unavailableSlotGO.SetActive(false);

        _playerIdentity.OnNameChanged -= SetNameToPlayerSlot;
        _playerIdentity = null;
    }

    private void SetNameToPlayerSlot(string name) => _playerName.text = name;

    private void SetReadyState(bool isReady)
    {
        _readyToggle.SetActive(isReady);
        _slotContainer.color = isReady ? _readyColor : _standardColor;
    }

    private void OnDestroy()
    {
        if(_playerIdentity != null)
        {
            _playerIdentity.OnNameChanged -= SetNameToPlayerSlot;
            _playerIdentity.OnReadyStateChanged -= SetReadyState;
        }
    }

}
