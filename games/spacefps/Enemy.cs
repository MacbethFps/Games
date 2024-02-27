using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public bool IsWalking;

    private Animator animator;
    private EnemyMovement enemyMovement;

    private void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        enemyMovement = GetComponent<EnemyMovement>(); // Add this line to get the reference to the EnemyMovement component
    }

    public void TakeDamage(int damageAmount)
    {
        if (currentHealth <= 0)
            return;

        currentHealth -= damageAmount;

        // Trigger the "GetHit" animation
        animator.SetTrigger("Hit");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Set the death animation trigger
        animator.SetTrigger("Die");

        // Additional death behavior, such as disabling components or dropping items
        Destroy(gameObject, 2.0f); // Destroy the enemy GameObject after 2 seconds
    }

    private void Update()
    {
        // Set the "IsWalking" parameter based on the enemy's movement status
        bool isWalking = enemyMovement.IsWalking;
        animator.SetBool("IsWalking", isWalking);
    }
}
