using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class BaseInputProcessor<U>
{

    public abstract U ProcessInput(InputAction inputAction);

}
