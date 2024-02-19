using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using Unity.Mathematics;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Start is called before the first frame update
    public EnemySO data;

    [SerializeField]
    private GameObject player;
    private float lastAttackTime;

    void Start()
    {
        // player = GameObject.FindWithTag("Player");
        lastAttackTime = Time.time;
        if (TryGetComponent<HealthController>(out var health))
        {
            health._currentHealth = data.maxHealth;
            health._maximumHealth = data.maxHealth;
        }
    }

    // Update is called once per frame
    public void AttackUpdate(Transform target)
    {
        if (data.enemyType == EnemyType.range)
        {
            RangeAttack(target);
        }
    }

    public void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private void RangeAttack(Transform target)
    {
        var cdMulti = GetComponent<StatModifiers>().AttackSpeedModifier;
        if (Time.time - lastAttackTime >= data.attackCd * cdMulti)
        {
            var proj = Instantiate(data.projectilePrefab, transform.position, quaternion.identity);
            proj.GetComponent<EnemyProjectile>().target = target.gameObject;
            proj.GetComponent<EnemyProjectile>().OnCollision += CollisionBehaviour;
            proj.GetComponent<EnemyProjectile>().data = data;
            lastAttackTime = Time.time;
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
    }

    // private void MeleeAttack()
    // {
    //     Collider2D[] hits = Physics2D.OverlapCircleAll(
    //         weaponTransform.position,
    //         weaponData.meleeRange,
    //         attackableLayer
    //     );
    //     foreach (Collider2D enemy in hitEnemies)
    //     {
    //         var healthController = enemy.gameObject.GetComponent<HealthController>();
    //         healthController.TakeDamage(weaponData.meleeDamage);
    //         if (weaponData.meleeDebuff)
    //         {
    //             enemy
    //                 .GetComponent<DebuffController>()
    //                 .ApplyDebuff(weaponData.meleeDebuff, weaponData.meleeDebuffDuration);
    //         }
    //         if (weaponData.meleeDOT)
    //         {
    //             enemy
    //                 .GetComponent<DebuffController>()
    //                 .ApplyDOT(
    //                     weaponData.meleeDebuff,
    //                     weaponData.meleeDebuffDuration,
    //                     weaponData.meleeDOTDuration
    //                 );
    //         }
    //     }
    // }
    public void CollisionAttack(Collider2D collision)
    {
        if (data.enemyType == EnemyType.contact)
        {
            var cdMulti = GetComponent<StatModifiers>().AttackSpeedModifier;
            if (collision.gameObject.CompareTag("Player"))
            {
                if (Time.time - lastAttackTime >= data.attackCd * cdMulti)
                {
                    if (collision.gameObject.GetComponent<HealthController>())
                    {
                        var healthController =
                            collision.gameObject.GetComponent<HealthController>();

                        healthController.TakeDamage(data.damage);
                        CollisionBehaviour(collision);
                        lastAttackTime = Time.time;
                    }
                }
            }
        }
    }
}
