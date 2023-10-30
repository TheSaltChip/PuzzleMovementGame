using System.Collections.Generic;

namespace DefaultNamespace
{
    public class AllNumberCompare : IRuleCompare
    {
        public bool Match(List<Card> cards)
        {
            if (cards.Count < 2)
            {
                return false;
            }
            var i = cards[0].GetNumber();
            for (var j = 1; j < cards.Count; j++)
            {
                if (cards[j].GetNumber() != i)
                {
                    return false;
                }
            }
            return true;
        }
    }
}