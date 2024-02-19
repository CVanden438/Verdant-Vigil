using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class EnemyAttackState : EnemyState
{
    public EnemyAttackState(
        StateMachine<EEnemyState> stateMachine,
        EnemyController e,
        AIPath move,
        AIDestinationSetter t
    )
        : base(stateMachine, e, move, t) { }

    public override void EnterState()
    {
        if (enemy.data.enemyType == EnemyType.range)
        {
            movement.enabled = false;
        }
    }

    public override void ExitState()
    {
        movement.enabled = true;
    }

    public override void UpdateState()
    {
        if (enemy.data.enemyType == EnemyType.range)
        {
            enemy.AttackUpdate(target.target);
        }
    }

    public override void OnCollisionEnter2D(Collision2D other) { }

    public override void OnTriggerEnter2D(Collider2D other) { }

    public override void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>())
        {
            Debug.Log("go to move");
            sm.ChangeState(EEnemyState.Move);
        }
    }

    public override void OnTriggerStay2D(Collider2D other)
    {
        // enemy.CollisionAttack(other);
    }

    public override void OnCollisionExit2D(Collision2D other) { }

    public override void OnCollisionStay2D(Collision2D other)
    {
        enemy.CollisionAttack(other.collider);
    }
}
