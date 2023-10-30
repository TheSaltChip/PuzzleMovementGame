using System.Collections.Generic;

namespace DefaultNamespace
{
    public interface IRuleCompare
    {
        public bool Match(List<Card> cards);
    }
}