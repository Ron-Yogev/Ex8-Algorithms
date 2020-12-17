using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/**
 * This class represent the enemies locator before each level
 */
public class LocatingTheEnemy : MonoBehaviour
{
    [SerializeField] Tilemap tilemap = null;
    [SerializeField] int enemyNumber = 1;//which enemy?


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool e=false;
        TilemapCaveGeneratorQ_6 a = tilemap.GetComponent<TilemapCaveGeneratorQ_6>();
        if (a != null)//checking which enemy is
        {
            if (enemyNumber == 1) e = a.E1flag;
            else e = a.E2flag;
        }
        // if the map done generating the cave locate the enemy
        if (a != null && e)
        {
            locatTheEnemy();
        }
    }

    void locatTheEnemy()
    {
        //locate the enemy in a random position in the cave
        TilemapCaveGeneratorQ_6 a = tilemap.GetComponent<TilemapCaveGeneratorQ_6>();
        int[,] data = a.caveGenerator.GetMap();

        int x = 0, y = 0;
        while (data[x, y] == 1)
        {
            x = Random.Range(0, a.gridSize);
            y = Random.Range(0, a.gridSize);
        }
        transform.position = tilemap.CellToWorld(new Vector3Int(x, y, 0));

        //if its done locating the enemy false the flag for the next level
        if (enemyNumber == 1) a.E1flag = false;
        else a.E2flag=false;

    }
}
