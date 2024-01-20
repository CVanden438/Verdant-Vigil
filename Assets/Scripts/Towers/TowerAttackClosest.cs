using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAttackClosest : MonoBehaviour
{
    [SerializeField]
    private TowerSO data;
    private List<GameObject> enemiesInRange = new List<GameObject>();
    private float lastAttackTime;

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
            GameObject closestEnemy = null;
            float closestDistance = float.MaxValue;

            foreach (GameObject enemy in enemiesInRange)
            {
                float distance = Vector3.Distance(transform.position, enemy.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEnemy = enemy;
                }
            }

            if (closestEnemy != null)
            {
                // animator.SetBool("isAttacking", true);
                // Instantiate a projectile and make it attack the enemy
                GameObject projectile = Instantiate(
                    data.projectilePrefab,
                    transform.position,
                    Quaternion.identity
                );
                projectile.GetComponent<TowerProjectile>().SetTarget(closestEnemy.transform);
                projectile.GetComponent<TowerProjectile>().SetDamage(data.damage);
                lastAttackTime = Time.time;
            }
        }
        else
        {
            // animator.SetBool("isAttacking", false);
        }
    }
}
