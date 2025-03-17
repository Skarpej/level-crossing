using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 0.2f; // Time between shots
    private float nextFireTime = 0f; // When the next shot is allowed

    void Update()
    {
        // Only shoot if enough time has passed
        if (Input.GetButtonDown("Shoot") && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate; // Set next allowed fire time
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        // Get the player's direction to determine bullet's direction
        int direction = GetComponent<PlayerMovement>().facingRight ? 1 : -1;

        bullet.GetComponent<Bullet>().SetDirection(direction); // Set bullet's direction
    }
}
