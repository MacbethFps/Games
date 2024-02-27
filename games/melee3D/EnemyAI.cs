using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsPlayer;

    [Header("Patrolling")]
    public float walkPointRange;
    private Vector3 walkPoint;
    private bool walkPointSet;

    [Header("Attacking")]
    public float timeBetweenAttacks;
    private bool alreadyAttacked;

    [Header("States")]
    public float sightRange, attackRange;
    private bool playerInSightRange, playerInAttackRange;

    [Header("Health")]
    public int maxHealth = 100;
    private int currentHealth;

    private EnemyAnimationController animationController; // Reference to the animation controller

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animationController = GetComponent<EnemyAnimationController>();

        // Initialize enemy's health
        currentHealth = maxHealth;
    }

    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange)
        {
            Patroling();
        }
        if (playerInSightRange && !playerInAttackRange)
        {
            ChasePlayer();
        }
        if (playerInSightRange && playerInAttackRange)
        {
            AttackPlayer();
        }
    }

    private void Patroling()
    {
        if (!walkPointSet)
        {
            SearchWalkPoint();
        }

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }

        // Tell the animation controller that the enemy is walking
        animationController.SetWalking(true);
    }

    private void SearchWalkPoint()
    {
        float randomz = Random.Range(-walkPointRange, walkPointRange);
        float randomx = Random.Range(-walkPointRange, walkPointRange);
        walkPoint = new Vector3(transform.position.x + randomx, transform.position.y, transform.position.z + randomz);

        if (NavMesh.SamplePosition(walkPoint, out NavMeshHit hit, 1.0f, NavMesh.AllAreas))
        {
            walkPoint = hit.position;
            walkPointSet = true;
        }
    }

    private void ChasePlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // If the player is within the attack range, stop chasing and start attacking
        if (distanceToPlayer <= attackRange)
        {
            AttackPlayer();

            return;
        }

        // If the player is not within attack range, continue chasing
        agent.SetDestination(player.position);

        // Rotate the enemy towards the player's position (optional)
        if (!alreadyAttacked)
        {
            transform.LookAt(player);
        }

        // Tell the animation controller that the enemy is walking
        animationController.SetWalking(true);
    }

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);

        if (!alreadyAttacked)
        {
            // Trigger the "IsAttacking" parameter in the enemy's animator
            animationController.SetAttacking(true);

            // Attack logic here

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;

        // Reset the "IsAttacking" parameter in the enemy's animator
        animationController.SetAttacking(false);
    }

    // Function to handle taking damage
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // Check if health is less than or equal to zero
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Handle enemy death here
    void Die()
    {
        // Implement enemy death logic, e.g., play death animations and remove the enemy from the scene.
        Destroy(gameObject);
    }
}
