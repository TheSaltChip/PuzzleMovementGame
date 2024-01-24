using System.Collections.Generic;
using System.Linq;

namespace CardMemorization.Strategies
{
    public class ColorCompare : IRuleCompare
    {
        public bool Match(Card[] cards,int entries,int matches)
        {
            if (entries < matches) return false;
            var firstCardColor = cards[0].GetColor();

            for (var i = 1; i < cards.Count(); i++)
            {
                if (cards[i].GetColor() != firstCardColor) return false;
            }

            return true;
        }
    }
}