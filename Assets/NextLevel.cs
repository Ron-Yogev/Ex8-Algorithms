using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class NextLevel : MonoBehaviour
{
    [SerializeField] Tilemap tilemap = null;
    [SerializeField] TileBase tileBaseEnd = null;
    TilemapCaveGeneratorQ_6 a = null;
    float size = 0f;

    // Start is called before the first frame update
    void Start()
    {
        a = tilemap.GetComponent<TilemapCaveGeneratorQ_6>();
        size = a.gridSize;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(tileBaseEnd.name);
        Debug.Log(tilemap.GetTile(tilemap.WorldToCell(transform.position)).name);

        if (tilemap.GetTile(tilemap.WorldToCell(transform.position)).name == tileBaseEnd.name)
        {
            a.gridSize =(int)(a.gridSize * 1.1);
            a.Start();
        }
    }
}
