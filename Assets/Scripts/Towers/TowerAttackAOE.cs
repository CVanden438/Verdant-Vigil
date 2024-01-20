using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAttackAOE : MonoBehaviour
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
        AttackAllEnemies();
    }

    void AttackAllEnemies()
    {
        if (enemiesInRange.Count > 0 && Time.time - lastAttackTime >= data.attackCooldown)
        {
            foreach (GameObject enemy in enemiesInRange)
            {
                var healthController = enemy.GetComponent<HealthController>();
                healthController.TakeDamage(data.damage);
            }
            lastAttackTime = Time.time;
        }
        else
        {
            // animator.SetBool("isAttacking", false);
        }
    }
}
