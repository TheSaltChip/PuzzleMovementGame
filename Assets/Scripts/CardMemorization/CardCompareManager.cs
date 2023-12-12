using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CardMemorization.Enums;
using CardMemorization.Strategies;
using UnityEngine;
using UnityEngine.Events;

namespace CardMemorization
{
    public class CardCompareManager : MonoBehaviour
    {

        [SerializeField] private CardRule rule;
        [SerializeField] private int amountToMatch;
        [SerializeField] private CardContainer cardContainer;
        [SerializeField] private UnityEvent missMatch;
        [SerializeField] private UnityEvent matched;

        private int _arrayPos;
        private IRuleCompare _ruleCompare;
        private int _clearedCards;

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
            _clearedCards = 0;
        }

        private void ResetCards()
        {
            ClearCards();
        }

        public void ResetCardsEvent()
        {
            if (_clearedCards < cardContainer.cards.Length)
            {
                _clearedCards++;
            }
            else
            {
                ClearCards();
            }
        }

        public void Compare()
        {
            if (cardContainer.position-1 < cardContainer.cards.Length) return;
            StartCoroutine(Delay());
        }

        private IEnumerator Delay()
        {
            yield return new WaitForSeconds(1);
            if (!_ruleCompare.Match(cardContainer.cards,cardContainer.position,amountToMatch))
            {
                missMatch.Invoke();
                ResetCards();
            }
            else
            {
                matched.Invoke();
            }
            
        }
    }
}