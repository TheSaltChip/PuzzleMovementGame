using UnityEngine;
using UnityEngine.Serialization;

namespace Puzzle.Scriptables
{
    [CreateAssetMenu(fileName = "SelectedSprite", menuName = "Puzzle/SelectedSprite")]
    public class SelectedSprite : ScriptableObject
    {
        public Sprite sprite;

        public Texture2D GetTexture2D()
        {
            return sprite.texture;
        }
    }
}