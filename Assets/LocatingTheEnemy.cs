using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LocatingTheEnemy : MonoBehaviour
{
    [SerializeField] Tilemap tilemap = null;
    [SerializeField] int enemyNumber = 1;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool e=false;
        TilemapCaveGeneratorQ_6 a = tilemap.GetComponent<TilemapCaveGeneratorQ_6>();
        if (a != null)
        {
            if (enemyNumber == 1) e = a.E1flag;
            else e = a.E2flag;
        }
        if (a != null && e)
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

        if (enemyNumber == 1) a.E1flag = false;
        else a.E2flag=false;

    }
}
