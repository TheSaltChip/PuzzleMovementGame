using System.Collections;
using UnityEngine;

public class Throwable : MonoBehaviour
{
    private Vector3 _buildUp;
    private Vector3 _position;
    private Rigidbody _rigidBody;
    private bool _useForce;
    private int _layer;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _layer = LayerMask.NameToLayer("Throwable");
        gameObject.layer = _layer;
    }

    public void Released()
    {
        _useForce = true;
        gameObject.layer = LayerMask.NameToLayer("Hand");
        StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        for (var i = 0; i < 6; i++)
        {
            yield return null;
        }
        
        gameObject.layer = _layer;
    }

    private void FixedUpdate()
    {
        if (!_useForce) return;
        _useForce = false;
        _rigidBody.AddForce(_rigidBody.velocity * (_rigidBody.mass * 1.1f), ForceMode.Impulse);
        
    }
}
