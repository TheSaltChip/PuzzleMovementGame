using System.Collections.Generic;
using UnityEngine;

namespace Memorization.Figures
{
    [CreateAssetMenu(menuName = "Memorization/Figure/Figures", fileName = "Figures", order = 0)]
    public class Figures : ScriptableObject
    {
        public List<GameObject> list;
    }
}