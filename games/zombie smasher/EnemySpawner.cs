using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int maxEnemies = 5;

    public Terrain terrain; // Reference to the terrain.

    private void Start()
    {
        for (int i = 0; i < maxEnemies; i++)
        {
            SpawnEnemies();
        }
    }

    private void SpawnEnemies()
    {
        if (terrain == null)
        {
            Debug.LogError("Terrain reference is not set. Assign the terrain in the Inspector.");
            return;
        }

        Vector3 spawnPosition;

        do
        {
            float x = Random.Range(0f, terrain.terrainData.size.x);
            float z = Random.Range(0f, terrain.terrainData.size.z);

            // Determine the terrain height at the random position
            float y = terrain.SampleHeight(new Vector3(x, 0, z));

            spawnPosition = new Vector3(x, y, z);
        }
        while (!IsSpawnPositionValid(spawnPosition));

        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }

    private bool IsSpawnPositionValid(Vector3 position)
    {
        // You can add custom checks here to determine if the spawn position is valid.
        // For example, you may want to check for obstructions or specific terrain features.
        // Return true if the position is valid, and false if it's not.

        return true; // Modify this based on your game's requirements.
    }
}
