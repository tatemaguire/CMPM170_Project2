using UnityEngine;
using UnityEngine.InputSystem;

public class TileCardController : MonoBehaviour
{
    public enum State
    {
        AVAILABLE, RED, BLUE
    }

    [SerializeField] private int pointValue = 3;

    [HideInInspector] public State currentState = State.AVAILABLE;
    bool p1Inside, p2Inside;
    SpriteRenderer sr;

    [HideInInspector] public static int redPoints = 0;
    [HideInInspector] public static int bluePoints = 0;

    void Awake() 
    {
        sr = GetComponent<SpriteRenderer>(); 
        redPoints = 0;
        bluePoints = 0;
    }

    void OnTriggerEnter2D(Collider2D c) { Track(c, true); }
    void OnTriggerExit2D(Collider2D c) { Track(c, false); }

    void Track(Collider2D c, bool inside)
    {
        if (c.gameObject.name == "Player 1") p1Inside = inside;
        else if (c.gameObject.name == "Player 2") p2Inside = inside;
    }

    void Update()
    {
        Keyboard kb = Keyboard.current;

        if (kb == null) return;
        if (currentState != State.AVAILABLE) return;

        if (p1Inside && kb.spaceKey.wasPressedThisFrame) 
        {
            sr.color = Color.red;
            currentState = State.RED;
            redPoints += pointValue;
        }
        if (p2Inside && (kb.enterKey.wasPressedThisFrame || kb.numpadEnterKey.wasPressedThisFrame))
        {
            sr.color = new Color(0.05f, 0.40f, 1f);
            currentState = State.BLUE;
            bluePoints += pointValue;
        }
    }
}

