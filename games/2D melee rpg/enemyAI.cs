using UnityEngine;

public class enemyAI : MonoBehaviour
{
    public float attackRange = 2f;
    public int attackDamage = 10;
    public float movementSpeed = 2f;
    public int maxHealth = 100;
    public Animator animator;

    public Transform player;
    public Player_Movement playerScript;
    private int currentHealth;
    private bool isAttacking = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerScript = player.GetComponent<Player_Movement>();
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (!isAttacking)
        {
            // Move towards the player
            Vector3 moveDirection = (player.position - transform.position).normalized;
            transform.position += moveDirection * movementSpeed * Time.deltaTime;

            // Perform raycast to detect the player
            RaycastHit2D hit = Physics2D.Raycast(transform.position, moveDirection, attackRange);

            // If the raycast hits the player, and the player is not already being attacked
            if (hit.collider != null && hit.collider.CompareTag("Player"))
            {
                Attack();
            }
        }
    }

    void Attack()
    {
        // Play attack animation
        animator.SetTrigger("attack1");
        // Deal damage to player
        playerScript.TakeDamage(attackDamage);
        // Set cooldown to avoid rapid attacks
        isAttacking = true;
        Invoke("ResetAttack", 1f);
    }

    void ResetAttack()
    {
        isAttacking = false;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            // Play hit animation
            animator.SetTrigger("hit1");
        }
    }

    void Die()
    {
        // Play death animation
        animator.SetTrigger("die1");
        // Disable enemy
        GetComponent<Collider2D>().enabled = false;
        enabled = false;
        // Add score, play sound, etc.
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, (player.position - transform.position).normalized * attackRange);
    }
}
