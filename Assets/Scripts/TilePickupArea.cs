using UnityEngine;

// A zone players walk into to pick up a new tile.
// After a player places a tile they are empty-handed until they visit a pickup area.
//
// UNITY SETUP (do this in the editor):
//   1. Create an empty GameObject (e.g. "Pickup Area P1") and position it off the grid.
//   2. Add this component. A BoxCollider2D is added automatically and set to "Is Trigger".
//      Resize the collider to the size of the area you want.
//   3. (Optional) Add a SpriteRenderer so players can see the area, and drag it into "Area Sprite".
//   4. (Optional) Drag a player into "Only For Player" to make the area belong to one player.
//      Leave it empty to let both players use it.
//   5. On each Player's PlayerController, tick "Use Pickup Area" (it is off by default so the
//      game keeps working until the areas exist).
[RequireComponent(typeof(Collider2D))]
public class TilePickupArea : MonoBehaviour
{
    [Tooltip("Leave empty so any player can use this area, or set one player to make it theirs.")]
    public PlayerController onlyForPlayer;

    [Tooltip("Optional: sprite that shows where the area is. Dims when the player inside already has a tile.")]
    public SpriteRenderer areaSprite;

    public Color readyColor = new Color(1f, 1f, 1f, 0.5f);
    public Color busyColor = new Color(1f, 1f, 1f, 0.2f);

    // Runs when the component is first added in the editor: makes the collider a trigger.
    void Reset()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }

    void Awake()
    {
        // Safety net in case someone unticks "Is Trigger" by accident.
        GetComponent<Collider2D>().isTrigger = true;
        SetSpriteColor(readyColor);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        TryGiveTile(other);
    }

    // Also check while standing inside, so a player who is already in the area
    // gets a tile as soon as they need one.
    void OnTriggerStay2D(Collider2D other)
    {
        TryGiveTile(other);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponentInParent<PlayerController>() != null)
        {
            SetSpriteColor(readyColor);
        }
    }

    void TryGiveTile(Collider2D other)
    {
        PlayerController player = other.GetComponentInParent<PlayerController>();
        if (player == null) return;
        if (onlyForPlayer != null && player != onlyForPlayer) return;

        if (!player.HasTile)
        {
            player.GiveTile();
        }

        SetSpriteColor(busyColor);
    }

    void SetSpriteColor(Color color)
    {
        if (areaSprite != null)
        {
            areaSprite.color = color;
        }
    }
}
