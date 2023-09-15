using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    public static DontDestroy Instance { get; private set; }
    private void Start()
    {
        
        if (Instance != null)
        {
            Debug.LogWarning(
                $"Invalid configuration. Duplicate Instances found! First one: {Instance.name} Second one: {name}. Destroying second one.");
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
