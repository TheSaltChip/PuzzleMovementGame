using UnityEngine;

namespace Memorization.Figure.ScriptableObjects
{
    [CreateAssetMenu(fileName = "FigureSpawnerSettings", menuName = "Memorization/Figure/FigureSpawnerSettings")]
    public class FigureSpawnerSettings : ScriptableObject
    {
        public float deltaThetaDeg;
        public float deltaPhiDeg;
        public float radius;
    }
}