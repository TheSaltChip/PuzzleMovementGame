using System;
using System.Collections;
using System.Collections.Generic;
using Events;
using Puzzle.Scriptables;
using Unity.VisualScripting;
using Unity.XR.PXR;
using UnityEngine;
using UnityEngine.Events;

public class PuzzleSetupManager : MonoBehaviour
{
    [SerializeField] private int height;
    [SerializeField] private int width;
    
    [SerializeField] private GameObject puzzlePiece;
    [SerializeField] private GameObject board;
    [SerializeField] private GameObject placePoint;
    [SerializeField] private SelectedImage selectedImage;
    [SerializeField] private GoalSprite goalSprite;

    [SerializeField] private UnityEvent changedImage;

    private Vector3 ppSize;
    private GameObject[] points;
    private int available;
    private Vector3 bSize;
    private static readonly int BaseMap = Shader.PropertyToID("_BaseMap");

    private void Awake()
    {
        ppSize = placePoint.GetComponent<Renderer>().bounds.size;
        bSize = board.GetComponent<Renderer>().bounds.size;
        bSize.y = bSize.x;
        ppSize.y = ppSize.x;
        ppSize *= 7.46f;
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
        points ??= new GameObject[height * width];
        var trpp = placePoint.transform;
        var pos = trpp.localPosition;
        var amount = height * width;
        var grid = new Vector3[height,width];
        var posY = -height/2;
        var posX = -width/2;
        foreach (var point in points)
        {
            Destroy(point);
        }

        var originVector = new Vector3();
        available = 0;
        
        if (height % 2 == 0)
        {
            originVector.y = -ppSize.y * (height / 2f)+ppSize.y / 2f;
        }
        else
        {
            originVector.y = - ppSize.y * (height / 2);
        }

        if (width % 2 == 0)
        {
            originVector.x = -ppSize.x * (width / 2f)+ppSize.x / 2f;
        }
        else
        {
            originVector.x = -ppSize.x * (width / 2);
        }
        
        
        
        for (var i = 0; i < height; i++)
        {
            for (var j = 0; j < width; j++)
            {
                grid[i, j] = new Vector3(originVector.x + j*ppSize.x,originVector.y + i*ppSize.y,pos.z);
            }
        }

        for (var i = 0; i < height; i++)
        {
            for (var j = 0; j < width; j++)
            {
                //right = up, up = right x swapped with y
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
        
        var scale = board.transform.localScale;
        scale.y += (height - 1) * scale.y;
        scale.x += (width - 1) * scale.x;
        
        board.transform.localScale = scale;
    }

    private void SetUpPieces()
    {
        var texture = selectedImage.currentSelected;
        for (var i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                var rect = new Rect(j*(texture.width/(width * 1f)),i*(texture.height/(height * 1f)),texture.width/(width * 1f),texture.height/(height * 1f));
                //var sprite = Sprite.Create(texture, rect, new Vector2(.5f, .5f));
                var piece = Instantiate(puzzlePiece);
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
        width = Int32.Parse(s[0]);
        height = Int32.Parse(s[1]);
        SetUp();
    }
}
