using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GuideToPoint : MonoBehaviour
{
    private Transform _target;
    private bool _track = true;
    private Transform origin;

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
        origin = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (_track)
        {
            transform.LookAt(_target);
        }
        else
        {
            transform.LookAt(origin);
        }
    }
}
