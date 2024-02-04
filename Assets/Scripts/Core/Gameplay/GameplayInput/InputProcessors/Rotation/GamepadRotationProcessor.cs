using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GamepadRotationProcessor : BaseInputProcessor<Vector2>
{

    public override Vector2 ProcessInput(InputAction inputAction)
    {
        Vector2 inputVector = inputAction.ReadValue<Vector2>();

        return inputVector;
    }

}
