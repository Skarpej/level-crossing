using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    private int direction = 1; // Default to moving right

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime * direction);
    }

    public void SetDirection(int newDirection)
    {
        direction = newDirection;
        transform.localScale = new Vector3(direction, 1, 1); // Flip bullet if needed
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
