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

using System.Collections.Generic;
using System.IO;
using CardMemorization.Enums;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;
using Util;
using Random = UnityEngine.Random;

namespace CardMemorization
{
    public class CardSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject cardPrefab;

        [SerializeField, Tooltip("Amount of cards in the width")]
        private int width;

        [SerializeField, Tooltip("Amount of cards in the height")]
        private int height;

        [SerializeField] private Transform spawnPoint;

        [SerializeField] private UnityEvent spawnNew;
        [SerializeField] private Texture2D cardBack;

        private DirectoryInfo _info;

        private ObjectPool<GameObject> _cardsPool;
        private List<(int, CardSuit)> _cardNumberAndSuitList;
        private CardRule _generationRule;
        private int _matchAmount;
        private static readonly int Back = Shader.PropertyToID("_Back");

        private Texture2D _cardFronts;
        private static readonly int Front = Shader.PropertyToID("_Front");

        private void Awake()
        {
            _info = new DirectoryInfo(Application.dataPath + "/ImportedAssets/Playing Cards/Image/PlayingCards");
            var ccm = gameObject.GetComponent<CardCompareManager>();
            _generationRule = ccm.GetCardRule();
            _matchAmount = ccm.GetAmountToMatch();
            _cardsPool = new ObjectPool<GameObject>(CreateToPool, GetFromPool, OnReleaseToPool, DestroyFromPool);
            _cardNumberAndSuitList = new List<(int, CardSuit)>(52);
            GenerateCards();
            PlaceCards();
        }

        private GameObject CreateToPool()
        {
            GameObject temp = Instantiate(cardPrefab, transform, true);

            var ret = temp.AddComponent<ReturnToPool>();
            ret.pool = _cardsPool;

            var c = temp.GetComponent<Card>();
            c.OnDone.AddListener(ret.Return);

            return temp;
        }

        private void GetFromPool(GameObject obj)
        {
            obj.SetActive(true);
        }

        private void OnReleaseToPool(GameObject obj)
        {
            obj.SetActive(false);
        }

        private void DestroyFromPool(GameObject obj)
        {
            Destroy(obj);
        }

        private void GenerateCards()
        {
            var cardAmount = width * height;

            _cardNumberAndSuitList.Clear();


            for (var i = 0; i < cardAmount / _matchAmount; i++)
            {
                var rnd = Mathf.FloorToInt(Random.Range(1, 14));
                var cardSuit = (CardSuit)(i % 4);
                for (var k = 0; k < _matchAmount; k++)
                {
                    _cardNumberAndSuitList.Add((rnd, cardSuit));
                }
            }

            _cardNumberAndSuitList.Shuffle();
        }

        private void PlaceCards()
        {
            spawnNew.Invoke();
            
            var localScale = cardPrefab.transform.localScale;
            var mat = cardPrefab.GetComponent<MeshFilter>().sharedMesh.bounds.size;
            localScale.Scale(mat);

            var paddingSide = 0.02f;
            var paddingTop = 0.035f;

            var boardX = width * localScale.x + width * paddingSide;
            var boardZ = height * localScale.z + height * paddingTop;

            var stepHeight = paddingTop + localScale.z;
            var stepWidth = paddingSide + localScale.x;

            var limitWidth = boardX / 2f - (localScale.x / 2f + paddingSide);
            var limitHeight = boardZ / 2f - (localScale.z / 2f + paddingTop);

            var j = 0;

            for (var x = -limitWidth; x < limitWidth + 0.1f; x += stepWidth)
            {
                for (var z = -limitHeight; z < limitHeight + 0.1f; z += stepHeight)
                {
                    var newCard = _cardsPool.Get();
                    newCard.GetComponent<Card>()
                        .SetValues(_cardNumberAndSuitList[j].Item1, _cardNumberAndSuitList[j].Item2,
                            GetColorFromSuit(_cardNumberAndSuitList[j].Item2),cardBack);
                    //var tex = newCard.GetComponent<MeshRenderer>().material;
                    //tex.SetTexture(Front, Resources.Load<Texture2D>("Images/Cards/" + _cardNumberAndSuitList[j].Item2 + "" + _cardNumberAndSuitList[j].Item1.ToString("00")));
                    //tex.SetTexture(Back, cardBack);
                    
                    var y = spawnPoint.localScale.y / 2f + 0.0001f + spawnPoint.localPosition.y;

                    newCard.transform.SetLocalPositionAndRotation(new Vector3(x, y, z), Quaternion.Euler(0, 0, 180));

                    j++;
                }
            }
        }

        private static CardColor GetColorFromSuit(CardSuit suit)
        {
            switch (suit)
            {
                case CardSuit.Club:
                case CardSuit.Spade:
                    return CardColor.Black;
                case CardSuit.Diamond:
                case CardSuit.Heart:
                default:
                    return CardColor.Red;
            }
        }
    }

    public static class Rand
    {
        private static readonly System.Random Rng = new();

        public static void Shuffle<T>(this IList<T> list)
        {
            var n = list.Count;
            while (n > 1)
            {
                n--;
                var k = Rng.Next(n + 1);
                (list[k], list[n]) = (list[n], list[k]);
            }
        }
    }
}