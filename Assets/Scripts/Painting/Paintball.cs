using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Paintball : MonoBehaviour
{
    public delegate void CollisionEvent(Paintball Paintball, Collision Collision);

    public event CollisionEvent OnCollision;
}
