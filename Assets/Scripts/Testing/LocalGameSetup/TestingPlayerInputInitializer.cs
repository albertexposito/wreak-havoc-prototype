using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingPlayerInputInitializer : MonoBehaviour
{

    public Player PlayerKeyboard { get => _playerKeyboard; }
    public Player PlayerGamepad { get => _playerGamepad; }

    [SerializeField] private Player _playerKeyboard;
    [SerializeField] private Player _playerGamepad;

}
