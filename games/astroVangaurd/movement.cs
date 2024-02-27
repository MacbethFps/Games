using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 5f;
    public Camera mainCamera;
    public GameObject bulletPrefab;

    void Update()
    {
        // Get the input for movement
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Calculate the movement direction and apply force to the spaceship
        Vector2 moveDirection = new Vector2(moveHorizontal, moveVertical);
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);

        // Look at the mouse pointer
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 directionToMouse = mousePosition - transform.position;
        float angle = Mathf.Atan2(directionToMouse.y, directionToMouse.x) * Mathf.Rad2Deg;
        Quaternion desiredRotation = Quaternion.Euler(0f, 0f, angle - 90f);
        transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, rotationSpeed * Time.deltaTime);

        // Prevent the spaceship from leaving the camera view
        float vertExtent = mainCamera.orthographicSize;
        float horzExtent = vertExtent * Screen.width / Screen.height;

        float minX = mainCamera.transform.position.x - horzExtent;
        float maxX = mainCamera.transform.position.x + horzExtent;
        float minY = mainCamera.transform.position.y - vertExtent;
        float maxY = mainCamera.transform.position.y + vertExtent;

        float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);
        float clampedY = Mathf.Clamp(transform.position.y, minY, maxY);

        transform.position = new Vector3(clampedX, clampedY, transform.position.z);

        if (Input.GetMouseButtonDown(0))
        {
            FireBullet();
        }
    }

    void FireBullet()
    {
        // Spawn a bullet prefab at the spaceship's position and rotation
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);

        // Set the bullet's initial velocity to match the spaceship's forward direction
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.velocity = transform.up * bullet.GetComponent<Bullet>().speed;
    }

    // This method is used to handle collisions with enemy bullets
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player collides with an enemy bullet
        if (other.CompareTag("EnemyBullet"))
        {
            Bullet bullet = other.GetComponent<Bullet>();
            if (bullet != null && !bullet.isPlayerBullet)
            {
                PlayerHealth playerHealth = GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(bullet.damage);
                    Destroy(other.gameObject); // Destroy the enemy's bullet after hitting the player
                }
            }
        }
    }
}
