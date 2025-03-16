using UnityEngine;

public class DeadZone : MonoBehaviour
{
    public Transform playerStartPosition;  // The respawn point
    public Transform cameraStartPosition; // The initial camera position (optional)

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Make sure player has the "Player" tag
        {
            // Reset player position
            other.transform.position = playerStartPosition.position;

            // Reset camera position (if using a simple camera follow script)
            Camera.main.transform.position = cameraStartPosition.position;
        }
    }
}
