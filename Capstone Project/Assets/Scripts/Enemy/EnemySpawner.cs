using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject enemyPrefab2;
    public float baseSpawnInterval = 3.5f;  // Starting interval
    public float spawnDistance = 20f;
    public Vector2 spawnRange = new Vector2(20f, 10f);
    public bool debugSpawnArea = true;

    private float currentSpawnInterval;
    private float lastSpawnTime;

    private float[] minSpawnIntervals = { 1.0f, 0.7f, 0.5f, 0.3f, 0.2f }; // Based upon 0-5, 5-10, 10-15, 15-20, 20+ minutes

    private void Start()
    {
        currentSpawnInterval = baseSpawnInterval;
        lastSpawnTime = Time.time;
    }

    private void Update()
    {
        float timeElapsed = Time.timeSinceLevelLoad;

        float minInterval = GetMinSpawnInterval(timeElapsed);

        // Spawn interval shrinks faster using a sharper curve
        float difficultyMultiplier = Mathf.Sqrt(timeElapsed) * 0.5f;
        currentSpawnInterval = Mathf.Max(minInterval, baseSpawnInterval - difficultyMultiplier);

        if (Time.time - lastSpawnTime >= currentSpawnInterval)
        {
            SpawnEnemy();
            lastSpawnTime = Time.time;
        }
    }

    private void SpawnEnemy()
    {
        Vector3 spawnPosition = GetRandomSpawnPosition();
        int roll = Random.Range(0, 8);
        if (roll == 7)
        {
            Instantiate(enemyPrefab2, spawnPosition, Quaternion.identity);
        }
        else
        {
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        }
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

    private float GetMinSpawnInterval(float timeElapsed)
    {
        if (timeElapsed < 300f)         // 0–5 min
            return minSpawnIntervals[0];
        else if (timeElapsed < 600f)    // 5–10 min
            return minSpawnIntervals[1];
        else if (timeElapsed < 900f)    // 10–15 min
            return minSpawnIntervals[2];
        else if (timeElapsed < 1200f)   // 15–20 min
            return minSpawnIntervals[3];
        else                            // After 20 min
            return minSpawnIntervals[4];
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
