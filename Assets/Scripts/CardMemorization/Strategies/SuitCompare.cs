using System.Collections.Generic;
using System.Linq;

namespace CardMemorization.Strategies
{
    public class SuitCompare : IRuleCompare
    {
        public bool Match(Card[] cards,int entries,int matches)
        {
            if (entries < matches) return false;

            var suit = cards[0].GetSuit();

            for (var i = 1; i < cards.Count(); i++)
            {
                if (cards[i].GetSuit() != suit) return false;
            }

            return true;
        }
    }
}