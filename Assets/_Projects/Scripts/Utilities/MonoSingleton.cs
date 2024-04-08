﻿using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    private static T _instance = null;
    public static T Instance
    {
        get
        {
            // Instance required for the first time, we look for it
            if (_instance != null) return _instance;
            
            _instance = GameObject.FindObjectOfType(typeof(T)) as T;

            // Object not found, we create a temporary one
            if (_instance == null)
            {
                Debug.LogWarning("No instance of " + typeof(T).ToString() + ", a temporary one is created.");

                _instance = new GameObject("Temp Instance of " + typeof(T).ToString(), typeof(T)).GetComponent<T>();

                // Problem during the creation, this should not happen
                if (_instance == null)
                {
                    Debug.LogError("Problem during the creation of " + typeof(T).ToString());
                }
            }

            _instance.Initiate();
            return _instance;
        }
    }

    private void Awake()
    {
        // if (m_Instance == null)
        // {
            _instance = this as T;
            if (_instance != null) _instance.Initiate();
            // }
        // else if (m_Instance != this)
        // {
        //     Debug.LogError("Another instance of " + GetType() + " is already exist! Destroying self...");
        //     Destroy(this.gameObject);
        //     return;
        // }
    }


    /// <summary>
    /// This function is called when the instance is used the first time
    /// Put all the initializations you need here, as you would do in Awake
    /// </summary>
    protected virtual void Initiate() { }

    /// Make sure the instance isn't referenced anymore when the user quit, just in case.
    private void OnApplicationQuit()
    {
        _instance = null;
    }
}