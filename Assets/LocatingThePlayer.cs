using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LocatingThePlayer : MonoBehaviour
{
    [SerializeField] Tilemap tilemap = null;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    public void updatePlayer()
    {
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
        StartCoroutine(waitTwoSec());
        a.playerFlag = false;
    }

    private IEnumerator waitTwoSec()
    {
        yield return new WaitForSeconds(2);
    }

    // Update is called once per frame
    void Update()
    {
        TilemapCaveGeneratorQ_6 a = tilemap.GetComponent<TilemapCaveGeneratorQ_6>();
        if (a!=null && a.playerFlag)
        {
            updatePlayer();
        }
    }
}
