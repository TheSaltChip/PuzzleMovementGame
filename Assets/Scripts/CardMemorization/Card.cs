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

using CardMemorization.Enums;
using Completables;
using UnityEngine;
using UnityEngine.Events;
using Util;

namespace CardMemorization
{
    public class Card : Completable
    {
        [SerializeField] private CardSuit suit;
        [SerializeField] private int number;
        [SerializeField] private CardContainer cardContainer;
        [SerializeField] private UnityEvent cardFlipped;
        [SerializeField] private ReturnToPool returnToPool;
        [SerializeField] private MeshRenderer meshRenderer;
        private CardColor _color;

        private bool _hasRotated;
        private static readonly int Front = Shader.PropertyToID("_Front");
        private static readonly int Back = Shader.PropertyToID("_Back");

        public void SetValues(int number, CardSuit suit, CardColor color, Texture2D cardBack)
        {
            this.suit = suit;
            this.number = number;
            _color = color;

            var tex = Resources.Load<Texture2D>($"Images/Cards/{suit}{number:00}");

            var material = meshRenderer.material;
            
            material.SetTexture(Front, tex);
            material.SetTexture(Back,cardBack);
        }

        public CardSuit GetSuit()
        {
            return suit;
        }

        public CardColor GetColor()
        {
            return _color;
        }

        public int GetNumber()
        {
            return number;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (_hasRotated) return;
            if (cardContainer.position >= cardContainer.cards.Length) return;
            Flip();
            cardContainer.cards[cardContainer.position] = this;
            cardContainer.position++;
            cardFlipped.Invoke();
        }

        private void Flip()
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            _hasRotated = true;
        }

        public void Deactivate()
        {
            Completed();
            returnToPool.Return();
        }

        public override void ResetState()
        {
            transform.localRotation = Quaternion.Euler(0, 0, 180);
            _hasRotated = false;
            OnResetState.Invoke();
        }
    }
}