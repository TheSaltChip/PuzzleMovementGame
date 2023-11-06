using System.Collections.Generic;

namespace CardMemorization.Strategies
{
    public class SuitCompare : IRuleCompare
    {
        public bool Match(List<Card> cards)
        {
            if (cards.Count < 2) return false;

            var suit = cards[0].GetSuit();

            for (var i = 1; i < cards.Count; i++)
            {
                if (cards[i].GetSuit() != suit) return false;
            }

            return true;
        }
    }
}