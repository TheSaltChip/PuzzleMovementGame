using UnityEngine;

namespace Tutorial
{
    [CreateAssetMenu(fileName = "MaterialContainer", menuName = "Tutorial/MaterialContainer", order = 0)]
    public class MaterialContainer : ScriptableObject
    {
        public Material material;
    }
}