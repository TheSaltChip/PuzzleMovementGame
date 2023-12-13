using System.Collections.Generic;
using UnityEngine;

namespace Memorization.Figure.ScriptableObjects
{
    [CreateAssetMenu(fileName = "SelectedFigures", menuName = "Memorization/Figure/SelectedFigures")]
    public class SelectedFigures : ScriptableObject
    {
        public FigureMatchingRules rules;
        public List<FigureInfo> list;

        public FigureInfo this[int i] => list[i];

        public void Add(FigureInfo item)
        {
            list.Add(item);
        }

        public bool Contains(FigureInfo item)
        {
            return list.Contains(item);
        }

        public bool Matches()
        {
            var item = list[0];

            for (var i = 1; i < list.Count; i++)
            {
                if (!item.Equals(list[i], rules.matchingRule)) return false;

                item = list[i];
            }

            return true;
        }
        
        public void DeleteAndClear()
        {
            foreach (var figureInfo in list)
            {
                Destroy(figureInfo);
            }
            list.Clear();
        }

        public void ReenableColliderAndClear()
        {
            foreach (var figureInfo in list)
            {
                figureInfo.EnableCollider();
            }
    
            list.Clear();
        }
    }
}