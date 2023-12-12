using System.Collections.Generic;
using UnityEngine;

namespace Memorization.Figures
{
    [CreateAssetMenu(menuName = "Memorization/Figure/FigureMaterials", fileName = "FigureMaterials")]
    public class FigureMaterials : ScriptableObject
    {
        public List<Material> list;
    }
}