using System.Collections.Generic;

namespace CardMemorization.Strategies
{
    public interface IRuleCompare
    {
        public bool Match(Card[] cards,int entries,int matches);
    }
}