using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed = 5f;
    public int maxHealth = 100;
    private int currentHealth;
    Vector2 movement;
    public Transform circleOrigin;
    public float radius;

    public Animator animator;

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        // Movement input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Set the "speed" parameter in the animator
        animator.SetFloat("speed", movement.sqrMagnitude);

        // If speed is greater than 0.001, set the "isRunning" parameter to true
        animator.SetBool("isRunning", movement.sqrMagnitude > 0.001f);

        // Attack input
        if (Input.GetMouseButtonDown(0)) // Assuming left mouse click for attack
        {
            Attack();
        }
    }

    public void Attack()
    {
        animator.SetTrigger("Attack");
        DetectColliders();
    }

    void FixedUpdate()
    {
        // Movement
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        if (movement.x > 0)
        {
            // Moving right
            transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
        else if (movement.x < 0)
        {
            // Moving left
            transform.localScale = new Vector3(-0.5f, 0.5f, 0.5f);
        }
    }

    // Corrected usage of OnDrawGizmosSelected
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Vector3 position = circleOrigin == null ? Vector3.zero : circleOrigin.position;
        Gizmos.DrawWireSphere(position, radius);
    }

    public void DetectColliders()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(circleOrigin.position, radius);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                // Damage the enemy
                enemyAI enemy = collider.GetComponent<enemyAI>();
                if (enemy != null)
                {
                    enemy.TakeDamage(1);
                }
            }
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Play death animation or do any other actions you want to perform when the player dies
        Debug.Log("Player died!");
        // For example, you can reload the level or show a game over screen
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // If collided with an enemy, take damage
            TakeDamage(10); // Adjust damage amount as needed
        }
    }
}
