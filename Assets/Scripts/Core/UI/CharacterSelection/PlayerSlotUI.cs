using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSlotUI : MonoBehaviour
{

    [SerializeField] private GameObject availableSlotGO;
    [SerializeField] private GameObject unavailableSlotGO;

    private int _slotIndex;

    [SerializeField] private TMP_Text _playerSlotTitle;
    [SerializeField] private TMP_Text _deviceName;

    public void Initialize(int index)
    {
        _slotIndex = index;

        _playerSlotTitle.text = $"Player {_slotIndex + 1}";

        availableSlotGO.SetActive(true);
        unavailableSlotGO.SetActive(false);
    }

    public void SetPlayerToSlot(Player newPlayer)
    {
        availableSlotGO.SetActive(false);
        unavailableSlotGO.SetActive(true);

        //InputDevice device = newPlayer.playerInput.GetDevice<InputDevice>();
        //_deviceName.text = device.displayName;
        _deviceName.text = $"Player {newPlayer.PlayerIndex}";
    }

    public void RemovePlayerFromSlot(Player newPlayer)
    {
        availableSlotGO.SetActive(true);
        unavailableSlotGO.SetActive(false);
    }

}
