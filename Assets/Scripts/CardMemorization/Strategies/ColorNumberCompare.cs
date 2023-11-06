using System.Collections.Generic;

namespace CardMemorization.Strategies
{
    public class ColorNumberCompare : IRuleCompare
    {
        public bool Match(List<Card> cards)
        {
            if (cards.Count < 2) return false;

            var firstCardColor = cards[0].GetColor();
            var firstCardNum = cards[0].GetNumber();

            for (var i = 1; i < cards.Count; i++)
            {
                if (cards[i].GetColor() != firstCardColor || cards[i].GetNumber() != firstCardNum) return false;
            }

            return true;
        }
    }
}