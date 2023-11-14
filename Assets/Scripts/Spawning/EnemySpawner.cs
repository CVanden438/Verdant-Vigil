using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // The prefab of the enemy object to be spawned
    public float spawnRate = 0.2f; // The time interval between spawns
    public Rect spawnArea;
    private float nextSpawnTime;

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + 1f / spawnRate; // Calculate the next spawn time
        }
    }

    void SpawnEnemy()
    {
        // Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        float randomX = Random.Range(spawnArea.xMin, spawnArea.xMax);
        float randomY = Random.Range(spawnArea.yMin, spawnArea.yMax);
        Vector3 randomPosition = new Vector3(randomX, randomY, transform.position.z);

        Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
    }
}
