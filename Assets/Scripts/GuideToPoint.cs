using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEngine;

public class GuideToPoint : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    private Transform _target;
    private bool _track = true;

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    public void ChangeTrackingState()
    {
        _track = !_track;
    }

    private void Start()
    {
        gameObject.layer = LayerMask.NameToLayer("CompassNeedle");
    }

    private void FixedUpdate()
    {
        Transform transform1;
        
        (transform1 = transform).LookAt(_target);
        
        rb.MoveRotation(Quaternion.LookRotation(transform1.forward));
    }
}