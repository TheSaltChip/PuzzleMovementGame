using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Puzzle.PuzzleUI
{
    [CreateAssetMenu(menuName = "Puzzle/JigsawImages", fileName = "JigsawImages")]
    public class JigsawImages : ScriptableObject, IEnumerable<Sprite>
    {
        public List<Sprite> images;
        
        public IEnumerator<Sprite> GetEnumerator()
        {
            return images.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public Sprite GetRandomImage()
        {
            return images[Random.Range(0, images.Count)];
        }
    }
}