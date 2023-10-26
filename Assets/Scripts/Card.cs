using UnityEngine;

namespace DefaultNamespace
{
    public class Card : MonoBehaviour
    {
        [SerializeField] private string suit;
        [SerializeField] private int number;

        public string GetSuit()
        {
            return suit;
        }

        public int GetNumber()
        {
            return number;
        }
    }
}