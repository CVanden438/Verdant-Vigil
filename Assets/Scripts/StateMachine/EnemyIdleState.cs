using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class EnemyIdleState : EnemyState
{
    public EnemyIdleState(
        StateMachine<EEnemyState> stateMachine,
        EnemyController e,
        AIPath move,
        AIDestinationSetter t
    )
        : base(stateMachine, e, move, t) { }

    public override void EnterState() { }

    public override void ExitState() { }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>())
        {
            sm.ChangeState(EEnemyState.Move);
        }
    }

    public override void OnCollisionEnter2D(Collision2D other)
    {
        // if (other.collider.GetComponent<PlayerController>())
        // {
        //     Debug.Log("TEST");
        //     sm.ChangeState(EEnemyState.Move);
        // }
    }

    public override void OnTriggerExit2D(Collider2D other) { }

    public override void OnTriggerStay2D(Collider2D other) { }

    public override void UpdateState() { }

    public override void OnCollisionExit2D(Collision2D other) { }

    public override void OnCollisionStay2D(Collision2D other) { }
}
