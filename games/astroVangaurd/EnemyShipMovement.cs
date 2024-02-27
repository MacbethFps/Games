using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipMovement : MonoBehaviour
{
    private Transform player;
    private float moveSpeed = 50f;

    public void SetPlayer(Transform target)
    {
        player = target;
    }

    public void SetSpeed(float speed)
    {
        moveSpeed = speed;
    }

    void Update()
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
}
