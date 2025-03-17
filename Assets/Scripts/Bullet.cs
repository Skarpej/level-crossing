using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    private int direction = 1;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = new Vector2(speed * direction, 0);
    }

    public void SetDirection(int newDirection)
    {
        direction = newDirection;
        rb.linearVelocity = new Vector2(speed * direction, 0);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground")) // Bullet disappears on wall hit
        {
            Destroy(gameObject);
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject); // Destroy if it goes off-screen
    }
}
