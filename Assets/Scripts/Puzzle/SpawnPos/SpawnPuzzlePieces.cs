﻿using Puzzle.Scriptables;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using Util.PRS;
using Random = UnityEngine.Random;

namespace Puzzle.SpawnPos
{
    public class SpawnPuzzlePieces : MonoBehaviour
    {
        [FormerlySerializedAs("selectedImage")] [SerializeField] private SelectedSprite selectedSprite;
        [SerializeField] private GoalSprite goalSprite;
        [SerializeField] private PuzzlePiecesSpawnPoints spawnPoints;
        [SerializeField] private GameObject puzzlePiecePrefab;

        [SerializeField] private PuzzleBoardDimensions puzzleBoardDimensions;

        public UnityEvent changedImage;

        private GameObject[] pieces;
        private int nextPieceIndex;

        private void Awake()
        {
            pieces = new GameObject[25];
            nextPieceIndex = 0;

            for (var i = 0; i < 25; i++)
            {
                var piece = Instantiate(puzzlePiecePrefab);
                piece.SetActive(false);
                pieces[i] = piece;
            }
        }

        public void SetUpPieces()
        {
            foreach (var piece in pieces)
            {
                piece.SetActive(false);
            }

            nextPieceIndex = 0;

            var height = puzzleBoardDimensions.Height;
            var width = puzzleBoardDimensions.Width;

            var tex = selectedSprite.GetTexture2D();

            var spawnLayers = new[]
            {
                spawnPoints.GetLayerCopy(0),
                spawnPoints.GetLayerCopy(1),
                spawnPoints.GetLayerCopy(2),
            };

            for (var col = 0; col < height; col++)
            for (var row = 0; row < width; row++)
            {
                var rect = new Rect(row * (tex.width / width), col * (tex.height / height),
                    tex.width / width, tex.height / height);

                var piece = pieces[nextPieceIndex];
                piece.name = nextPieceIndex.ToString();
                piece.SetActive(true);
                ++nextPieceIndex;

                var tr = piece.transform;


                var randomLayer = Random.Range(0, 3);
                var spawnLayer = spawnLayers[randomLayer];
                var randomPos = Random.Range(0, spawnLayer.Count);

                var posRotScl = spawnLayer[randomPos];
                var randomAngleOffset = Random.Range(0, 4);

                posRotScl.rotation.z *= randomAngleOffset;
                tr.SetParent(null);
                tr.SetFromPosRotScl(posRotScl);
                tr.SetParent(gameObject.transform);
                spawnLayer.RemoveAt(randomPos);


                var sliced = new Texture2D((int)rect.width, (int)rect.height)
                {
                    filterMode = tex.filterMode
                };
                sliced.SetPixels(0, 0, (int)rect.width, (int)rect.height,
                    tex.GetPixels((int)rect.x, (int)rect.y, (int)rect.width, (int)rect.height));
                sliced.Apply();
                piece.GetComponent<Renderer>().materials[0].SetTexture(PuzzleShaderVariables.BaseMap, sliced);

                piece.SetActive(true);
            }


            goalSprite.image = selectedSprite.sprite;
            changedImage.Invoke();
        }
    }
}