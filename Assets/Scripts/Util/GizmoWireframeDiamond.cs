using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Util
{
    public class GizmoWireframeDiamond : MonoBehaviour
    {
        [SerializeField] private float radius = 0.5f;

        private void OnDrawGizmos()
        {
            var half = radius * 0.5f;
            var x = Vector3.right * half;
            var y = Vector3.up * half;
            var z = Vector3.forward * half;
            var position = transform.position;

            Gizmos.color = Color.red;
            Gizmos.DrawLine(position - x, position + x);
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(position - y, position + y);
            Gizmos.color = Color.green;
            Gizmos.DrawLine(position - z, position + z);

            Gizmos.color = Color.white;
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