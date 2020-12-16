using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LocatingTheEnemy : MonoBehaviour
{
    [SerializeField] Tilemap tilemap = null;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TilemapCaveGeneratorQ_6 a = tilemap.GetComponent<TilemapCaveGeneratorQ_6>();
        if (a != null && a.flag)
        {
            locatTheEnemy();
        }
    }

    void locatTheEnemy()
    {
        TilemapCaveGeneratorQ_6 a = tilemap.GetComponent<TilemapCaveGeneratorQ_6>();
        int[,] data = a.caveGenerator.GetMap();

        int x = 0, y = 0;
        while (data[x, y] == 1)
        {
            x = Random.Range(0, a.gridSize);
            y = Random.Range(0, a.gridSize);
        }
        transform.position = tilemap.CellToWorld(new Vector3Int(x, y, 0)); 
    }
}
