using UnityEngine;
using UnityEngine.Tilemaps;

/**
 * This component allows the player to move by clicking the arrow keys,
 * but only if the new position is on an allowed tile.
 */
public class KeyboardMoverByTile: KeyboardMover {
    [SerializeField] Tilemap tilemap = null;
    [SerializeField] AllowedTiles allowedTiles = null;
    ////////
        [SerializeField] TileBase deleteTile = null; // the type we wont to carve
        [SerializeField] TileBase freeTile = null;   //the type we wont to create instead 
    ///////
    private TileBase TileOnPosition(Vector3 worldPosition) {
        Vector3Int cellPosition = tilemap.WorldToCell(worldPosition);
        return tilemap.GetTile(cellPosition);
    }

    void Update()  {
        Vector3 newPosition = NewPosition();
        TileBase tileOnNewPosition = TileOnPosition(newPosition);
        if (allowedTiles.Contain(tileOnNewPosition)) {
            transform.position = newPosition;
        }
        ///we add the option to Carve in the mountain and turn it into a regular walking surface
         else if(!allowedTiles.Contain(tileOnNewPosition) && tileOnNewPosition.Equals(deleteTile) && Input.GetKey(KeyCode.X))
        {
            tilemap.SetTile(tilemap.WorldToCell(newPosition), freeTile);
        ///
        }  else {
            Debug.Log("You cannot walk on " + tileOnNewPosition + "!");
        }
    }
}
