using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public GameObject asteroidPrefab;
    public float spawnInterval = 2f;
    public float asteroidSpeed = 5f;

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        InvokeRepeating("SpawnAsteroid", 0f, spawnInterval);
    }

    void SpawnAsteroid()
    {
        Vector3 spawnPosition = GetRandomEdgeSpawnPosition();
        Vector3 worldSpawnPosition = mainCamera.ScreenToWorldPoint(spawnPosition);

        // Set the z-coordinate to zero to ensure the asteroid is on the same plane as the camera
        worldSpawnPosition.z = 0f;

        // Instantiate the asteroid prefab at the calculated spawn position
        GameObject asteroid = Instantiate(asteroidPrefab, worldSpawnPosition, Quaternion.identity);

        // Calculate the direction from the spawn position to the camera's position
        Vector3 directionToCamera = mainCamera.transform.position - worldSpawnPosition;

        // Calculate the target position based on the movement direction from spawn to camera
        Vector3 targetPosition = worldSpawnPosition + directionToCamera.normalized * 10f; // Set the desired distance

        // Get the asteroid movement script and set the target position and speed
        AsteroidMovement asteroidMovement = asteroid.GetComponent<AsteroidMovement>();
        asteroidMovement.SetTargetPosition(targetPosition);
        asteroidMovement.SetSpeed(asteroidSpeed);
    }

    Vector3 GetRandomEdgeSpawnPosition()
    {
        // Determine the spawn side randomly (top, left, right, or bottom)
        int spawnSide = Random.Range(0, 4);

        // Calculate the screen coordinates based on the chosen side
        switch (spawnSide)
        {
            case 0: // Top side
                return new Vector3(Random.Range(0f, Screen.width), Screen.height + 2f, 0f);
            case 1: // Left side
                return new Vector3(-2f, Random.Range(0f, Screen.height), 0f);
            case 2: // Right side
                return new Vector3(Screen.width + 2f, Random.Range(0f, Screen.height), 0f);
            case 3: // Bottom side
                return new Vector3(Random.Range(0f, Screen.width), -2f, 0f);
            default:
                return Vector3.zero;
        }
    }
}
