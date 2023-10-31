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
        private static readonly int Front = Shader.PropertyToID("_Front");

        public void SetValues(int number, CardSuits suit)
        {
            this.suit = suit;
            this.number = number;
            
            var tex = Resources.Load<Texture2D>($"PlayingCards\\{suit}{number:00}");
                        gameObject.GetComponent<MeshRenderer>().material.SetTexture(Front,tex);
        }

        private void Awake()
        {
            _t = transform;
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
            Completed();
        }

        public override void ResetState()
        {
            transform.rotation = Quaternion.Euler(0,0,180);
            _hasRotated = false;
            OnResetState.Invoke();
        }
    }
}