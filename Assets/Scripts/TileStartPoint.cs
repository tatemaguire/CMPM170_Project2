using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileStartPoint : MonoBehaviour
{
    public PlayerController player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Tilemap tilemap = player.tilemap;

        Vector3Int tilePos = player.grid.WorldToCell(transform.position);

        TileBase targetTile = tilemap.GetTile(tilePos);

        //allow tile color to be edited, then edit tile color
        tilemap.SetTileFlags(tilePos, TileFlags.None);
        tilemap.SetColor(tilePos, player.highlightColor);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
