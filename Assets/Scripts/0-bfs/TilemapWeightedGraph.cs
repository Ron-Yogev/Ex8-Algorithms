using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/**
 * A graph that represents a tilemap, using only the allowed tiles.
 */
public class TilemapWeightedGraph : WeightedGraph<Vector3Int>
{
    private Tilemap tilemap;
    private TileBase[] allowedTiles;
    private Dictionary<TileBase, int> tilesWeight;

    public TilemapWeightedGraph(Tilemap tilemap, TileBase[] allowedTiles, Dictionary<TileBase, int> tilesWeight)
    {

        this.tilemap = tilemap;
        this.allowedTiles = allowedTiles;
        this.tilesWeight = tilesWeight;
    }

    static Vector3Int[] directions = {
            new Vector3Int(-1, 0, 0),
            new Vector3Int(1, 0, 0),
            new Vector3Int(0, -1, 0),
            new Vector3Int(0, 1, 0),
    };

    public IEnumerable<Vector3Int> Neighbors(Vector3Int node)
    {
        foreach (var direction in directions)
        {
            Vector3Int neighborPos = node + direction;
            TileBase neighborTile = tilemap.GetTile(neighborPos);
            if (allowedTiles.Contains(neighborTile))
                yield return neighborPos;
        }
    }

    public int GetWeight(Vector3Int node1)
    {
        TileBase pos = this.tilemap.GetTile(node1);
        return tilesWeight[pos];
    }
}
