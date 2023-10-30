using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using Level.Completables;
using Unity.VisualScripting;
using UnityEngine;

namespace DefaultNamespace
{
    public class Card : Completable
    {
        [SerializeField] private CardSuits suit;
        [SerializeField] private int number;
        private Transform _t;

        private bool _hasRotated;
        private Quaternion _origin;
        private static readonly int Front = Shader.PropertyToID("_Front");

        public Card(int number, CardSuits suit)
        {
            this.suit = suit;
            this.number = number;
        }

        private void Awake()
        {
            _t = transform;
            _origin = _t.rotation;
            var tex = Resources.Load<Texture2D>($"PlayingCards\\{suit}{number:00}");
            gameObject.GetComponent<MeshRenderer>().material.SetTexture(Front,tex);
        }

        public CardSuits GetSuit()
        {
            return suit;
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
            transform.rotation = Quaternion.Euler(0,0,0);
            _hasRotated = true;
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
            Completed();
        }

        public override void ResetState()
        {
            transform.rotation = _origin;
            _hasRotated = false;
        }
    }
}