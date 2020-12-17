using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.Tilemaps;

/**
 * This component allows the player to move by clicking the arrow keys,
 * but only if the new position is on an allowed tile.
 */
public class KeyboardMoverByTile: KeyboardMover {
    [SerializeField] Tilemap tilemap = null;
    [SerializeField] AllowedTiles allowedTiles = null;
    [SerializeField] float timeToDestoy = 0.5f;

    private TileBase TileOnPosition(Vector3 worldPosition) {
        Vector3Int cellPosition = tilemap.WorldToCell(worldPosition);
        return tilemap.GetTile(cellPosition);
    }

    void Update()  {
        Vector3 newPosition = NewPosition();
        TileBase tileOnNewPosition = TileOnPosition(newPosition);
        if (allowedTiles.Contain(tileOnNewPosition)) {
            transform.position = newPosition;
        } else {//if the player hit a mountain, and also press both 'x' and arrow its break the tile into a grass and put the player on his position
            if (twoButt&&tileOnNewPosition.name.Equals("mountains"))
            {
                
                Vector3Int a = tilemap.WorldToCell(newPosition);
                tilemap.SetTile(a, allowedTiles.Get()[0]);
                twoButt = false;
                Thread.Sleep((int)(timeToDestoy*1000));
                transform.position = newPosition;

            }
            Debug.Log("You cannot walk on " + tileOnNewPosition + "!");
        }
    }
}
