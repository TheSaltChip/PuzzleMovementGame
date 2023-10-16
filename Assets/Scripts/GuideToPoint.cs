using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GuideToPoint : MonoBehaviour
{
    private Transform _target;
    private bool _track = true;
    private Transform _origin;

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    public void ChangeTrackingState()
    {
        _track = !_track;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        _origin = transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(_track ? _target : _origin);
    }
}
