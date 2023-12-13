using System.Collections.Generic;
using System.Linq;

namespace CardMemorization.Strategies
{
    public class SuitNumberCompare : IRuleCompare
    {
        public bool Match(Card[] cards,int entries,int matches)
        {
            if (entries < matches) return false;

            var suit = cards[0].GetSuit();
            var num = cards[0].GetNumber();
            
            for (var i = 1; i < cards.Count(); i++)
            {
                if (cards[i].GetSuit() != suit) return false;

                if (cards[i].GetNumber() != num) return false;
            }

            return true;
        }
    }
}