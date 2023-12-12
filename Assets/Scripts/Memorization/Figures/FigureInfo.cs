using UnityEngine;

namespace Memorization.Figures
{
    public class FigureInfo : MonoBehaviour
    {
        [field: SerializeField] public Color Color1 { get; set; }
        [field: SerializeField] public string Shape { get; set; }
    }
}