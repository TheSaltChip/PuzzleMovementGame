using UnityEngine;

public class Throwable : MonoBehaviour
{
    private Transform _throwableTransform;
    private Vector3 _buildUp;
    private Vector3 _position;
    private Rigidbody _rigidBody;
    private bool _useForce;
    private int _layer;

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _throwableTransform = GetComponent<Transform>();
        _layer = LayerMask.NameToLayer(gameObject.layer.ToString());
    }

    public void Grabbed()
    {
        
    }

    public void Released()
    {
        _useForce = true;
        gameObject.layer = LayerMask.NameToLayer("Hand");
    }

    private void FixedUpdate()
    {
        if (!_useForce) return;
        _useForce = false;
        _rigidBody.AddForce(_rigidBody.mass*_rigidBody.velocity, ForceMode.Impulse);
        gameObject.layer = _layer;
    }
}
