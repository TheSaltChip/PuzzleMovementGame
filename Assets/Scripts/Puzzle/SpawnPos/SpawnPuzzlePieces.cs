using Puzzle.Scriptables;
using UnityEngine;
using UnityEngine.Events;

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
            var tex = selectedImage.currentSelected;
            var av = 0;
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
            var k = 0;
            pieces = new GameObject[height * width];
            var ppcb = tex.width / width;
            var ppch = tex.height / height;
            var col = 0;

            for (var i = 0; i < height; i++)
            {
                var row = 0;
                for (int j = 0; j < width; j++)
                {
                    var q = new Quad();
                    q.rows = new int[ppch, ppcb];
                    puzzleImageQuads.quads[k] = q;
                    var p = ppch * tex.width * col + row * ppcb;
                    for (var l = 0; l < ppch; l++)
                    {
                        var x = p + l * tex.width;
                        for (var m = 0; m < ppcb; m++)
                        {
                            q.rows[l, m] = x + m;
                        }
                    }

                    row++;
                    k++;

                    var rect = new Rect(j * (tex.width / width), i * (tex.height / height),
                        tex.width / width, tex.height / height);
                    var piece = Instantiate(puzzlePiecePrefab);
                    piece.name = av + "";
                    pieces[av] = piece;
                    av++;
                    var tr = piece.transform;
                    tr.parent = gameObject.transform;
                    tr.position = new Vector3(0, 1.2f + j * 0.1f, 0.25f - i * 0.15f);
                    piece.SetActive(true);


                    var sliced = new Texture2D((int)rect.width, (int)rect.height)
                    {
                        filterMode = tex.filterMode
                    };
                    sliced.SetPixels(0, 0, (int)rect.width, (int)rect.height,
                        tex.GetPixels((int)rect.x, (int)rect.y, (int)rect.width, (int)rect.height));
                    sliced.Apply();
                    piece.GetComponent<Renderer>().materials[0].SetTexture(PuzzleShaderVariables.BaseMap, sliced);
                }

                col++;
            }
            
            var sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(.5f, .5f));
            goalSprite.image = sprite;
            changedImage.Invoke();
        }
    }
}