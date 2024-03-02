using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

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

    [SerializeField]
    private int angleBetweenProj = 10;

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
        if (TryGetComponent<HealthController>(out var health))
        {
            health._currentHealth = data.maxHealth;
            health._maximumHealth = data.maxHealth;
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
            case TowerAttackType.random:
                AttackRandomEnemy();
                break;
        }
    }

    void CollisionBehaviour(Collider2D collision)
    {
        if (data.debuff)
        {
            collision
                .GetComponent<DebuffController>()
                .ApplyDebuff(data.debuff, data.debuffDuration);
        }
        if (data.DOT)
        {
            collision
                .GetComponent<DebuffController>()
                .ApplyDOT(data.DOT, data.DOTDuration, data.DOTDamage);
        }
        towerEffect?.CollisionEffect(collision.transform.position);
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
                if (data.projCount == 0)
                {
                    DirectDamage(closestEnemy);
                }
                SetupProjectile(closestEnemy.transform);
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
                if (data.projCount == 0)
                {
                    DirectDamage(highestHPEnemy);
                }
                SetupProjectile(highestHPEnemy.transform);
                lastAttackTime = Time.time;
            }
        }
    }

    void AttackRandomEnemy()
    {
        if (enemiesInRange.Count > 0 && Time.time - lastAttackTime >= data.attackCooldown)
        {
            var randomNum = Random.Range(0, enemiesInRange.Count);
            var randomEnemy = enemiesInRange[randomNum];
            if (data.projCount == 0)
            {
                DirectDamage(randomEnemy);
            }
            SetupProjectile(randomEnemy.transform);
            lastAttackTime = Time.time;
        }
    }

    void AttackAllEnemies()
    {
        if (enemiesInRange.Count > 0 && Time.time - lastAttackTime >= data.attackCooldown)
        {
            foreach (GameObject enemy in enemiesInRange)
            {
                DirectDamage(enemy);
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

    public void OnMouseEnter()
    {
        TooltipManager.instance.ShowTooltip(data.name, Input.mousePosition);
    }

    public void OnMouseExit()
    {
        Debug.Log("Exit");
        TooltipManager.instance.HideTooltip();
    }

    void SetupProjectile(Transform target)
    {
        Vector2 baseDirection = (target.position - transform.position).normalized;
        int angleOffset = 0;
        for (int i = 0; i < data.projCount; i++)
        {
            angleOffset *= -1;
            // int angleOffset = i * angleBetweenProj;
            if ((i + 1) % 2 == 0 && i != 0)
            {
                angleOffset += angleBetweenProj;
            }
            Quaternion rotation = Quaternion.Euler(0f, 0f, angleOffset);
            Vector2 rotatedDirection = rotation * baseDirection;
            GameObject projectile = Instantiate(
                data.projectilePrefab,
                transform.position,
                Quaternion.identity
            );
            var controller = projectile.GetComponent<ProjectileController>();
            controller.SetDirection(rotatedDirection);
            controller.OnCollision += CollisionBehaviour;
            controller.data = data;
        }
    }

    void DirectDamage(GameObject enemy)
    {
        var healthController = enemy.GetComponent<HealthController>();
        healthController.TakeDamage(data.damage);
        CollisionBehaviour(enemy.GetComponent<Collider2D>());
    }
}
