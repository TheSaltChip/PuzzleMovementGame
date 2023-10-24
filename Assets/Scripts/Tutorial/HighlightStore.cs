using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightStore : MonoBehaviour
{
    public static HighlightStore Instance { get; private set; }
    private  Material _highlight;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _highlight = gameObject.GetComponent<Renderer>().material;
    }

    public Material GetMaterial()
    {
        return _highlight;
    }
}
