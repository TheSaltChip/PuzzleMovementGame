namespace CardMemorization.Strategies
{
    public class AllNumberCompare : IRuleCompare
    {
        public bool Match(Card[] cards,int entries,int matches)
        {
            if (entries < matches) return false;

            var i = cards[0].GetNumber();

            for (var j = 1; j < cards.Length; j++)
            {
                if (cards[j].GetNumber() != i) return false;
            }

            return true;
        }
    }
}