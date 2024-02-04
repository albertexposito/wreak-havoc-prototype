using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LocalPlayer : Player
{
    public PlayerInput playerInput;
    public GameObject localPlayerGameObject;

    //public LocalPlayer(string id, string name, PlayerInput playerInput) : base(id, name)
    //{
    //    this.playerInput = playerInput;
    //    localPlayerGameObject = playerInput.gameObject;
    //}

}
