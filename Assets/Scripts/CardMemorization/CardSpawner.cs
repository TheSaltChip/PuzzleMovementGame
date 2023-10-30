using UnityEngine;
using UnityEngine.Rendering;

namespace DefaultNamespace
{
    public class CardSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject card;
        [SerializeField] private int width; //Amount of cards in the width
        [SerializeField] private int height; //Amount of cards in the height
        private ObjectPool<GameObject> _cards;

        private void GenerateCards()
        {
            
        }
    }
}