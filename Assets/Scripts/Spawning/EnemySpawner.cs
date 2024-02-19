using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<EnemySO> enemies; // The prefab of the enemy object to be spawned
    public float spawnRate = 5f; // The time interval between spawns
    public Rect spawnArea;
    public Rect spawnArea2;
    public Rect spawnArea3;
    private float nextSpawnTime = 0;

    // private List<EnemySO> spawnList = new();
    public int enemyBudget = 10;
    public int waveNumber = 1;

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            // SpawnEnemy();
            // nextSpawnTime = Time.time + spawnRate; // Calculate the next spawn time
        }
    }

    void SpawnEnemy(EnemySO enemy)
    {
        // Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        float randomX = UnityEngine.Random.Range(spawnArea.xMin, spawnArea.xMax);
        float randomY = UnityEngine.Random.Range(spawnArea.yMin, spawnArea.yMax);
        Vector3 randomPosition = new(randomX, randomY);
        float randomX2 = UnityEngine.Random.Range(spawnArea2.xMin, spawnArea2.xMax);
        float randomY2 = UnityEngine.Random.Range(spawnArea2.yMin, spawnArea2.yMax);
        Vector3 randomPosition2 = new(randomX2, randomY2);
        float randomX3 = UnityEngine.Random.Range(spawnArea3.xMin, spawnArea3.xMax);
        float randomY3 = UnityEngine.Random.Range(spawnArea3.yMin, spawnArea3.yMax);
        Vector3 randomPosition3 = new(randomX3, randomY3);
        var randomNum = UnityEngine.Random.Range(0, 3);
        Vector3 spawnPos = randomPosition;
        switch (randomNum)
        {
            case 0:
                spawnPos = randomPosition;
                break;
            case 1:
                spawnPos = randomPosition2;
                break;
            case 2:
                spawnPos = randomPosition3;
                break;
        }
        var newEnemy = Instantiate(enemy.prefab, spawnPos, Quaternion.identity);
        //could be on enemy script itself
        EnemyManager.instance.enemies.Add(newEnemy);
    }

    public void SpawnWave()
    {
        List<EnemySO> spawnList = new();

        int remainingValue = enemyBudget * waveNumber;
        while (remainingValue > 0)
        {
            var randomNumber = UnityEngine.Random.Range(0, enemies.Count);
            var randomEnemy = enemies[3];
            if (randomEnemy.difficulty <= remainingValue)
            {
                spawnList.Add(randomEnemy);
                remainingValue -= randomEnemy.difficulty;
            }
        }
        foreach (var enemy in spawnList)
        {
            SpawnEnemy(enemy);
        }
    }
}
