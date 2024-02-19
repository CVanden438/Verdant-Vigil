using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class EnemyMoveState : EnemyState
{
    public EnemyMoveState(
        StateMachine<EEnemyState> stateMachine,
        EnemyController e,
        AIPath move,
        AIDestinationSetter t
    )
        : base(stateMachine, e, move, t) { }

    public override void EnterState() { }

    public override void ExitState() { }

    public override void OnCollisionEnter2D(Collision2D other) { }

    public override void OnCollisionExit2D(Collision2D other) { }

    public override void OnCollisionStay2D(Collision2D other) { }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (
            other.GetComponent<PlayerController>()
            || other.GetComponent<WallController>()
            || other.GetComponent<TowerController>()
        )
        {
            Debug.Log("Go to attack");
            target.target = other.transform;
            sm.ChangeState(EEnemyState.Attack);
        }
    }

    public override void OnTriggerExit2D(Collider2D other)
    {
        // if (other.GetComponent<PlayerController>())
        // {
        //     Debug.Log("go to idle");
        //     sm.ChangeState(EEnemyState.Idle);
        // }
    }

    public override void OnTriggerStay2D(Collider2D other) { }

    public override void UpdateState() { }
}
