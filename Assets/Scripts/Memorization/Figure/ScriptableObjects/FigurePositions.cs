using System.Collections.Generic;
using UnityEngine;

namespace Memorization.Figure.ScriptableObjects
{
    [CreateAssetMenu(fileName = "FigurePositions", menuName = "Memorization/Figure/FigurePositions", order = 0)]
    public class FigurePositions : ScriptableObject
    {
        public List<Vector3> list;

        public void Add(Vector3 item)
        {
            list.Add(item);
        }

        public void Clear()
        {
            list.Clear();
        }

        public List<Vector3> Copy()
        {
            return new List<Vector3>(list);
        }
    }
}