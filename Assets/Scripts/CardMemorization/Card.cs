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