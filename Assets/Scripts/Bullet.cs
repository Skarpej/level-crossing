using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    private Rigidbody2D rb;
    private int direction = 1; // 1 means right, -1 means left

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0; // Disable gravity
        rb.linearVelocity = new Vector2(speed * direction, 0); // Move bullet straight

    }

    // Set the bullet's direction (right or left)
    public void SetDirection(int newDirection)
    {
        direction = newDirection;
        rb.linearVelocity = new Vector2(speed * direction, 0); // Update velocity based on direction
    }

    // Destroy the bullet when it collides with the ground
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) // If it hits an object with the Ground tag
        {
            Destroy(gameObject); // Destroy the bullet
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject); // Destroy if it goes off-screen
    }
}
