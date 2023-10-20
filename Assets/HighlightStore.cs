using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightStore : MonoBehaviour
{
    public static HighlightStore Instance { get; private set; }
    private static Material _highlight;

    private void Start()
    {
        _highlight = gameObject.GetComponent<Renderer>().material;
    }

    public static Material GetMaterial()
    {
        return _highlight;
    }
}
