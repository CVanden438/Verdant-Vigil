using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveState : EnemyState
{
    // public EnemyMoveState(EEnemyState key, StateMachine<EEnemyState> stateMachine)
    //     : base(key, stateMachine) { }

    public override void EnterState() { }

    public override void ExitState() { }

    public override void OnCollisionEnter2D(Collision2D other) { }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>())
        {
            Debug.Log("Go to attack");
            sm.ChangeState(EEnemyState.Attack);
        }
    }

    public override void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>())
        {
            Debug.Log("go to idle");
            sm.ChangeState(EEnemyState.Idle);
        }
    }

    public override void OnTriggerStay2D(Collider2D other) { }

    public override void UpdateState() { }
}
