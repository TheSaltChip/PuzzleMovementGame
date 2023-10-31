using System;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    public class CardSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject card;
        [SerializeField] private int width; //Amount of cards in the width
        [SerializeField] private int height; //Amount of cards in the height
        [SerializeField] private Transform spawnPoint;
        private ObjectPool<GameObject> _cards;

        private void Start()
        {
            _cards = new ObjectPool<GameObject>(CreateToPool, GetFromPool, OnReleaseToPool, DestroyFromPool);
            GenerateCards();
        }

        private GameObject CreateToPool()
        {
            GameObject temp = Instantiate(card, transform, true);
            var ret = temp.AddComponent<ReturnToPool>();
            var c = temp.GetComponent<Card>();
            c.OnDone.AddListener(ret.Return);
            ret.pool = _cards;
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
            var cards = new List<(int, CardSuits)>();

            for (var i = 0; i < cardAmount / 2; i++)
            {
                var rnd = Mathf.FloorToInt(Random.Range(1, 14));
                var cardSuit = (CardSuits)(i % 4);
                cards.Add((rnd, cardSuit));
                cards.Add((rnd, cardSuit));
                print(i);
            }

            cards.Shuffle();

            var localScale = card.transform.localScale;
            var mat = card.GetComponent<MeshFilter>().sharedMesh.bounds.size;
            localScale.Scale(mat);
            var paddingSide = 0.02f;
            var paddingTop = 0.035f;
            var boardX = width * localScale.x + ((width ) * paddingSide);
            var boardZ = height * localScale.z + ((height) * paddingTop);
            
            var stepHeight = paddingTop + localScale.z;
            var stepWidth = paddingSide + localScale.x;

            var limitWidth = boardX / 2f - (localScale.x / 2 + paddingSide);
            var limitHeight = boardZ / 2f - (localScale.z / 2 + paddingTop);

            var j = 0;

            for (var x = -limitWidth; x < limitWidth + 0.1f; x += stepWidth)
            {
                for (var z = -limitHeight; z < limitHeight + 0.1f; z += stepHeight)
                {
                    var newCard = _cards.Get();
                    //Instantiate(card, new Vector3(0, 0, 0), Quaternion.identity);
                    newCard.GetComponent<Card>().SetValues(cards[j].Item1, cards[j].Item2);
                    var pos = spawnPoint.localPosition;
                    newCard.transform.SetLocalPositionAndRotation(
                        new Vector3(x, spawnPoint.localScale.y / 2 + 0.0001f + pos.y, z), Quaternion.identity);
                    newCard.transform.rotation = Quaternion.Euler(0, 0, 180);
                    j++;
                }
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

    public class ReturnToPool : MonoBehaviour
    {
        public IObjectPool<GameObject> pool;

        public void Return()
        {
            // Return to the pool

            if (!gameObject.activeSelf) return;
            pool.Release(gameObject);
        }
    }
}