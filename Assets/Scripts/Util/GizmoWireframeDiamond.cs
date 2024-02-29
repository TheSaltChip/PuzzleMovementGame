using UnityEngine;

namespace Util
{
    public class GizmoWireframeDiamond : MonoBehaviour
    {
        [SerializeField] private float radius = 0.5f;

        private void OnDrawGizmos()
        {
            var half = radius * 0.5f;
            var transform1 = transform;
            var rotation = transform1.rotation;

            var x = rotation * (Vector3.right) * half;
            var y = rotation * (Vector3.up) * half;
            var z = rotation * (Vector3.forward) * half;
            var position = transform1.localPosition;

            Gizmos.color = Color.red;
            Gizmos.DrawLine(position, position + x);
            Gizmos.color = Color.white;
            Gizmos.DrawLine(position - x, position);
            
            Gizmos.color = Color.green;
            Gizmos.DrawLine(position, position + y);
            Gizmos.color = Color.white;
            Gizmos.DrawLine(position - y, position);
            
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(position, position + z);
            Gizmos.color = Color.white;
            Gizmos.DrawLine(position - z, position);

            Gizmos.DrawLine(position + x, position + z);
            Gizmos.DrawLine(position + x, position - z);
            Gizmos.DrawLine(position - x, position + z);
            Gizmos.DrawLine(position - x, position - z);

            Gizmos.DrawLine(position + y, position + z);
            Gizmos.DrawLine(position + y, position - z);
            Gizmos.DrawLine(position + y, position - x);
            Gizmos.DrawLine(position + y, position + x);

            Gizmos.DrawLine(position - y, position + z);
            Gizmos.DrawLine(position - y, position - z);
            Gizmos.DrawLine(position - y, position - x);
            Gizmos.DrawLine(position - y, position + x);
        }
    }
}