using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionPainter : MonoBehaviour
{
    [SerializeField] private Color paintColor;
    private float strength = 1f;
    private float hardness = 0.5f;
    private float radius = 0.25f;

    private void OnCollisionEnter(Collision other)
    {
        Paintable p = other.collider.GetComponent<Paintable>();
        if (p != null)
        {
            Vector3 pos = other.contacts[0].point;
            PaintManager.Instance.paint(p,pos,radius,hardness,strength,paintColor);
        }
    }
}
