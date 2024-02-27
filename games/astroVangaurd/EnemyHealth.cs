using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHits = 2;
    private int currentHits = 0;

    public bool isToughEnemy = false; // Set this to true for tough enemies

    public GameObject destroyEffectPrefab; // Prefab to instantiate when enemy is destroyed

    public void TakeDamage(int damage)
    {
        currentHits += damage;

        // If the enemy has reached the maximum hits, destroy it
        if (currentHits >= maxHits)
        {
            InstantiateDestroyEffect();
            Destroy(gameObject);

            // Add points to the PointsSystem when the enemy is destroyed
            PointsSystem pointsSystem = FindObjectOfType<PointsSystem>();
            if (pointsSystem != null)
            {
                pointsSystem.AddPointsOnDestroy(isToughEnemy); // Pass isToughEnemy as an argument to determine the points to add
            }
        }
    }

    private void InstantiateDestroyEffect()
    {
        if (destroyEffectPrefab != null)
        {
            Instantiate(destroyEffectPrefab, transform.position, Quaternion.identity);
        }
    }
}
