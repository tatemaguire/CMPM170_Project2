using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;

    Grid grid;
    Tilemap tilemap;
    
    private Vector2 controlVector;

    public void Start()
    {
        grid = FindAnyObjectByType<Grid>();
        tilemap = FindAnyObjectByType<Tilemap>();
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

        if (targetTile != null)
        {
            tilemap.SetTileFlags(tilePos, TileFlags.None);

            tilemap.SetColor(tilePos, Color.red);
        }
    }

    public void FixedUpdate()
    {
        rb.linearVelocity = controlVector * speed;
    }
}
