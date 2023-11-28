using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ColorTracker : MonoBehaviour
{
    [SerializeField] private Color color;
    public UnityEvent<Color> press;

    private void OnCollisionEnter(Collision other)
    {
        press.Invoke(color);
    }
}
