using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public SpriteRenderer currentTileInicator;
    public float speed;

    public Color[] colors;

    Grid grid;
    Tilemap tilemap;
    
    private Vector2 controlVector;

    Color currentColor;

    public void Start()
    {
        grid = FindAnyObjectByType<Grid>();
        tilemap = FindAnyObjectByType<Tilemap>();

        pickNewColor();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        controlVector = context.ReadValue<Vector2>();
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        //stops button from firing multiple times on one press
        if (!context.performed) return;


        Vector3Int tilePos = grid.WorldToCell(transform.position);

        TileBase targetTile = tilemap.GetTile(tilePos);

        //this code counts on the color set on the tilemap being exactly white
        if (targetTile != null && tilemap.GetColor(tilePos) == Color.white)
        {
            //allow tile color to be edited, then edit tile color
            tilemap.SetTileFlags(tilePos, TileFlags.None);
            tilemap.SetColor(tilePos, currentColor);

            //give player new random color
            pickNewColor();
        }
    }

    void pickNewColor()
    {
        int index = Random.Range(0, colors.Length);

        currentColor = colors[index];

        currentTileInicator.color = currentColor;
    }

    public void FixedUpdate()
    {
        rb.linearVelocity = controlVector * speed;
    }
}
