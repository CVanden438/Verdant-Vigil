using System;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public delegate void CollisionAction(Collider2D collision);
    public event CollisionAction OnCollision;
    private Transform target;
    private Rigidbody2D rb;
    private float force = 5;
    private List<GameObject> enemiesInRange = new List<GameObject>();

    [SerializeField]
    private TowerSO data;
    private int pierceCount = 0;
    private int chainCount = 0;

    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    void Start()
    {
        Move();
    }

    private void Move()
    {
        rb = GetComponent<Rigidbody2D>();
        Vector3 dir = target.position - transform.position;
        rb.velocity = new Vector2(dir.x, dir.y).normalized * force;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<EnemyMovement>())
        {
            if (data.aoe == 0)
            {
                var healthController = collision.gameObject.GetComponent<HealthController>();
                healthController.TakeDamage(data.damage);
                OnCollision?.Invoke(collision);
            }
            else
            {
                AOEDamage();
            }
            pierceCount += 1;
            if (pierceCount >= data.maxPierce)
            {
                if (chainCount < data.maxChain)
                {
                    chainCount += 1;
                    UpdateEnemiesInRange();
                }
                else
                {
                    Destroy(gameObject);
                }
            }
        }
    }

    void UpdateEnemiesInRange()
    {
        enemiesInRange.Clear();

        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closestEnemy = null;
        float closestDistance = 999;
        foreach (GameObject enemy in allEnemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance <= data.attackRange)
            {
                enemiesInRange.Add(enemy);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEnemy = enemy;
                }
            }
        }
        if (enemiesInRange.Count > 0)
        {
            enemiesInRange.Remove(closestEnemy);
        }
        ChainAttack();
    }

    void ChainAttack()
    {
        if (enemiesInRange.Count > 0)
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
                SetTarget(closestEnemy.transform);
                Move();
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void AOEDamage()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(
            transform.position,
            data.aoe
        // attackableLayer
        );
        foreach (var enemy in hitEnemies)
        {
            if (enemy.GetComponent<EnemyMovement>())
            {
                var healthController = enemy.gameObject.GetComponent<HealthController>();
                healthController.TakeDamage(data.damage);
                OnCollision?.Invoke(enemy);
            }
        }
    }
}
