#region License
// Copyright (C) 2024 Sebastian Misje Jonassen & Mathias Nupen
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the Commons Clause License version 1.0 with GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// Commons Clause License and GNU General Public License for more details.
// 
// You should have received a copy of the Commons Clause License and GNU General Public License
// along with this program.  If not, see <https://commonsclause.com/> and <https://www.gnu.org/licenses/>.
#endregion

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