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
    private float currentSpawnInterval; //can be printed to show how fast enemies spawn speed progresses
    private float lastSpawnTime;
    private float timeElapsed;
    private float bossSpawnInterval = 120f;
    private float nextBossSpawnTime = 180f;

    private float[] minSpawnIntervals = { 0.8f, 0.68f, 0.5f, 0.4f, 0.1f }; // Based upon 0-2, 2-4, 4-6, 6-8, 10+ minutes
    //will update with spawn intervals so game gets harder per 2 minutes
    private float[] GetEnemySpawnWeights(float time)
    {
        // Order: enemyPrefab, enemyPrefab2, enemyPrefab3, enemyPrefab4
        if (time < 120f)         // 0–2 min
            return new float[] { 0.8f, 0.10f, 0.05f, 0.05f };
        else if (time < 240f)    // 2–4 min
            return new float[] { 0.5f, 0.15f, 0.2f, 0.15f };
        else if (time < 360f)    // 4–6 min
            return new float[] { 0.2f, 0.2f, 0.4f, 0.2f };
        else if (time < 480f)    // 6–8 min
            return new float[] { 0.05f, 0.25f, 0.3f, 0.4f };
        else                     // 8+ min
            return new float[] { 0.15f, 0.25f, 0.30f, 0.35f };
    }
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
        float difficultyMultiplier = Mathf.Sqrt(timeElapsed) * 0.1f;
        currentSpawnInterval = Mathf.Max(minInterval, baseSpawnInterval - difficultyMultiplier);

        if (timeElapsed >= nextBossSpawnTime)
        {
            print("Boss Spawning");
            Vector3 bossSpawnPos = GetRandomSpawnPosition();
            Instantiate(bossPrefab, bossSpawnPos, Quaternion.identity);
            nextBossSpawnTime += bossSpawnInterval; // Schedule next boss
        }
        if (Time.time - lastSpawnTime >= currentSpawnInterval)
        {
            SpawnEnemy();
            lastSpawnTime = Time.time;
        }
    }

    private void SpawnEnemy()
    {
        Vector3 spawnPosition = GetRandomSpawnPosition();
        float[] weights = GetEnemySpawnWeights(timeElapsed);
        float total = 0f;

        foreach (float w in weights)
            total += w;

        float rand = Random.Range(0f, total);
        float cumulative = 0f;

        if ((cumulative += weights[0]) >= rand)
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        else if ((cumulative += weights[1]) >= rand)
            Instantiate(enemyPrefab2, spawnPosition, Quaternion.identity);
        else if ((cumulative += weights[2]) >= rand)
            Instantiate(enemyPrefab3, spawnPosition, Quaternion.identity);
        else
            Instantiate(enemyPrefab4, spawnPosition, Quaternion.identity);

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
}
