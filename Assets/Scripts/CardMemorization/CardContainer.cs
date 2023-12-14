using UnityEngine;

namespace CardMemorization
{
    [CreateAssetMenu(fileName = "CardContainer", menuName = "CardMem/CardContainer", order = 0)]
    public class CardContainer : ScriptableObject
    {
        public Card[] cards;
        public int position;
    }
}