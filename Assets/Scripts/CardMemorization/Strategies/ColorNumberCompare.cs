using System.Collections.Generic;
using System.Linq;

namespace CardMemorization.Strategies
{
    public class ColorNumberCompare : IRuleCompare
    {
        public bool Match(Card[] cards,int entries,int matches)
        {
            if (entries < matches) return false;

            var firstCardColor = cards[0].GetColor();
            var firstCardNum = cards[0].GetNumber();

            for (var i = 1; i < cards.Count(); i++)
            {
                if (cards[i].GetColor() != firstCardColor || cards[i].GetNumber() != firstCardNum) return false;
            }

            return true;
        }
    }
}