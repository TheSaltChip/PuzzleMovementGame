using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.Serialization;

public class GuideToPoint : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    private Transform _target;
    private bool _track = true;
    private bool _freeRoamed = false;
    private Transform _start;
    [SerializeField] private Transform transform1;

    private float _timeCount;

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    public void TrackingTrue()
    {
        _start = transform;
        _freeRoamed = true;
        _track = true;
    }
    
    public void TrackingFalse()
    {
        _track = false;
    }

    private void Start()
    {
        gameObject.layer = LayerMask.NameToLayer("CompassNeedle");
    }

    private void FixedUpdate()
    {
        if (!_track) return;
        
        if (_freeRoamed)
        {
            
            if (_timeCount >= 1)
            {
                _freeRoamed = false;
            }
            
            
            transform1.LookAt(_target);
            
            _timeCount += Time.fixedDeltaTime;
            
            rb.MoveRotation(Quaternion.Slerp(_start.rotation, transform1.rotation, _timeCount));
        }
        else
        {
            Transform transform2;
            _timeCount = 0;
            (transform2 = transform).LookAt(_target);

            rb.MoveRotation(Quaternion.LookRotation(transform2.forward));
        }
    }
}