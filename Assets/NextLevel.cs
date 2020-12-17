using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/**
 * This class represnet the next level changer
 */
public class NextLevel : MonoBehaviour
{
    [SerializeField] Tilemap tilemap = null;
    [SerializeField] TileBase tileBaseEnd = null;
    TilemapCaveGeneratorQ_6 a = null;
    float size = 0f;

    // Start is called before the first frame update
    void Start()
    {
        //assign to current tilemap size and the cave generator
        a = tilemap.GetComponent<TilemapCaveGeneratorQ_6>();
        size = a.gridSize;
    }

    // Update is called once per frame
    void Update()
    {
        // when the player hit the end point tile we changing to the next level by setting higher gridsize
        if (tilemap.GetTile(tilemap.WorldToCell(transform.position)).name == tileBaseEnd.name)
        {
            a.gridSize =(int)(a.gridSize * 1.1);
            a.Start();
        }
    }
}
