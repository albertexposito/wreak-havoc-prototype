using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptableObjectSingleton<T> : ScriptableObject where T : ScriptableObjectSingleton<T>
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
            Debug.LogError($"[ScriptableObjectSingleton -> EnableManager] Trying to instantiate a second instance of a singleton class of type {typeof(T).Name.ToString()}");
        }
        else
        {
            Debug.Log($"[ScriptableObjectSingleton -> EnableManager] {typeof(T).Name.ToString()} enabled successfully");
            _instance = this as T;
        }
    }

    protected virtual void OnDestroy()
    {
        if (_instance == this)
        {
            Debug.Log($"[ScriptableObjectSingleton -> OnDestroy] {typeof(T).Name.ToString()} destroyed successfully");
            _instance = null;
        }
    }
}
