using CardMemorization.Enums;
using Completables;
using UnityEngine;

namespace CardMemorization
{
    public class Card : Completable
    {
        [SerializeField] private CardSuit suit;
        [SerializeField] private int number;
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
            CardCompareManager.Instance.Compare(this);
        }

        public void Flip()
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            _hasRotated = true;
        }

        public void Deactivate()
        {
            Completed();
        }

        public override void ResetState()
        {
            transform.localRotation = Quaternion.Euler(0, 0, 180);
            _hasRotated = false;
            OnResetState.Invoke();
        }
    }
}