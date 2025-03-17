using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTransform; // Reference to the player's transform
    public Vector3 offset = new Vector3(0f, 2f, -10f); // Default offset
    public float smoothTime = 0.15f; // Lower values = faster response, higher = smoother
    public float maxSpeed = 30f; // Limit for max follow speed

    private Vector3 velocity = Vector3.zero;

    private void LateUpdate()
    {
        if (playerTransform == null) return;

        // Desired camera position
        Vector3 targetPosition = playerTransform.position + offset;

        // Smoothly move the camera
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime, maxSpeed);
    }
}
