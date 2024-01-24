using System.Collections.Generic;
using UnityEngine;

namespace Memorization.Figure.ScriptableObjects
{
    [CreateAssetMenu(menuName = "FigureMatching/Figures", fileName = "Figures", order = 0)]
    public class Figures : ScriptableObject
    {
        public List<GameObject> list;

        public GameObject RandomObject()
        {
            return list[Random.Range(0, list.Count)];
        }

        public List<GameObject> RandomFigures(int num)
        {
            if (num >= list.Count) return list;

            var l = new List<GameObject>(num);

            var usedNumbers = new List<int>(num);
            var index = -1;
            usedNumbers.Add(index);
            
            for (var i = 0; i < num; i++)
            {
                while (usedNumbers.Contains(index))
                {
                    index = Random.Range(0, num);
                }
                usedNumbers.Add(index);

                l.Add(list[index]);
            }

            return l;
        }

        public GameObject this[int i] => list[i];
    }
}