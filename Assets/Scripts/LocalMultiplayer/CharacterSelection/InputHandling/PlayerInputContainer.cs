using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputContainer : MonoBehaviour
{

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

}
