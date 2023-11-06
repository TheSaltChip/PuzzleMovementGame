using System.Collections.Generic;

namespace CardMemorization.Strategies
{
    public class ColorCompare : IRuleCompare
    {
        public bool Match(List<Card> cards)
        {
            if (cards.Count < 2) return false;
            var firstCardColor = cards[0].GetColor();

            for (var i = 1; i < cards.Count; i++)
            {
                if (cards[i].GetColor() != firstCardColor) return false;
            }

            return true;
        }
    }
}