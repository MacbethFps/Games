using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int maxEnemies = 5;
    public float spawnRadius = 20f;
    public float followDistance = 10f; // Maximum distance for enemies to follow player

    private Transform player;
    private Terrain terrain;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        terrain = Terrain.activeTerrain;
        SpawnEnemies();
    }

    private void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length < maxEnemies)
        {
            SpawnEnemies();
        }
    }

    private void SpawnEnemies()
    {
        Vector2 randomCirclePos = Random.insideUnitCircle.normalized * spawnRadius;
        Vector3 spawnPosition = player.position + new Vector3(randomCirclePos.x, 0f, randomCirclePos.y);

        // Raycast to determine terrain height at the spawn position
        RaycastHit hit;
        if (Physics.Raycast(new Vector3(spawnPosition.x, terrain.transform.position.y + 1000f, spawnPosition.z), Vector3.down, out hit, Mathf.Infinity, LayerMask.GetMask("Terrain")))
        {
            spawnPosition.y = hit.point.y;
        }

        GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        EnemyMovement enemyMovement = newEnemy.GetComponent<EnemyMovement>();
        enemyMovement.followDistance = followDistance; // Assign the follow distance to the spawned enemy
    }
}
