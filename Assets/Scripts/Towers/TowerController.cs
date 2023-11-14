using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    public float attackRange = 10f;
    public float attackCooldown = 2f;
    public GameObject projectilePrefab;
    public int crystalCost = 1;
    private List<GameObject> enemiesInRange = new List<GameObject>();
    private float lastAttackTime;

    [SerializeField]
    private Transform crystalTransform;

    // [SerializeField]
    // private Animator animator;

    void Start()
    {
        lastAttackTime = Time.time;
        InvokeRepeating("UpdateEnemiesInRange", 0f, 1f); // Update the list of enemies every second
        // animator = GetComponentInChildren<Animator>();
    }

    void UpdateEnemiesInRange()
    {
        enemiesInRange.Clear();

        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in allEnemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance <= attackRange)
            {
                enemiesInRange.Add(enemy);
            }
        }

        AttackClosestEnemy();
    }

    void AttackClosestEnemy()
    {
        if (enemiesInRange.Count > 0 && Time.time - lastAttackTime >= attackCooldown)
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
                    projectilePrefab,
                    crystalTransform.position,
                    Quaternion.identity
                );
                projectile.GetComponent<TowerProjectile>().SetTarget(closestEnemy.transform);
                lastAttackTime = Time.time;
            }
        }
        else
        {
            // animator.SetBool("isAttacking", false);
        }
    }
}
