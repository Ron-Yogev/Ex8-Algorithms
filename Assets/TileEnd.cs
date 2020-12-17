using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/**
 * This class represent the end tile locator
 */
public class TileEnd : MonoBehaviour
{

    [SerializeField] TileBase tilebase = null;

    [SerializeField] Tilemap endtilemap = null;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void updatePlayer()
    {
        //locating the end point in the corner
        TilemapCaveGeneratorQ_6 a = endtilemap.GetComponent<TilemapCaveGeneratorQ_6>();
        int[,] data = a.caveGenerator.GetMap();
        int max = 0;
        Vector3Int pos = new Vector3Int();
        for (int x = 0; x < a.gridSize; x++)
        {
            for (int y = a.gridSize - 1; y >= 0; y--)
            {
                int b = a.gridSize - y;
                if (data[x, y] != 1)
                {
                    int temp = x + b;
                    if (temp > max)
                    {
                        max = temp;
                        pos = new Vector3Int(x, y, 0);
                    }
                }
            }
        }
        endtilemap.SetTile(pos, tilebase);
        a.EndFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        //checking if the map done generating the cave
        TilemapCaveGeneratorQ_6 a = endtilemap.GetComponent<TilemapCaveGeneratorQ_6>();
        if (a != null && a.EndFlag)
        {
            updatePlayer();
        }
    }
}
