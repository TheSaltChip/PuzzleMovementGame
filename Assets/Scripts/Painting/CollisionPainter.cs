using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollisionPainter : MonoBehaviour
{
    [SerializeField] private Color paintColor;
    [SerializeField] private float strength = 1f;
    [SerializeField] private float hardness = 0.5f;
    [SerializeField] private float radius = 0.25f;
    public UnityEvent collided;

    private void OnCollisionEnter(Collision other)
    {
        Paintable p = other.collider.GetComponent<Paintable>();
        if (p != null)
        {
            print("collided");
            Vector3 pos = other.contacts[0].point;
            PaintManager.Instance.paint(p,pos,radius,hardness,strength,paintColor);
        }
        collided.Invoke();
    }

    public void SetColor(Color color)
    {
        paintColor = color;
    }
}
