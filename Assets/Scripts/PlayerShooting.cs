using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 0.2f;
    private float nextFireTime = 0f;

    void Update()
    {
        if (Input.GetButton("Shoot") && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

void Shoot()
{
    GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

    // Check PlayerMovement's facingRight variable
    bool isFacingRight = GetComponent<PlayerMovement>().facingRight;

    // Set bullet direction
    bullet.GetComponent<Bullet>().SetDirection(isFacingRight ? 1 : -1);
}

}
