using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    public Transform cameraTransform;  // Reference to the camera
    public float parallaxFactor;       // The speed at which the background layer moves

    private Vector3 previousCameraPosition;

    void Start()
    {
        // Initial camera position
        previousCameraPosition = cameraTransform.position;
    }

    void Update()
    {
        // Calculate how far the camera has moved
        float deltaMovement = cameraTransform.position.x - previousCameraPosition.x;

        // Move the background layer based on the camera's movement and the parallax factor
        transform.position += Vector3.right * deltaMovement * parallaxFactor;

        // Update the previous camera position
        previousCameraPosition = cameraTransform.position;
    }
}
