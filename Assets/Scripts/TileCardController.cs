using UnityEngine;
using UnityEngine.InputSystem;

public class TileCardController : MonoBehaviour
{
    bool p1Inside, p2Inside;
    SpriteRenderer sr;

    void Awake() { sr = GetComponent<SpriteRenderer>(); }

    void OnTriggerEnter2D(Collider2D c) { Track(c, true); }
    void OnTriggerExit2D(Collider2D c) { Track(c, false); }

    void Track(Collider2D c, bool inside)
    {
        if (c.gameObject.name == "Player 1") p1Inside = inside;
        else if (c.gameObject.name == "Player 2") p2Inside = inside;
    }

    void Update()
    {
         var kb = Keyboard.current;
        if (kb == null) return;
        if (p1Inside && kb.spaceKey.wasPressedThisFrame) sr.color = Color.red;
        if (p2Inside && (kb.enterKey.wasPressedThisFrame || kb.numpadEnterKey.wasPressedThisFrame))
            sr.color = new Color(0.05f, 0.40f, 1f);
    }
}

