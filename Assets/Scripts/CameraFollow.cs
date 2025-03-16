using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTransform; // Reference to the player's transform
    public Vector3 offset; // The offset of the camera relative to the player
    public float smoothSpeed = 0.075f; // The base smooth speed for the camera
    public float maxSpeedMultiplier = 2f; // Maximum multiplier for speed when far from the camera

    private Camera mainCamera; // Reference to the Camera component
    private void Start()
    {
        // Get the main camera if not already assigned
        mainCamera = Camera.main;
    }

    private void LateUpdate()
    {
        // Calculate the desired position of the camera
        Vector3 targetPosition = playerTransform.position + offset;
        
        // Get the current position of the camera
        Vector3 currentPosition = transform.position;

        // Calculate the distance between the camera and the player
        float distance = Vector3.Distance(currentPosition, targetPosition);

        // Adjust the smooth speed based on how far the player is
        float speedMultiplier = Mathf.Lerp(1f, maxSpeedMultiplier, distance / 10f); // 10f is just an arbitrary threshold, tweak as needed
        float adjustedSmoothSpeed = smoothSpeed * speedMultiplier;

        // Use Lerp to smoothly follow the player
        Vector3 smoothedPosition = Vector3.Lerp(currentPosition, targetPosition, adjustedSmoothSpeed * Time.deltaTime);
        
        // Apply the smoothed position to the camera
        transform.position = smoothedPosition;
    }
}
