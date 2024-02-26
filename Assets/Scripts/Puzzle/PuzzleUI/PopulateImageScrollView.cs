using System;
using UnityEngine;
using UnityEngine.UI;

namespace Puzzle.PuzzleUI
{
    public class PopulateImageScrollView : MonoBehaviour
    {
        [SerializeField] private GameObject buttonPrefab;
        [SerializeField] private JigsawImages jigsawImages;

        private void Awake()
        {
            foreach (var image in jigsawImages)
            {
                var g = Instantiate(buttonPrefab, transform);

                g.GetComponent<Image>().sprite = image;
                g.GetComponent<ChosenImage>().SetImage(image);
            }
        }
    }
}