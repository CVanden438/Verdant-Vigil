using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState : BaseState<EEnemyState>
{
    // protected EnemyState(EEnemyState key, StateMachine<EEnemyState> stateMachine)
    //     : base(key, stateMachine) { }
    protected EnemyState(StateMachine<EEnemyState> stateMachine, EnemyController e)
        : base(stateMachine)
    {
        enemy = e;
    }

    protected EnemyController enemy;
}
