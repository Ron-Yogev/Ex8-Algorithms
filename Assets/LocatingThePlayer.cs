﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/**
 * This class represent the player locator before each level
 */
public class LocatingThePlayer : MonoBehaviour
{
    [SerializeField] Tilemap tilemap = null;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    public void updatePlayer()
    {
        // locating the player in the corner of the cave
        TilemapCaveGeneratorQ_6 a = tilemap.GetComponent<TilemapCaveGeneratorQ_6>();
        int[,] data = a.caveGenerator.GetMap();
        int min = 100000000;
        Vector3Int pos = new Vector3Int();
        for (int x = 0; x < a.gridSize; x++)
        {
            for (int y = a.gridSize - 1; y >= 0; y--)
            {
                int b = a.gridSize - y;
                if (data[x, y] != 1)
                {
                    int temp = x + b;
                    if (temp < min)
                    {
                        min = temp;
                        pos = new Vector3Int(x, y, 0);
                    }
                }
            }
        }
        
        transform.position = tilemap.CellToWorld(pos);
        a.playerFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        TilemapCaveGeneratorQ_6 a = tilemap.GetComponent<TilemapCaveGeneratorQ_6>();
        //if its done generating the cave loacte the player
        if (a!=null && a.playerFlag)
        {
            updatePlayer();
        }
    }
}
