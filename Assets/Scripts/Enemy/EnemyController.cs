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
    private GameObject target;
    private GameObject player;
    private float lastAttackTime;
    private AIPath path;
    private AIDestinationSetter targetSetter;
    bool isAttacking = false;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        path = GetComponent<AIPath>();
        targetSetter = GetComponent<AIDestinationSetter>();
        player = GameObject.FindWithTag("Player");
        target = player;
        lastAttackTime = Time.time;
        if (TryGetComponent<HealthController>(out var health))
        {
            health._currentHealth = data.maxHealth;
            health._maximumHealth = data.maxHealth;
        }
    }

    // Update is called once per frame
    public void Update()
    {
        if (data.enemyType == EnemyType.range && isAttacking)
        {
            RangeAttack();
        }
        //target has died - careful unity does special stuff with reference to destroyed gameobjects
        if (target == null)
        {
            // var newTarget = Physics2D.OverlapCircle(transform.position, 2);
            // if(newTarget.GetComponent<EnemyController>()){return;}
            if (animator)
            {
                animator.SetBool("isMoving", true);
            }
            isAttacking = false;
            path.enabled = true;
            target = player;
            targetSetter.target = player.transform;
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
            Debug.Log("Target:" + target);
            proj.GetComponent<EnemyProjectile>().target = target;
            proj.GetComponent<EnemyProjectile>().OnCollision += CollisionBehaviour;
            proj.GetComponent<EnemyProjectile>().data = data;
            lastAttackTime = Time.time;
        }
    }

    void CollisionBehaviour(Collider2D collision)
    {
        if (!collision.transform.GetComponent<DebuffController>())
        {
            return;
        }
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

    public void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.GetComponent<EnemyController>())
        {
            return;
        }
        if (data.enemyType == EnemyType.contact && isAttacking)
        {
            if (animator)
            {
                animator.SetBool("isMoving", false);
            }
            var cdMulti = GetComponent<StatModifiers>().AttackSpeedModifier;
            if (collision.gameObject.TryGetComponent<HealthController>(out var health))
            {
                if (Time.time - lastAttackTime >= data.attackCd * cdMulti)
                {
                    health.TakeDamage(data.damage);
                    CollisionBehaviour(collision.collider);
                    lastAttackTime = Time.time;
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<EnemyController>())
        {
            return;
        }
        if (isAttacking)
        {
            return;
        }
        if (other.GetComponent<HealthController>())
        {
            Debug.Log(other.transform);
            target = other.gameObject;
            targetSetter.target = other.transform;
            isAttacking = true;
            if (data.enemyType == EnemyType.range)
            {
                animator.SetBool("isMoving", false);
                path.enabled = false;
            }
        }
    }

    //
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject != target)
        {
            return;
        }
        isAttacking = false;
        path.enabled = true;
    }

    void OnDestroy()
    {
        ResourceManager.instance.AddCoins(10);
    }

    public void Die()
    {
        animator.SetTrigger("isDead");
        path.enabled = false;
        Destroy(gameObject, 1f);
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




//Example check if target desttoyed with event/delegate
// public class Target : MonoBehaviour
// {
//     public delegate void OnDestroyedHandler();
//     public event OnDestroyedHandler OnDestroyed;

//     void OnDestroy()
//     {
//         if (OnDestroyed != null)
//             OnDestroyed();
//     }
// }

// public class Observer : MonoBehaviour
// {
//     public Target target;

//     void OnEnable()
//     {
//         if (target != null)
//             target.OnDestroyed += TargetDestroyed;
//     }

//     void OnDisable()
//     {
//         if (target != null)
//             target.OnDestroyed -= TargetDestroyed;
//     }

//     private void TargetDestroyed()
//     {
//         // Target is about to be destroyed
//         Debug.Log("Target is about to be destroyed.");
//     }
// }
