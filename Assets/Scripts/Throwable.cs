using System.Collections;
using UnityEngine;

public class Throwable : MonoBehaviour
{
    private Transform _throwableTransform;
    private Vector3 _buildUp;
    private Vector3 _position;
    private Rigidbody _rigidBody;
    private bool _useForce;
    private int _layer;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _throwableTransform = GetComponent<Transform>();
        var l = LayerMask.NameToLayer(gameObject.layer.ToString());
    }

    public void Released()
    {
        _useForce = true;
        gameObject.layer = LayerMask.NameToLayer("Hand");
        var i =  Wait();
        gameObject.layer = _layer;
    }

    private static IEnumerable Wait()
    {
        yield return new WaitForSeconds(0.5f);
    }

    private void FixedUpdate()
    {
        if (!_useForce) return;
        _useForce = false;
        _rigidBody.AddForce(_rigidBody.mass*_rigidBody.velocity, ForceMode.Impulse);
        
    }
}
