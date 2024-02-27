using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float moveSpeed = 3.0f;
    public float changeDirectionInterval = 2.0f; // Time interval to change direction.
    public GameObject deathEffect; // Particle effect to play when the enemy is destroyed.
    private Vector3 randomDirection;
    private float nextDirectionChangeTime;
    private bool isWalking; // Added field to track walking status
    private PlayerController playerController; // Reference to the PlayerController script.

    private void Start()
    {
        // Initialize the first random direction and set the initial time for direction change.
        GetRandomDirection();
        nextDirectionChangeTime = Time.time + changeDirectionInterval;

        // Find the PlayerController component on the player (car) GameObject.
        playerController = GameObject.FindGameObjectWithTag("PlayerCar").GetComponent<PlayerController>();
    }

    private void Update()
    {
        // Check if it's time to change direction.
        if (Time.time >= nextDirectionChangeTime)
        {
            GetRandomDirection();
            nextDirectionChangeTime = Time.time + changeDirectionInterval;
        }

        // Move in the random direction.
        transform.Translate(randomDirection * moveSpeed * Time.deltaTime, Space.World);

        // Determine the walking status
        isWalking = randomDirection.magnitude > 0.1f;
    }

    private void GetRandomDirection()
    {
        // Generate a random direction vector.
        randomDirection = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlayerCar"))
        {
            // Check if the collision is with the player (car).
            DestroyEnemy();
        }
    }

    private void DestroyEnemy()
    {
        // Play the death effect if provided.
        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }

        // Destroy the enemy.
        Destroy(gameObject);

        // Increment the player's score.
        playerController.IncreaseScore();
    }

    // Getter for IsWalking status
    public bool IsWalking
    {
        get { return isWalking; }
    }
}
