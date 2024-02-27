using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilities : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public int numberOfBullets = 3;
    public float spreadAngle = 15f;
    public float abilityCooldown = 2f; // Cooldown time in seconds

    private float cooldownTimer = 0f;

    private void Update()
    {
        // Check if the cooldown timer has expired
        if (cooldownTimer <= 0f)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Shoot();
                // Set the cooldown timer to the cooldown value
                cooldownTimer = abilityCooldown;
            }
        }
        else
        {
            // Decrement the cooldown timer
            cooldownTimer -= Time.deltaTime;
        }
    }

    private void Shoot()
    {
        // Calculate the angle step between bullets
        float angleStep = spreadAngle / (numberOfBullets - 1);

        // Calculate the direction of the normal bullet
        Vector3 normalBulletDirection = bulletSpawnPoint.up;

        // Calculate the initial angle for the first bullet
        float startAngle = -spreadAngle / 2f;

        // Loop to instantiate bullets in a spread pattern
        for (int i = 0; i < numberOfBullets; i++)
        {
            // Calculate the rotation for the bullet
            Quaternion rotation = Quaternion.Euler(0f, 0f, startAngle + i * angleStep);

            // Calculate the direction for the spread bullet
            Vector3 spreadBulletDirection = rotation * normalBulletDirection;

            // Instantiate a bullet and set its direction
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, rotation);
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            if (bulletScript)
            {
                bulletScript.SetDirection(spreadBulletDirection);
            }
        }
    }
}
