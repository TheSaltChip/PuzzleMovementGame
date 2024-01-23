using FigureMatching.ScriptableObjects;
using UnityEngine;

namespace FigureMatching
{
    public class FigurePositionsCreator : MonoBehaviour
    {
        [SerializeField] private FigurePositions positions;
        [SerializeField] private FigureSpawnerSettings settings;

        public void Create()
        {
            positions.Clear();
            var deltaTheta = settings.deltaThetaDeg * Mathf.Deg2Rad;
            var deltaPhi = settings.deltaPhiDeg * Mathf.Deg2Rad;
            var radius = settings.radius;

            const float tau = 2f * Mathf.PI;
            const float piDiv2 = Mathf.PI / 2f;
            
            for (var theta = piDiv2; theta > Mathf.PI / 6 + 1e-6f; theta -= deltaTheta)
            {
                for (var phi = 0f; phi < tau - 1e-6f; phi += deltaPhi)
                {
                    var pos = new Vector3(
                        radius * Mathf.Cos(phi) * Mathf.Sin(theta),
                        radius * Mathf.Cos(theta),
                        radius * Mathf.Sin(phi) * Mathf.Sin(theta));
                    positions.Add(pos);
                }
            }
        }
    }
}