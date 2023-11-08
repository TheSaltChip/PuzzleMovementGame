using System.Collections.Generic;

namespace CardMemorization.Strategies
{
    public interface IRuleCompare
    {
        public bool Match(List<Card> cards);
    }
}