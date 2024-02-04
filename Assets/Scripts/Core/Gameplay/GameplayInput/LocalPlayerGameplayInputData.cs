using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public struct LocalPlayerGameplayInputData
{

    public Vector2 movementInput;
    public Vector2 rotationInput;

    public bool DashInput;
    public bool FireInput;
    public bool SpecialInput;

    /// <summary>
    /// For debugging purposes.
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        string moveX = string.Format("{0:0.00}", movementInput.x);
        string moveY = string.Format("{0:0.00}", movementInput.y);
        string rotateX = string.Format("{0:0.00}", rotationInput.x);
        string rotateY = string.Format("{0:0.00}", rotationInput.y);

        string inputData = $"Move: ({moveX},{moveY}) | Rotate: ({rotateX},{rotateY}) | Dash: {DashInput} | Fire: {FireInput} | Special: {SpecialInput}";
        
        return inputData;
    }

}
