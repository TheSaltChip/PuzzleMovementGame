using Puzzle.Scriptables;
using UnityEngine;
using UnityEngine.Events;
using Util.PRS;

namespace Puzzle.SpawnPos
{
    public class SpawnPuzzlePieces : MonoBehaviour
    {
        [SerializeField] private SelectedImage selectedImage;
        [SerializeField] private GoalSprite goalSprite;
        [SerializeField] private PuzzlePiecesSpawnPoints spawnPoints;
        [SerializeField] private GameObject puzzlePiecePrefab;

        [SerializeField] private PuzzleBoardDimensions puzzleBoardDimensions;
        [SerializeField] private PuzzleImageQuads puzzleImageQuads;

        public UnityEvent changedImage;

        private GameObject[] pieces;

        public void SetUpPieces()
        {
            if (pieces != null)
            {
                foreach (var piece in pieces)
                {
                    Destroy(piece);
                }
            }

            var height = puzzleBoardDimensions.Height;
            var width = puzzleBoardDimensions.Width;

            puzzleImageQuads.quads = new Quad[height * width];
            puzzleImageQuads.rearrangedQuads = new Quad[height * width];

            pieces = new GameObject[height * width];

            var tex = selectedImage.currentSelected;

            var ppcb = tex.width / width;
            var ppch = tex.height / height;

            var spawnLayers = new[]
            {
                spawnPoints.GetLayerCopy(0),
                spawnPoints.GetLayerCopy(1),
                spawnPoints.GetLayerCopy(2),
            };

            var av = 0;

            for (var col = 0; col < height; col++)
            for (var row = 0; row < width; row++)
            {
                var q = new Quad
                {
                    rows = new int[ppch, ppcb]
                };

                puzzleImageQuads.quads[row + col * width] = q;

                var p = ppch * tex.width * col + row * ppcb;

                for (var l = 0; l < ppch; l++)
                {
                    var x = p + l * tex.width;
                    for (var m = 0; m < ppcb; m++)
                    {
                        q.rows[l, m] = x + m;
                    }
                }

                var rect = new Rect(row * (tex.width / width), col * (tex.height / height),
                    tex.width / width, tex.height / height);

                var piece = Instantiate(puzzlePiecePrefab);
                piece.name = av.ToString();
                pieces[av++] = piece;

                var tr = piece.transform;

                if (false)
                {
                    var randomLayer = Random.Range(0, 3);
                    var spawnLayer = spawnLayers[randomLayer];
                    var randomPos = Random.Range(0, spawnLayer.Count);
                    
                    var posRotScl = spawnLayer[randomPos];
                    var randomAngleOffset = Random.Range(0, 4);

                    posRotScl.rotation.z *= randomAngleOffset;

                    tr.SetFromPosRotScl(posRotScl);
                    tr.parent = gameObject.transform;
                    spawnLayer.RemoveAt(randomPos);
                }
                else
                {
                    tr.localScale = spawnLayers[0][0].scale;
                    tr.parent = gameObject.transform;
                    tr.position = new Vector3(0, 1.2f + row * 0.1f, 0.25f - col * 0.15f);
                }

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


            var sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(.5f, .5f));
            goalSprite.image = sprite;
            changedImage.Invoke();
        }
    }
}