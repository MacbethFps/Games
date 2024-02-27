using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float lifeTime = 2f;
    public bool isPlayerBullet = true; // Set this to true for player bullets
    public int damage = 10; // Damage value for the bullet

    private float lifeTimer;
    private Vector3 bulletDirection;

    void Start()
    {
        lifeTimer = lifeTime;
    }

    void Update()
    {
        // Move the bullet
        transform.Translate(bulletDirection * speed * Time.deltaTime);

        // Destroy the bullet after the specified lifetime
        lifeTimer -= Time.deltaTime;
        if (lifeTimer <= 0f)
        {
            Destroy(gameObject);
        }
    }

    public void SetDirection(Vector3 direction)
    {
        bulletDirection = direction.normalized;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the bullet has collided with an enemy and the bullet is from the player
        if (isPlayerBullet && (other.CompareTag("enemy") || other.CompareTag("toughenemy")))
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage); // Inflict damage on the enemy
            }
            Destroy(gameObject); // Destroy the player's bullet after hitting an enemy
        }
    }
}
