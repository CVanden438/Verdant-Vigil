using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAttackHighestHP : MonoBehaviour
{
    [SerializeField]
    private TowerSO data;
    private List<GameObject> enemiesInRange = new List<GameObject>();
    private float lastAttackTime;
    private int shotCount = 0;
    private int superShotInterval = 3;
    private int superShotMultiplier = 3;

    void Start()
    {
        lastAttackTime = Time.time;
        InvokeRepeating("UpdateEnemiesInRange", 0f, 1f);
    }

    void UpdateEnemiesInRange()
    {
        enemiesInRange.Clear();

        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in allEnemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance <= data.attackRange)
            {
                enemiesInRange.Add(enemy);
            }
        }
        AttackClosestEnemy();
    }

    void AttackClosestEnemy()
    {
        if (enemiesInRange.Count > 0 && Time.time - lastAttackTime >= data.attackCooldown)
        {
            GameObject highestHPEnemy = null;
            float highestHP = 0;

            foreach (GameObject enemy in enemiesInRange)
            {
                float health = enemy.GetComponent<HealthController>().GetHealth();
                Debug.Log(health);
                if (health > highestHP)
                {
                    highestHP = health;
                    highestHPEnemy = enemy;
                }
            }

            if (highestHPEnemy != null)
            {
                shotCount += 1;
                // animator.SetBool("isAttacking", true);
                // Instantiate a projectile and make it attack the enemy
                GameObject projectile = Instantiate(
                    data.projectilePrefab,
                    transform.position,
                    Quaternion.identity
                );
                projectile.GetComponent<ProjectileController>().SetTarget(highestHPEnemy.transform);
                if (shotCount < superShotInterval)
                {
                    projectile.GetComponent<ProjectileController>().SetDamage(data.damage);
                }
                else
                {
                    projectile
                        .GetComponent<ProjectileController>()
                        .SetDamage(data.damage * superShotMultiplier);
                    shotCount = 0;
                }
                lastAttackTime = Time.time;
            }
        }
        else
        {
            // animator.SetBool("isAttacking", false);
        }
    }
}
