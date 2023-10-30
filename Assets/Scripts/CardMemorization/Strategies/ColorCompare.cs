using System.Collections.Generic;

namespace DefaultNamespace
{
    public class ColorCompare : IRuleCompare
    {
        public bool Match(List<Card> cards)
        {
            if (cards.Count < 2)
            {
                return false;
            }
            var color = cards[0].GetSuit();
            if (color is CardSuits.Club or CardSuits.Spade)
            {
                for (var i = 1; i < cards.Count; i++)
                {
                
                    if (cards[i].GetSuit() != CardSuits.Club || cards[i].GetSuit() != CardSuits.Spade)
                    {
                        return false;
                    }
                }
            }
            else
            {
                for (var i = 1; i < cards.Count; i++)
                {
                
                    if (cards[i].GetSuit() != CardSuits.Diamond || cards[i].GetSuit() != CardSuits.Heart)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}