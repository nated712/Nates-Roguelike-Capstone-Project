using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject enemyPrefab2;
    public GameObject enemyPrefab3;
    public GameObject enemyPrefab4;
    public GameObject bossPrefab;
    public float baseSpawnInterval = 2.5f;  // Starting interval
    public float spawnDistance = 20f;
    public Vector2 spawnRange = new Vector2(20f, 10f);
    public bool debugSpawnArea = true;

    private float currentSpawnInterval; //can be printed to show how fast enemies spawn speed progresses
    private float lastSpawnTime;
    private float timeElapsed;

    private float[] minSpawnIntervals = { 2.0f, 1.4f, 0.8f, 0.3f, 0.05f }; // Based upon 0-2, 2-4, 4-6, 6-8, 10+ minutes

    private void Start()
    {
        currentSpawnInterval = baseSpawnInterval;
        lastSpawnTime = Time.time;
    }

    private void Update()
    {
        timeElapsed = Time.timeSinceLevelLoad;

        float minInterval = GetMinSpawnInterval(timeElapsed);

        // Spawn interval shrinks as a curve
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
        int roll = Random.Range(0, 20);
        if (roll >= 7 && roll <= 8)
        {
            Instantiate(enemyPrefab2, spawnPosition, Quaternion.identity);
        }
        if (roll == 6)
        {
            Instantiate(enemyPrefab3, spawnPosition, Quaternion.identity);
        }
        if (roll == 5)
        {
            Instantiate(enemyPrefab4, spawnPosition, Quaternion.identity);
        }
        else
        {
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        }
        if(timeElapsed == 180f)
        {
            Instantiate(bossPrefab, spawnPosition, Quaternion.identity);
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
        if (timeElapsed < 120f)         // 0–2 min
            return minSpawnIntervals[0];
        else if (timeElapsed < 240f)    // 2–4 min
            return minSpawnIntervals[1];
        else if (timeElapsed < 360f)    // 4–6 min
            return minSpawnIntervals[2];
        else if (timeElapsed < 480f)   // 6–8 min
            return minSpawnIntervals[3];
        else                            // After 8 min
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
