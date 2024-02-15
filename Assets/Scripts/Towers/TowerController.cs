using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public interface ITowerEffect
{
    public void CollisionEffect(Vector3 position);
}

public class TowerController : MonoBehaviour
{
    [SerializeField]
    private TowerSO data;
    private List<GameObject> enemiesInRange = new List<GameObject>();
    private float lastAttackTime;
    private ITowerEffect towerEffect;

    public TowerSO GetData()
    {
        return data;
    }

    void Start()
    {
        lastAttackTime = Time.time;
        if (GetComponent<ITowerEffect>() != null)
        {
            towerEffect = GetComponent<ITowerEffect>();
        }
    }

    void Update()
    {
        UpdateEnemiesInRange();
    }

    void UpdateEnemiesInRange()
    {
        //potentially use a circlecast instead
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
        switch (data.attackType)
        {
            case TowerAttackType.closest:
                AttackClosestEnemy();
                break;
            case TowerAttackType.highestHP:
                AttackHighestHPEnemy();
                break;
            case TowerAttackType.AOE:
                AttackAllEnemies();
                break;
        }
    }

    void CollisionBehaviour(Collider2D collision)
    {
        if (data.debuff)
        {
            collision
                .GetComponent<BuffDebuffController>()
                .ApplyDebuff(data.debuff, data.debuffDuration);
        }
        if (data.DOT)
        {
            collision
                .GetComponent<BuffDebuffController>()
                .ApplyDOT(data.DOT, data.DOTDuration, data.DOTDamage);
        }
        towerEffect.CollisionEffect(collision.transform.position);
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
                projectile.GetComponent<ProjectileController>().SetTarget(closestEnemy.transform);
                projectile.GetComponent<ProjectileController>().OnCollision += CollisionBehaviour;
                lastAttackTime = Time.time;
            }
        }
    }

    void AttackHighestHPEnemy()
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
                GameObject projectile = Instantiate(
                    data.projectilePrefab,
                    transform.position,
                    Quaternion.identity
                );
                projectile.GetComponent<ProjectileController>().SetTarget(highestHPEnemy.transform);
                projectile.GetComponent<ProjectileController>().OnCollision += CollisionBehaviour;
                lastAttackTime = Time.time;
            }
        }
    }

    void AttackAllEnemies()
    {
        if (enemiesInRange.Count > 0 && Time.time - lastAttackTime >= data.attackCooldown)
        {
            foreach (GameObject enemy in enemiesInRange)
            {
                var healthController = enemy.GetComponent<HealthController>();
                healthController.TakeDamage(data.damage);
                CollisionBehaviour(enemy.GetComponent<Collider2D>());
            }
            lastAttackTime = Time.time;
        }
    }

    void OnMouseDown()
    {
        if (!BuildingManager.instance.isBuilding)
        {
            BuildingManager.instance.highlightedBuilding = gameObject;
            UIManager.instance.ShowInfo();
            UIManager.instance.buildingName.text = data.buildingName;
            if (data.tier < 3)
            {
                UIManager.instance.maxUpgradeButtons.SetActive(false);
                UIManager.instance.upgradeButton.SetActive(true);
            }
            else if (data.tier == 3)
            {
                UIManager.instance.maxUpgradeButtons.SetActive(true);
                UIManager.instance.upgradeButton.SetActive(false);
            }
            else if (data.tier == 4)
            {
                UIManager.instance.maxUpgradeButtons.SetActive(false);
                UIManager.instance.upgradeButton.SetActive(false);
            }
        }
    }
}
