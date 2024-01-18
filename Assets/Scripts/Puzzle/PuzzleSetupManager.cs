using System;
using System.Collections;
using System.Collections.Generic;
using Autohand;
using Events;
using Puzzle.Scriptables;
using Unity.Mathematics;
using Unity.VisualScripting;
using Unity.XR.PXR;
using UnityEngine;
using UnityEngine.Events;
using Variables;

public class PuzzleSetupManager : MonoBehaviour
{
    [SerializeField] private ComputeShader comp;
    [SerializeField] private IntVariable height;
    [SerializeField] private IntVariable width;
    
    [SerializeField] private GameObject puzzlePiece;
    [SerializeField] private GameObject board;
    [SerializeField] private GameObject placePoint;
    [SerializeField] private SelectedImage selectedImage;
    [SerializeField] private GoalSprite goalSprite;
    
    [SerializeField] private Placed placed;

    [SerializeField] private UnityEvent changedImage;

    
    private Vector3 scale;
    private Vector3 ppSize;//place point size
    private GameObject[] points;
    private GameObject[] pieces;
    private int available;
    private int av;
    private Vector3 bSize;//board size
    private Texture2D tex;
    private int prevPlaced;
    private int[,] indexes;
    private int[] indPos;
    private Quad[] rearrangedQuads;
    private Quad[] quads;
    private struct Quad
    {
        public int[,] rows;
    }
    
    private static readonly int BaseMap = Shader.PropertyToID("_BaseMap");
    private static readonly int Image = Shader.PropertyToID("image");
    private static readonly int Ri = Shader.PropertyToID("ri");//render image in compute shader
    private static readonly int Result = Shader.PropertyToID("Result");
    private static readonly int Quads = Shader.PropertyToID("quads");
    private static readonly int RearrangedQuads = Shader.PropertyToID("rearrangedQuads");

    private void Awake()
    {
        ppSize = placePoint.GetComponent<Renderer>().bounds.size;
        bSize = board.GetComponent<Renderer>().bounds.size;
        bSize.y = bSize.x;
        ppSize.y = ppSize.x;
        ppSize *= 7.46f;
        scale = board.transform.localScale;
        SetUp();
    }

    public void SetUp()
    {
        IndexArray();
        ScaleBoard();
        PlacePoints();
        SetUpPieces();
        GoalSprite();
    }

    private void IndexArray()
    {
        tex = selectedImage.currentSelected;
        indexes = new int[tex.height,tex.width];
        var a = 0;
        for (var i = 0; i < tex.height; i++)
        {
            for (var j = 0; j < tex.width; j++)
            {
                indexes[i,j] = a;
                a++;
            }
        }
    }

    private void PlacePoints()
    {
        var trpp = placePoint.transform;//transform place point
        var pos = trpp.localPosition;
        var grid = new Vector3[height.value,width.value];
        if (points != null)
        {
            foreach (var point in points)
            {
                Destroy(point);
            }
        }
        
        points = new GameObject[height.value * width.value];

        var originVector = new Vector3();
        available = 0;
        
        if (height.value % 2 == 0)
        {
            originVector.y = -ppSize.y * (height.value / 2)+ppSize.y / 2;
        }
        else
        {
            originVector.y = - ppSize.y * (height.value / 2);
        }

        if (width.value % 2 == 0)
        {
            originVector.x = -ppSize.x * (width.value / 2)+ppSize.x / 2;
        }
        else
        {
            originVector.x = -ppSize.x * (width.value / 2);
        }
        
        
        
        for (var i = 0; i < height.value; i++)
        {
            for (var j = 0; j < width.value; j++)
            {
                grid[i, j] = new Vector3(originVector.x + j*ppSize.x,originVector.y + i*ppSize.y,pos.z);
            }
        }

        var a = 0;
        indPos = new int[width.value * height.value];
        
        for (var i = available - width.value; i >= 0; i -= width.value)
        {
            for (var j = 0; j < width.value; j++)
            {
                indPos[a] = i + j;
                a++;
            }
        }
        
        for (var i = 0; i < height.value; i++)
        {
            for (var j = 0; j < width.value; j++)
            {
                var obj = Instantiate(placePoint);
                points[available] = obj;
                obj.SetActive(true);
                obj.name = available + "";
                var tr = obj.transform;
                tr.SetParent(gameObject.transform);
                tr = obj.transform;
                tr.localScale = trpp.localScale;
                var p = tr.up * grid[i, j].y;
                tr.localPosition = p + (tr.right * grid[i, j].x);
                tr.rotation = trpp.rotation;
                available++;
            }
        }
    }

    private void ScaleBoard()
    {
        var y = (height.value - 1) * scale.y;
        var x = (width.value - 1) * scale.x;
        
        board.transform.localScale = new Vector3(scale.x+x,scale.y+y,scale.z);
    }

    private void SetUpPieces()
    {
        IndexArray();
        tex = selectedImage.currentSelected;
        av = 0;
        if (pieces != null)
        {
            foreach (var piece in pieces)
            {
                Destroy(piece);
            }
        }

        quads = new Quad[height.value * width.value];
        rearrangedQuads = new Quad[height.value * width.value];
        var k = 0;
        pieces = new GameObject[height.value * width.value];
        var ppcb = tex.width / width.value;
        var ppch = tex.height / height.value;
        var col = 0;

        for (var i = 0; i < height.value; i++)
        {
            var row = 0;
            for (int j = 0; j < width.value; j++)
            {
                var q = new Quad();
                q.rows = new int[ppch, ppcb];
                quads[k] = q;
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
                var rect = new Rect(j * (tex.width / (width.value * 1f)), i * (tex.height / (height.value * 1f)),
                    tex.width / (width.value * 1f), tex.height / (height.value * 1f));
                var piece = Instantiate(puzzlePiece);
                piece.name = av + "";
                pieces[av] = piece;
                av++;
                var tr = piece.transform;
                tr.parent = gameObject.transform;
                tr.position = new Vector3(1 + i, 1, j);
                tr.localScale = puzzlePiece.transform.localScale;
                piece.SetActive(true);

                var sliced = new Texture2D((int)rect.width, (int)rect.height)
                {
                    filterMode = tex.filterMode
                };
                sliced.SetPixels(0, 0, (int)rect.width, (int)rect.height,
                    tex.GetPixels((int)rect.x, (int)rect.y, (int)rect.width, (int)rect.height));
                sliced.Apply();
                piece.GetComponent<Renderer>().materials[0].SetTexture(BaseMap, sliced);
            }

            col++;
        }
    }

    private void ShuffleIndex()
    {
        print(placed.number);
        var pos = Int32.Parse(placed.number);
        var point = points[pos];
        var child = point.transform.GetChild(0);
        var pieceQuad = Int32.Parse(child.name);
        var quad = indPos[pos];
        rearrangedQuads[quad] = quads[pieceQuad];
        print("shuffle completed");
    }

    public void CompareImage()
    {
        if (placed.amount != (height.value * width.value))
        {
            ShuffleIndex();
            return;
        }
            
        var compTex = new Texture2D(tex.width,tex.height)
        {
            filterMode = tex.filterMode
        };
        
        var quadsFlat = new int[tex.height * tex.width];
        var rQuadsFlat = new int[tex.height * tex.width];
        var pos = 0;

        for (var i = 0; i < quads.Length; i++)
        {
            for (var j = 0; j < tex.height/height.value; j++)
            {
                for (var l = 0; l < tex.width/width.value; l++)
                {
                    quadsFlat[pos] = quads[i].rows[j,l];
                    rQuadsFlat[pos] = rearrangedQuads[i].rows[j,l];
                    pos++;
                }
            }
        }

        var squares = new GraphicsBuffer(GraphicsBuffer.Target.Structured, height.value * width.value,sizeof(float));
        squares.SetData(quadsFlat);
        
        var rearrangedSquares = new GraphicsBuffer(GraphicsBuffer.Target.Structured,height.value*width.value,sizeof(float));
        rearrangedSquares.SetData(rQuadsFlat);
        
        var sprite = Sprite.Create(compTex, new Rect(0,0,compTex.width,compTex.height), new Vector2(.5f,.5f));
        goalSprite.image = sprite;
        changedImage.Invoke();

        var result = new int[tex.width * tex.height];
        var buffer = new GraphicsBuffer(GraphicsBuffer.Target.Structured,tex.width*tex.height,sizeof(float));
        
        var rTex  = new RenderTexture(tex.width, tex.height, 0)
        {
            enableRandomWrite = true
        };
        
        Graphics.Blit(tex,rTex);

        comp.SetTexture(0,Image,rTex);
        comp.SetBuffer(0,Result,buffer);
        comp.SetBuffer(0,Quads,squares);
        comp.SetBuffer(0,RearrangedQuads,rearrangedSquares);

        var k = new int();
        comp.GetKernelThreadGroupSizes(k,out var x,out var y,out var z);
        comp.Dispatch(k,Mathf.CeilToInt((float)tex.width/x),Mathf.CeilToInt((float)tex.height/y),(int)z);
        buffer.GetData(result);
        
        buffer.Release();
        squares.Release();
        rearrangedSquares.Release();
        foreach (var res in result)
        {
            print(res);
        }
    }

    private void GoalSprite()
    {
        tex = selectedImage.currentSelected;
        var sprite = Sprite.Create(tex, new Rect(0,0,tex.width,tex.height), new Vector2(.5f,.5f));
        goalSprite.image = sprite;
        changedImage.Invoke();
    }

    [Tooltip("width,height")]
    public void SetParameters(string val)
    {
        var s = val.Split(',');
        width.value = Int32.Parse(s[0]);
        height.value = Int32.Parse(s[1]);
        SetUp();
    }

}
