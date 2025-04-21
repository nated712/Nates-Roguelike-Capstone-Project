using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float baseSpawnInterval = 2f;  // Starting interval
    public float minSpawnInterval = 0.01f; // Minimum cap for spawn speed
    public float spawnDistance = 15f;
    public Vector2 spawnRange = new Vector2(20f, 10f);
    public bool debugSpawnArea = true;

    private float currentSpawnInterval;
    private float lastSpawnTime;

    private void Start()
    {
        currentSpawnInterval = baseSpawnInterval;
        lastSpawnTime = Time.time;
    }

    private void Update()
    {
        // Calculate spawn interval based on time since level load
        float timeElapsed = Time.timeSinceLevelLoad;
        currentSpawnInterval = Mathf.Max(minSpawnInterval, baseSpawnInterval - (timeElapsed / 60f)); // Every 60s, reduce interval
        //print(currentSpawnInterval);
        // Spawn based on interval
        if (Time.time - lastSpawnTime >= currentSpawnInterval)
        {
            SpawnEnemy();
            lastSpawnTime = Time.time;
        }
    }

    private void SpawnEnemy()
    {
        Vector3 spawnPosition = GetRandomSpawnPosition();
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }

    private Vector3 GetRandomSpawnPosition()
    {
        float randomX = Random.Range(transform.position.x - spawnRange.x, transform.position.x + spawnRange.x);
        float randomY = Random.Range(transform.position.y - spawnRange.y, transform.position.y + spawnRange.y);

        if (Random.value > 0.5f)
            randomX = transform.position.x + spawnDistance * (Random.value > 0.5f ? 1 : -1);
        else
            randomY = transform.position.y + spawnDistance * (Random.value > 0.5f ? 1 : -1);
    
        return new Vector3(randomX, randomY, 0f);
    }

    private void OnDrawGizmos()
    {
        if (!debugSpawnArea) return;

        Gizmos.color = Color.green;
        Vector3 bottomLeft = new Vector3(transform.position.x - spawnRange.x, transform.position.y - spawnRange.y, transform.position.z);
        Vector3 topRight = new Vector3(transform.position.x + spawnRange.x, transform.position.y + spawnRange.y, transform.position.z);

        Gizmos.DrawLine(bottomLeft, new Vector3(bottomLeft.x, topRight.y, bottomLeft.z));
        Gizmos.DrawLine(bottomLeft, new Vector3(topRight.x, bottomLeft.y, bottomLeft.z));
        Gizmos.DrawLine(topRight, new Vector3(topRight.x, bottomLeft.y, topRight.z));
        Gizmos.DrawLine(topRight, new Vector3(bottomLeft.x, topRight.y, topRight.z));
        Gizmos.DrawWireSphere(transform.position, 0.5f);
    }
}
