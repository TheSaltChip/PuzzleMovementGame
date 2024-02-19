using System.Collections;
using CardMemorization.Enums;
using CardMemorization.Strategies;
using UnityEngine;

namespace CardMemorization
{
    public class CardCompareManager : MonoBehaviour
    {

        [SerializeField] private CardRule rule;
        [SerializeField] private int amountToMatch;
        [SerializeField] private CardContainer cardContainer;

        private int _arrayPos;
        private IRuleCompare _ruleCompare;

        private void Awake()
        {
            cardContainer.position = 0; //Due to editor
            _ruleCompare = new AllNumberCompare();
            cardContainer.cards = new Card[amountToMatch];
        }

        public int GetAmountToMatch()
        {
            return amountToMatch;
        }

        public CardRule GetCardRule()
        {
            return rule;
        }

        public void SetCompareRule(IRuleCompare newRule)
        {
            _ruleCompare = newRule;
        }

        private void ClearCards()
        {
            cardContainer.cards = new Card[amountToMatch];
            cardContainer.position = 0;
        }

        private void DeactivateCards()
        {
            foreach (var card in cardContainer.cards)
            {
                card.Deactivate();
            }
            ClearCards();
        }

        private void ResetCards()
        {
            foreach (var card in cardContainer.cards)
            {
                card.ResetState();
            }
            ClearCards();
        }

        public void Compare()
        {
            if (cardContainer.position < cardContainer.cards.Length) return;
            StartCoroutine(Delay());
        }

        private IEnumerator Delay()
        {
            yield return new WaitForSeconds(1);
            if (!_ruleCompare.Match(cardContainer.cards,cardContainer.position,amountToMatch))
            {
                ResetCards();
            }
            else
            {
                DeactivateCards();
            }
            
        }
    }
}