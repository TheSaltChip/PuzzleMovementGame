using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompassCollision : MonoBehaviour
{
    [SerializeField] private SphereCollider col;
    [SerializeField] private GuideToPoint needle;

    private void Start()
    {
        gameObject.layer = LayerMask.NameToLayer("CompassNeedle");
    }

    private void OnTriggerEnter(Collider other)
    {
        needle.TrackingFalse();
    }

    private void OnTriggerExit(Collider other)
    {
        needle.TrackingTrue();
    }
}
