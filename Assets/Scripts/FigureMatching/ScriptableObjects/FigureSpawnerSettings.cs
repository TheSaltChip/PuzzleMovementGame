using UnityEngine;

namespace FigureMatching.ScriptableObjects
{
    [CreateAssetMenu(fileName = "FigureSpawnerSettings", menuName = "FigureMatching/FigureSpawnerSettings")]
    public class FigureSpawnerSettings : ScriptableObject
    {
        public float deltaThetaDeg;
        public float deltaPhiDeg;
        public float radius;
    }
}