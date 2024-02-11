using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<EnemySO> enemies; // The prefab of the enemy object to be spawned
    public float spawnRate = 5f; // The time interval between spawns
    public Rect spawnArea;
    private float nextSpawnTime = 0;

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + spawnRate; // Calculate the next spawn time
        }
    }

    void SpawnEnemy()
    {
        // Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        float randomX = UnityEngine.Random.Range(spawnArea.xMin, spawnArea.xMax);
        float randomY = UnityEngine.Random.Range(spawnArea.yMin, spawnArea.yMax);
        Vector3 randomPosition = new(randomX, randomY);
        var num = UnityEngine.Random.Range(0, enemies.Count);
        var enemy = Instantiate(enemies[num].prefab, randomPosition, Quaternion.identity);
        EnemyManager.instance.enemies.Add(enemy);
    }
}
