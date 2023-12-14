using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleSetupManager : MonoBehaviour
{
    [SerializeField] private int height;
    [SerializeField] private int width;
    
    [SerializeField] private GameObject puzzlePiece;
    [SerializeField] private GameObject board;
    [SerializeField] private GameObject placePoint;

    private Vector3 ppSize;

    private void Awake()
    {
        ppSize = placePoint.GetComponent<Renderer>().bounds.size;
    }

    private void PlacePieces()
    {
        var trpp = placePoint.transform;
        var rot = trpp.rotation;
        var pos = trpp.position;
        var amount = height * width;
        var grid = new Vector3[height,width];
        var posY = -height / 2;
        var posX = -width / 2;
        if (amount % 2 == 0)
        {
            var posEven = new Vector3(pos.x-ppSize.x,pos.y-ppSize.y,pos.z);
            for (var i = 0; i < height; i++)
            {
                posX = 0;
                for (var j = 0; j < width; j++)
                {
                    grid[i, j] = new Vector3(posEven.x + posX*ppSize.x-ppSize.x/2,posEven.y + posY*ppSize.y-ppSize.y/2,pos.z);
                    posX++;
                }

                posY--;
            }
        }
        else
        {
            
        }
    }
}
