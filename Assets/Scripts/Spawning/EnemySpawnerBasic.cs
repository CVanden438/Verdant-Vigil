using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerBasic : MonoBehaviour
{
    // Start is called before the first frame update
    private float nextspawnTime;
    public List<EnemySO> enemies;
    private float spawnRate = 10;
    public Vector3 spawnPos;

    void Start()
    {
        nextspawnTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextspawnTime)
        {
            SpawnEnemy();
            SpawnEnemy();
            SpawnEnemy();
            SpawnEnemy();
            SpawnEnemy();
            nextspawnTime += spawnRate;
        }
    }

    void SpawnEnemy()
    {
        var num = UnityEngine.Random.Range(-5, 5);
        int rand = UnityEngine.Random.Range(0, enemies.Count);
        Instantiate(enemies[rand].prefab, spawnPos + new Vector3(num, num), Quaternion.identity);
    }
}
