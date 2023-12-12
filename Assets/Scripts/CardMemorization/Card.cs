using System.Linq;
using CardMemorization.Enums;
using Completables;
using Events;
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
        [SerializeField] private UnityEvent cardDeactivated;
        private CardColor _color;

        private bool _hasRotated;
        private static readonly int Front = Shader.PropertyToID("_Front");

        public void SetValues(int number, CardSuit suit, CardColor color)
        {
            this.suit = suit;
            this.number = number;
            _color = color;

            var tex = Resources.Load<Texture2D>($"PlayingCards\\{suit}{number:00}");
            gameObject.GetComponent<MeshRenderer>().material.SetTexture(Front, tex);
        }

        private void Awake()
        {
            var tex = Resources.Load<Texture2D>($"PlayingCards\\{suit}{number:00}");
            gameObject.GetComponent<MeshRenderer>().material.SetTexture(Front, tex);
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
            print("Collision event completed");
        }

        private void Flip()
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            _hasRotated = true;
        }

        public void Deactivate()
        {
            if (!cardContainer.cards.Contains(this)) return;
            Completed();
            gameObject.GetComponent<ReturnToPool>().Return();
            cardDeactivated.Invoke();

        }

        public override void ResetState()
        {
            transform.localRotation = Quaternion.Euler(0, 0, 180);
            _hasRotated = false;
            OnResetState.Invoke();
        }
    }
}