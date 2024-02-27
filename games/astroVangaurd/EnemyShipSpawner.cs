using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class EnemyWave
{
    public int numberOfGroups; // Number of groups of enemies to spawn in this wave
    public int minEnemiesInGroup; // Minimum number of enemies in each group
    public int maxEnemiesInGroup; // Maximum number of enemies in each group
    public float groupInterval; // Time interval between spawning each group of enemies
    public float spawnDistanceFromPlayer; // Distance between the player and the spawned enemies

    public List<GameObject> enemyPrefabs; // List of available enemy prefabs for this wave
    public List<float> enemySpeeds; // Speeds for each enemy type in this wave
}

public class EnemyShipSpawner : MonoBehaviour
{
    public List<EnemyWave> enemyWaves;
    public float waveInterval = 10f; // Time between each wave
    public float enemyLifetime = 10f; // Lifetime for spawned enemies

    private Camera mainCamera;
    private Transform playerTransform;
    private bool isSpawningWave = false;

    private PointsSystem pointsSystem; // Reference to the PointsSystem script

    private int currentWaveIndex = 0; // Keep track of the current wave index
    private int waveNumber = 1; // Keep track of the wave number

    public Text waveNumberText; // Reference to the UI Text element to display wave number

    void Start()
    {
        mainCamera = Camera.main;
        playerTransform = GameObject.FindGameObjectWithTag("player").transform;

        // Check if enemyWaves list is empty
        if (enemyWaves.Count == 0)
        {
            Debug.LogWarning("No enemy waves found. Please add enemy waves to the enemyWaves list in the Inspector.");
        }

        // Find the PointsSystem script attached to a GameObject in the scene
        pointsSystem = FindObjectOfType<PointsSystem>();
        if (pointsSystem == null)
        {
            Debug.LogWarning("PointsSystem script not found in the scene. Make sure the PointsSystem script is attached to a GameObject.");
        }

        InvokeRepeating("SpawnWave", 0f, waveInterval);
    }

    void SpawnWave()
    {
        if (!isSpawningWave && enemyWaves.Count > 0)
        {
            // Enable the Wave Number Text object and set the wave number in the UI text
            if (waveNumberText != null)
            {
                waveNumberText.gameObject.SetActive(true);
                waveNumberText.text = "Wave: " + waveNumber.ToString();
            }

            StartCoroutine(SpawnEnemiesInWave(currentWaveIndex));
        }
    }

    IEnumerator<WaitForSeconds> SpawnEnemiesInWave(int waveIndex)
    {
        isSpawningWave = true;
        EnemyWave currentWave = enemyWaves[waveIndex];

        for (int i = 0; i < currentWave.numberOfGroups; i++)
        {
            int numberOfEnemies = Random.Range(currentWave.minEnemiesInGroup, currentWave.maxEnemiesInGroup + 1);
            List<int> availableEdges = new List<int> { 0, 1, 2, 3 }; // Store available edges for randomization

            for (int j = 0; j < numberOfEnemies; j++)
            {
                if (availableEdges.Count == 0)
                {
                    Debug.LogWarning("Not enough available edges for the required number of enemies in a group. Aborting the current group.");
                    break;
                }

                int randomEdgeIndex = Random.Range(0, availableEdges.Count);
                int selectedEdge = availableEdges[randomEdgeIndex];
                availableEdges.RemoveAt(randomEdgeIndex); // Remove the selected edge from the availableEdges list

                Vector3 spawnPosition = GetRandomEdgeSpawnPosition(selectedEdge);
                Vector3 worldSpawnPosition = spawnPosition + Random.insideUnitSphere * currentWave.spawnDistanceFromPlayer;
                worldSpawnPosition.z = 0f;

                int randomEnemyIndex = Random.Range(0, currentWave.enemyPrefabs.Count);
                GameObject randomEnemyPrefab = currentWave.enemyPrefabs[randomEnemyIndex];
                GameObject enemy = Instantiate(randomEnemyPrefab, worldSpawnPosition, Quaternion.identity);

                // Setup enemy behavior (e.g., movement, shooting) as needed

                EnemyShipMovement enemyShipMovement = enemy.GetComponent<EnemyShipMovement>();
                if (enemyShipMovement != null)
                {
                    enemyShipMovement.SetPlayer(playerTransform);

                    if (randomEnemyIndex < currentWave.enemySpeeds.Count)
                    {
                        float enemySpeed = currentWave.enemySpeeds[randomEnemyIndex];
                        enemyShipMovement.SetSpeed(enemySpeed);
                    }
                }

                // Destroy the enemy ship after a certain amount of time (e.g., enemyLifetime)
                Destroy(enemy, enemyLifetime);
            }

            // Delay between spawning each group of enemies
            yield return new WaitForSeconds(currentWave.groupInterval);
        }

        // Increase the wave number and call the method to increase the multiplier
        waveNumber++;
        pointsSystem.IncreaseMultiplier();

        // Disable the Wave Number Text object after the wave is spawned
        if (waveNumberText != null)
        {
            waveNumberText.gameObject.SetActive(false);
        }

        isSpawningWave = false;
        currentWaveIndex++; // Move to the next wave
        if (currentWaveIndex >= enemyWaves.Count)
        {
            currentWaveIndex = 0; // Loop back to the first wave if the last wave is reached
        }
    }

    Vector3 GetRandomEdgeSpawnPosition(int edgeSide)
    {
        float cameraHeight = mainCamera.orthographicSize * 2f;
        float cameraWidth = cameraHeight * mainCamera.aspect;
        Vector3 spawnPosition = Vector3.zero;

        switch (edgeSide)
        {
            case 0: // Top side
                spawnPosition = new Vector3(Random.Range(-cameraWidth / 2f, cameraWidth / 2f), cameraHeight / 2f, 0f);
                break;
            case 1: // Left side
                spawnPosition = new Vector3(-cameraWidth / 2f, Random.Range(-cameraHeight / 2f, cameraHeight / 2f), 0f);
                break;
            case 2: // Right side
                spawnPosition = new Vector3(cameraWidth / 2f, Random.Range(-cameraHeight / 2f, cameraHeight / 2f), 0f);
                break;
            case 3: // Bottom side
                spawnPosition = new Vector3(Random.Range(-cameraWidth / 2f, cameraWidth / 2f), -cameraHeight / 2f, 0f);
                break;
        }

        return spawnPosition;
    }
}
