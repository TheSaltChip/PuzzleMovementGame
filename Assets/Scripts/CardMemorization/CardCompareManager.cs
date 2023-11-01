using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class CardCompareManager : MonoBehaviour
    {
        public static CardCompareManager Instance { get; private set; }
        [SerializeField] private CardRules rule;
        private List<Card> _selectedCards;
        [SerializeField] private int amountToMatch;
        private int _arrayPos;
        private IRuleCompare _ruleCompare;

        private void Awake()
        {
            Instance = this;
            _ruleCompare = new AllNumberCompare();
            _selectedCards = new List<Card>();
        }

        public int GetAmountToMatch()
        {
            return amountToMatch;
        }

        public CardRules GetCardRule()
        {
            return rule;
        }

        public void SetCompareRule(IRuleCompare newRule)
        {
            _ruleCompare = newRule;
        }

        public void Compare(Card card)
        {
            if (_selectedCards.Contains(card))
            {
                return;
            }

            if (_selectedCards.Count >= amountToMatch) return;

            _selectedCards.Add(card);
            card.Flip();
            if (_selectedCards.Count != amountToMatch)
            {
                return;
            }

            StartCoroutine(Delay(card));
        }

        private IEnumerator Delay(Card card)
        {
            yield return new WaitForSeconds(1);
            if (!_ruleCompare.Match(_selectedCards))
            {
                foreach (var c in _selectedCards)
                {
                    c.ResetState();
                }

                _selectedCards.Clear();
                yield break;
            }

            foreach (var c in _selectedCards)
            {
                c.Deactivate();
            }

            _selectedCards.Clear();
        }
    }
}