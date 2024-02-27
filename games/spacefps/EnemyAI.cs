using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround,whatIsPlayer;

    //patrolling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;
    
    //attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    
    //states 
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        player = GameObject.Find("FirstPersonController").transform;
        agent = GetComponent<NavMeshAgent>();

    }
    private void update ()
    {
        playerInSightRange = Physics.CheckSphere(transform.position,sightRange,whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position,attackRange,whatIsPlayer);
        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInSightRange && playerInAttackRange) AttackPlayer();
    }

    private void Patroling()
    {
        if(!walkPointSet) SearchWalkPoint();
        if (walkPointSet) agent.SetDestination(walkPoint);
        Vector3 distanceToWalkPoint = transform.position - walkPoint;
    }


        private void SearchWalkPoint(){
        float randomz = Random.Range(-walkPointRange,walkPointRange);
        float randomx = Random.Range(-walkPointRange, walkPointRange);
        walkPoint = new Vector3(transform.position.x + randomx,transform.position.y,transform.position.z + randomz);
        if (Physics.Raycast(walkPoint,-transform.up,2f,whatIsGround))
        walkPointSet = true;


    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);

    }

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(player);

        if(!alreadyAttacked){
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack),timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked=false;
    }
}
