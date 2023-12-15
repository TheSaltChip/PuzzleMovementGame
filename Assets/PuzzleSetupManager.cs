using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PuzzleSetupManager : MonoBehaviour
{
    [SerializeField] private int height;
    [SerializeField] private int width;
    
    [SerializeField] private GameObject puzzlePiece;
    [SerializeField] private GameObject board;
    [SerializeField] private GameObject placePoint;

    private Vector3 ppSize;
    private GameObject[] points;
    private int available;

    private void Awake()
    {
        ppSize = placePoint.GetComponent<Renderer>().bounds.size;
        PlacePieces();
    }

    public void PlacePieces()
    {
        points ??= new GameObject[height * width];
        var trpp = placePoint.transform;
        var pos = trpp.position;
        var amount = height * width;
        var grid = new Vector3[height,width];
        var posY = -height / 2;
        foreach (var point in points)
        {
            Destroy(point);
        }
        available = 0;
        if (amount % 2 == 0)
        {
            for (var i = 0; i < height/2; i++)
            {
                var posX = -width/2;
                for (var j = 0; j < width/2; j++)
                {
                    grid[i, j] = new Vector3(pos.x + posX*ppSize.x-ppSize.x/2,pos.y + posY*ppSize.y-ppSize.y/2,pos.z);
                    posX++;
                }

                for (var k = width / 2; k < width; k++)
                {
                    grid[i, k] = new Vector3(pos.x + posX*ppSize.x+ppSize.x/2,pos.y + posY*ppSize.y-ppSize.y/2,pos.z);
                    posX++;
                }

                posY++;
            }
            
            for (var i = 0; i < height/2; i++)
            {
                var posX = -width/2;
                for (var j = 0; j < width/2; j++)
                {
                    grid[i, j] = new Vector3(pos.x + posX*ppSize.x-ppSize.x/2,pos.y + posY*ppSize.y+ppSize.y/2,pos.z);
                    posX++;
                }

                for (var k = width / 2; k < width; k++)
                {
                    grid[i, k] = new Vector3(pos.x + posX*ppSize.x+ppSize.x/2,pos.y + posY*ppSize.y+ppSize.y/2,pos.z);
                    posX++;
                }

                posY++;
            }
        }
        else
        {
            for (var i = 0; i < height; i++)
            {
                var posX = -width/2;
                for (var j = 0; j < width; j++)
                {
                    grid[i, j] = new Vector3(pos.x + posX*ppSize.x,pos.y + posY*ppSize.y,pos.z);
                    posX++;
                }

                posY++;
            }
        }

        for (var i = 0; i < height; i++)
        {
            for (var j = 0; j < width; j++)
            {
                var obj = Instantiate(placePoint);
                print(obj.GetComponent<Renderer>().bounds.size);
                points[available] = obj;
                obj.SetActive(true);
                var tr = obj.transform;
                tr.parent = gameObject.transform;
                tr = obj.transform;
                tr.localScale = trpp.localScale;
                var p = tr.position;
                p += (tr.forward * grid[i, j].y);
                tr.position = p+(tr.up * grid[i, j].x);
                available++;
            }
        }
    }
}
