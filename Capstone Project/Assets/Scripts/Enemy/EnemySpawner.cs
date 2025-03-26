using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;        // Reference to the enemy prefab
    public float spawnInterval = 2f;      // Time interval between spawns
    public float spawnDistance = 15f;     // Distance from player to spawn enemies
    public Vector2 spawnRange = new Vector2(20f, 10f);  // Defines a rectangular range for spawning (x = width, y = height)

    public bool debugSpawnArea = true;   // Toggle this to turn the debug visualization on/off

    private void Start()
    {
        // Start spawning enemies after a delay
        InvokeRepeating("SpawnEnemy", 0f, spawnInterval);
    }

    private void SpawnEnemy()
    {
        // Randomly choose a direction (off-screen) to spawn the enemy
        Vector3 spawnPosition = GetRandomSpawnPosition();

        // Instantiate the enemy at the calculated position
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }

    private Vector3 GetRandomSpawnPosition()
    {
        // Generate random position based on the player's position and spawn range
        float randomX = Random.Range(transform.position.x - spawnRange.x, transform.position.x + spawnRange.x);
        float randomY = Random.Range(transform.position.y - spawnRange.y, transform.position.y + spawnRange.y);

        // Randomly choose a side (left or right, above or below) to spawn
        if (Random.value > 0.5f)
            randomX = (transform.position.x + spawnDistance * (Random.value > 0.5f ? 1 : -1));  // Spawn off-screen horizontally
        else
            randomY = (transform.position.y + spawnDistance * (Random.value > 0.5f ? 1 : -1));  // Spawn off-screen vertically

        return new Vector3(randomX, randomY, 0f);
    }

    // This function draws the debug spawn area in the Scene view
    private void OnDrawGizmos()
    {
        if (!debugSpawnArea) return;  // Skip drawing if debug is turned off

        // Set the Gizmo color to something visible
        Gizmos.color = Color.green;

        // Calculate the bounds of the spawn area based on player's position and the spawn range
        Vector3 bottomLeft = new Vector3(transform.position.x - spawnRange.x, transform.position.y - spawnRange.y, transform.position.z);
        Vector3 topRight = new Vector3(transform.position.x + spawnRange.x, transform.position.y + spawnRange.y, transform.position.z);

        // Draw a wireframe cube around the spawn area
        Gizmos.DrawLine(bottomLeft, new Vector3(bottomLeft.x, topRight.y, bottomLeft.z)); // Left side
        Gizmos.DrawLine(bottomLeft, new Vector3(topRight.x, bottomLeft.y, bottomLeft.z)); // Bottom side
        Gizmos.DrawLine(topRight, new Vector3(topRight.x, bottomLeft.y, topRight.z));     // Right side
        Gizmos.DrawLine(topRight, new Vector3(bottomLeft.x, topRight.y, topRight.z));     // Top side

        // Optionally, draw a cross or circle in the middle of the spawn area to indicate the center point
        Gizmos.DrawWireSphere(transform.position, 0.5f);  // Draw a small sphere at the spawner position
    }
}
