using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    private void Start()
    {
        Debug.Log($"Not destroying: {gameObject.name}");
        DontDestroyOnLoad(gameObject);
    }
}
