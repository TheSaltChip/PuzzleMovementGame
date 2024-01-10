using System;
using System.Collections;
using System.Collections.Generic;
using Events;
using Puzzle.Scriptables;
using Unity.VisualScripting;
using Unity.XR.PXR;
using UnityEngine;
using UnityEngine.Events;
using Variables;

public class PuzzleSetupManager : MonoBehaviour
{
    [SerializeField] private FloatVariable height;
    [SerializeField] private FloatVariable width;
    
    [SerializeField] private GameObject puzzlePiece;
    [SerializeField] private GameObject board;
    [SerializeField] private GameObject placePoint;
    [SerializeField] private SelectedImage selectedImage;
    [SerializeField] private GoalSprite goalSprite;

    [SerializeField] private UnityEvent changedImage;

    private Vector3 scale;
    private Vector3 ppSize;
    private GameObject[] points;
    private GameObject[] pieces;
    private int available;
    private int av;
    private Vector3 bSize;
    private static readonly int BaseMap = Shader.PropertyToID("_BaseMap");

    private void Awake()
    {
        ppSize = placePoint.GetComponent<Renderer>().bounds.size;
        bSize = board.GetComponent<Renderer>().bounds.size;
        bSize.y = bSize.x;
        ppSize.y = ppSize.x;
        ppSize *= 7.46f;
        scale = board.transform.localScale;
    }

    public void SetUp()
    {
        ScaleBoard();
        PlacePoints();
        SetUpPieces();
        GoalSprite();
    }

    private void PlacePoints()
    {
        
        var trpp = placePoint.transform;
        var pos = trpp.localPosition;
        var grid = new Vector3[(int)height.value,(int)width.value];
        if (points != null)
        {
            foreach (var point in points)
            {
                Destroy(point);
            }
        }
        
        points = new GameObject[(int)height.value * (int)width.value];

        var originVector = new Vector3();
        available = 0;
        
        if (height.value % 2 == 0)
        {
            originVector.y = -ppSize.y * (height.value / 2f)+ppSize.y / 2f;
        }
        else
        {
            originVector.y = - ppSize.y * (height.value / 2);
        }

        if (width.value % 2 == 0)
        {
            originVector.x = -ppSize.x * (width.value / 2f)+ppSize.x / 2f;
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

        for (var i = 0; i < height.value; i++)
        {
            for (var j = 0; j < width.value; j++)
            {
                var obj = Instantiate(placePoint);
                points[available] = obj;
                obj.SetActive(true);
                var tr = obj.transform;
                tr.parent = gameObject.transform;
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

        var texture = selectedImage.currentSelected;
        av = 0;
        if (pieces != null)
        {
            foreach (var piece in pieces)
            {
                Destroy(piece);
            }
        }
        
        pieces = new GameObject[(int)height.value * (int)width.value];
        for (var i = 0; i < height.value; i++)
        {
            for (int j = 0; j < width.value; j++)
            {
                var rect = new Rect(j*(texture.width/(width.value * 1f)),i*(texture.height/(height.value * 1f)),texture.width/(width.value * 1f),texture.height/(height.value * 1f));
                //var sprite = Sprite.Create(texture, rect, new Vector2(.5f, .5f));
                var piece = Instantiate(puzzlePiece);
                pieces[av] = piece;
                av++;
                var tr = piece.transform;
                tr.parent = gameObject.transform;
                tr.position = new Vector3(1+i, 1, j);
                tr.localScale = puzzlePiece.transform.localScale;
                piece.SetActive(true);
                var sliced = new Texture2D((int)rect.width, (int)rect.height)
                {
                    filterMode = texture.filterMode
                };
                sliced.SetPixels(0,0,(int)rect.width,(int)rect.height,texture.GetPixels((int)rect.x,(int)rect.y,(int)rect.width,(int)rect.height));
                sliced.Apply();
                piece.GetComponent<Renderer>().materials[0].SetTexture(BaseMap,sliced);
            }
        }
    }

    private void GoalSprite()
    {
        var texture = selectedImage.currentSelected;
        var sprite = Sprite.Create(texture, new Rect(0,0,texture.width,texture.height), new Vector2(.5f,.5f));
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
