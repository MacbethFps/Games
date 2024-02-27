using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector3 targetPosition;
    private float moveSpeed = 15f; // Set an appropriate speed for the asteroids

    public void SetTargetPosition(Vector3 target)
    {
        targetPosition = target;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = (targetPosition - transform.position).normalized * moveSpeed;
    }

    // Method to set the speed for the asteroid
    public void SetSpeed(float speed)
    {
        moveSpeed = speed;
    }

    void Update()
    {
        // Check if the asteroid has reached the target position
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            // Uncomment the line below if you want to destroy the asteroid when it reaches the target
            // Destroy(gameObject);
        }
    }
}
