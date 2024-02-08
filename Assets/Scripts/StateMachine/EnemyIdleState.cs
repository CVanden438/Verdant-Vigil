using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyState
{
    // public EnemyIdleState(EEnemyState key, StateMachine<EEnemyState> stateMachine)
    //     : base(key, stateMachine) { }

    public override void EnterState() { }

    public override void ExitState() { }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>())
        {
            Debug.Log("Go to move");
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
}
