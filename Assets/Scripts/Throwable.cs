using UnityEngine;

public class Throwable : MonoBehaviour
{
    private Transform _throwableTransform;
    private Vector3 _buildUp;
    private Vector3 _position;
    private Rigidbody _rigidBody;
    private bool _useForce;

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _throwableTransform = GetComponent<Transform>();
    }

    public void Grabbed()
    {
        
    }

    public void Released()
    {
        _useForce = true;
    }

    void FixedUpdate()
    {
        if (_useForce)
        {
            _useForce = false;
            _rigidBody.AddForce(_rigidBody.mass*_rigidBody.velocity, ForceMode.Impulse);
        }
    }
}
