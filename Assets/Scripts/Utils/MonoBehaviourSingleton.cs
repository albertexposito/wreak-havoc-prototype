using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoBehaviourSingleton<T> : MonoBehaviour where T : MonoBehaviourSingleton<T>
{
    private static T _instance;
    public static T Instance
    {
        get { return _instance; }
    }

    public static bool IsInitialized
    {
        get { return _instance != null; }
    }

    protected void InitializeSingleton()
    {
        if (_instance != null)
        {
            Debug.LogError($"[MonoBehaviourSingleton -> InitializeSingleton] Trying to instantiate a second instance of a singleton class of type {typeof(T).Name.ToString()}");
        }
        else
        {
            Debug.Log($"[MonoBehaviourSingleton -> InitializeSingleton] {typeof(T).Name.ToString()} enabled successfully");
            _instance = this as T;
            DontDestroyOnLoad( gameObject );
        }
    }

    protected virtual void OnDestroy()
    {
        if (_instance == this)
        {
            Debug.Log($"[MonoBehaviourSingleton -> OnDestroy] {typeof(T).Name.ToString()} destroyed successfully");
            _instance = null;
        }
    }
}
