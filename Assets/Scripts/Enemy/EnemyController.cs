using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Start is called before the first frame update
    public EnemySO data;

    [SerializeField]
    private GameObject player;
    private float lastAttackTime;

    //ll
    void Start()
    {
        // player = GameObject.FindWithTag("Player");
        lastAttackTime = Time.time;
    }

    // Update is called once per frame
    public void AttackUpdate()
    {
        if (data.enemyType == EnemyType.range)
        {
            RangeAttack();
        }
    }

    public void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private void RangeAttack()
    {
        var cdMulti = GetComponent<StatModifiers>().AttackSpeedModifier;
        if (Time.time - lastAttackTime >= data.attackCd * cdMulti)
        {
            var proj = Instantiate(data.projectilePrefab, transform.position, quaternion.identity);
            proj.GetComponent<EnemyProjectile>().Target = player;
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
                .GetComponent<BuffDebuffController>()
                .ApplyDebuff(data.debuff, data.debuffDuration);
        }
        if (data.DOT)
        {
            collision
                .GetComponent<BuffDebuffController>()
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
    //                 .GetComponent<BuffDebuffController>()
    //                 .ApplyDebuff(weaponData.meleeDebuff, weaponData.meleeDebuffDuration);
    //         }
    //         if (weaponData.meleeDOT)
    //         {
    //             enemy
    //                 .GetComponent<BuffDebuffController>()
    //                 .ApplyDOT(
    //                     weaponData.meleeDebuff,
    //                     weaponData.meleeDebuffDuration,
    //                     weaponData.meleeDOTDuration
    //                 );
    //         }
    //     }
    // }
    public void CollisionAttack(Collision2D collision)
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
                        CollisionBehaviour(collision.collider);
                        lastAttackTime = Time.time;
                    }
                }
            }
        }
    }
}
