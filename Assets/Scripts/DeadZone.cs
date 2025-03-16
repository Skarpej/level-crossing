using UnityEngine;

public class DeadZone : MonoBehaviour
{
    public Transform playerStartPosition;  // The position where the player should reset to.

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Assuming you tagged your player object with "Player"
        {
            // Reset player position to the starting point
            other.transform.position = playerStartPosition.position;

            // Optionally, reset other aspects like health, score, etc.
            // other.GetComponent<PlayerHealth>().ResetHealth();
        }
    }
}
