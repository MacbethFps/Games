using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletShooter : MonoBehaviour
{
    public GameObject enemyBulletPrefab;
    public float shootInterval = 2f;
    public float bulletSpeed = 10f;
    public float bulletSpreadAngle = 15f; // The angle by which bullet direction will vary

    private Transform playerTransform;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("player").transform;
        InvokeRepeating("ShootBullet", shootInterval, shootInterval);
    }

    void Update()
    {
        // Make the enemy face the player
        if (playerTransform != null)
        {
            Vector3 directionToPlayer = playerTransform.position - transform.position;
            float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle - 90f));
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
        }
    }

    void ShootBullet()
    {
        if (playerTransform != null)
        {
            Vector3 directionToPlayer = playerTransform.position - transform.position;
            directionToPlayer.Normalize();

            // Introduce randomness in the bullet direction by adding a random angle within the spread angle
            float randomSpreadAngle = Random.Range(-bulletSpreadAngle, bulletSpreadAngle);
            Quaternion randomSpreadRotation = Quaternion.Euler(0f, 0f, randomSpreadAngle);
            Vector3 bulletDirection = randomSpreadRotation * directionToPlayer;

            // Instantiate the bullet with the calculated direction and rotation as the enemy
            GameObject enemyBullet = Instantiate(enemyBulletPrefab, transform.position, Quaternion.identity);
            Rigidbody2D bulletRigidbody = enemyBullet.GetComponent<Rigidbody2D>();
            bulletRigidbody.velocity = bulletDirection.normalized * bulletSpeed;

            // Ensure the bullets face the correct direction without any rotation
            enemyBullet.transform.up = bulletDirection.normalized;
        }
    }
}