using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public SpriteRenderer currentTileInicator;
    public float speed;

    public Color[] colors;
    public Color highlightColor;
    public Color neutralHighlight;
    public PlayerController opponent;

    [Tooltip("On: after placing a tile, the player must visit a TilePickupArea to get another. Off: a new tile is given right away (old behaviour).")]
    public bool usePickupArea = false;

    bool hasTile;
    public bool HasTile => hasTile;

    [DoNotSerializeAttribute] public Grid grid;
    [DoNotSerializeAttribute] public Tilemap tilemap;
    
    private Vector2 controlVector;

    Color currentColor;

    public void Awake()
    {
        grid = FindAnyObjectByType<Grid>();
        tilemap = FindAnyObjectByType<Tilemap>();
    }

    public void Start()
    {
        
        GiveTile();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        controlVector = context.ReadValue<Vector2>();
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        //stops button from firing multiple times on one press
        if (!context.performed) return;

        //can't place anything while empty-handed
        if (!hasTile) return;


        Vector3Int tilePos = grid.WorldToCell(transform.position);

        TileBase targetTile = tilemap.GetTile(tilePos);

        //only highlighted tiles (this player's or shared) can be placed on
        //this code counts on the color set on the tilemap being exactly white
        if (targetTile != null && (tilemap.GetColor(tilePos) == highlightColor 
            || tilemap.GetColor(tilePos) == neutralHighlight))
        {
            
            highlightAjacentTiles(tilePos);
            
            //allow tile color to be edited, then edit tile color
            tilemap.SetTileFlags(tilePos, TileFlags.None);
            tilemap.SetColor(tilePos, currentColor);

            //either empty the player's hand or give them a new tile right away
            if (usePickupArea) ClearTile();
            else GiveTile();

            //give player new random color
            //pickNewColor();
        }
    }

    //called by TilePickupArea (and at the start of the game)
    public void GiveTile()
    {
        pickNewColor();
        hasTile = true;
        currentTileInicator.enabled = true;
    }

    public void ClearTile()
    {
        hasTile = false;
        currentTileInicator.enabled = false;
    }

    void pickNewColor()
    {
        int index = Random.Range(0, colors.Length);

        currentColor = colors[index];

        currentTileInicator.color = currentColor;
    }

    void highlightAjacentTiles(Vector3Int tilePos)
    {
        Vector3Int[] directions = 
            { 
                new Vector3Int(1, 0, 0),
                new Vector3Int(1, 1, 0),
                new Vector3Int(0, 1, 0), 
                new Vector3Int(-1, 1, 0), 
                new Vector3Int(-1, 0, 0), 
                new Vector3Int(-1, -1, 0), 
                new Vector3Int(0, -1, 0),
                new Vector3Int(1, -1, 0),
            };

        for (int i = 0; i < directions.Length; i++)
        {
            Vector3Int adjacentTilePos = tilePos + directions[i];

            //print(adjacentTilePos);

            if(tilemap.GetColor(adjacentTilePos) == Color.white)
            {
                tilemap.SetTileFlags(adjacentTilePos, TileFlags.None);
                tilemap.SetColor(adjacentTilePos, highlightColor);
            }
            else if (tilemap.GetColor(adjacentTilePos) == opponent.highlightColor)
            {
                tilemap.SetTileFlags(adjacentTilePos, TileFlags.None);
                tilemap.SetColor(adjacentTilePos, neutralHighlight);
            }
        }
    }

    public void FixedUpdate()
    {
        rb.linearVelocity = controlVector * speed;
    }
}