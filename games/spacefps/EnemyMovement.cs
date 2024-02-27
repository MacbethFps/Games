using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 3.0f;
    public float followDistance = 10.0f; // Maximum distance to follow the player
    private Transform player;
    private bool isWalking; // Added field to track walking status

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (player != null && Vector3.Distance(transform.position, player.position) <= followDistance)
        {
            MoveTowardsPlayer();
        }
    }

    private void MoveTowardsPlayer()
    {
        if (player == null)
        {
            return; // Player not found, exit movement
        }

        Vector3 directionToPlayer = player.position - transform.position;
        directionToPlayer.y = 0; // Prevent vertical movement
        directionToPlayer.Normalize();

        // Rotate to face the player on the x-axis
        Quaternion targetRotation = Quaternion.LookRotation(new Vector3(directionToPlayer.x, 0, directionToPlayer.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5.0f);

        // Move towards the player
        transform.Translate(directionToPlayer * moveSpeed * Time.deltaTime, Space.World);

        // Determine the walking status
        isWalking = directionToPlayer.magnitude > 0.1f;
    }

    // Getter for IsWalking status
    public bool IsWalking
    {
        get { return isWalking; }
    }
}
