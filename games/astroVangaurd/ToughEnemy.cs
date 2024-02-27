using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToughEnemy : MonoBehaviour
{
    public int maxHealth = 3; // Number of hits required to destroy the tough enemy
    private int currentHealth;

    public float moveSpeed = 3f; // Set the desired slower move speed

    private Transform player;

    void Start()
    {
        currentHealth = maxHealth;

        // Find the player's transform
        player = GameObject.FindGameObjectWithTag("player").transform;
    }

    void Update()
    {
        MoveTowardsPlayer();
    }

    void MoveTowardsPlayer()
    {
        if (player == null)
        {
            return;
        }

        // Calculate the direction to the player
        Vector3 directionToPlayer = player.position - transform.position;

        // Normalize the direction and apply movement speed
        Vector3 movement = directionToPlayer.normalized * moveSpeed * Time.deltaTime;
        transform.position += movement;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();

            // Check if the bullet is from the player
            if (bullet != null && bullet.isPlayerBullet)
            {
                TakeDamage(bullet.damage); // Apply damage to the tough enemy based on the bullet's damage
                Destroy(collision.gameObject); // Destroy the player's bullet after hitting the tough enemy
            }
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Destroy(gameObject); // Destroy the tough enemy when its health is zero
        }
    }
}
