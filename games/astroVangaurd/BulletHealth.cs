using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHealth : MonoBehaviour
{
    public int maxHits = 1;
    private int currentHits = 0;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EnemyBullet") || other.CompareTag("enemy"))
        {
            currentHits++;

            // Check if the bullet has hit something once and destroy it
            if (currentHits >= maxHits)
            {
                Destroy(gameObject);
            }
        }
    }
}
