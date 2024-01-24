using System.Collections.Generic;
using UnityEngine;

namespace Memorization.Figure.ScriptableObjects
{
    [CreateAssetMenu(menuName = "FigureMatching/FigureMaterials", fileName = "FigureMaterials")]
    public class FigureMaterials : ScriptableObject
    {
        public List<Material> list;

        public Material RandomObject()
        {
            return list[Random.Range(0, list.Count)];
        }

        public List<Material> RandomMaterials(int num)
        {
            if (num >= list.Count) return list;

            var l = new List<Material>(num);

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
    }
}