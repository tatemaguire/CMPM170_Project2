using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    
    private Vector2 controlVector;

    public void OnMove(InputAction.CallbackContext context)
    {
        controlVector = context.ReadValue<Vector2>();
    }

    public void FixedUpdate()
    {
        rb.linearVelocity = controlVector * speed;
    }
}
