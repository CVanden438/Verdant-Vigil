using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public abstract class EnemyState : BaseState<EEnemyState>
{
    // protected EnemyState(EEnemyState key, StateMachine<EEnemyState> stateMachine)
    //     : base(key, stateMachine) { }
    protected EnemyState(
        StateMachine<EEnemyState> stateMachine,
        EnemyController e,
        AIPath move,
        AIDestinationSetter t
    )
        : base(stateMachine)
    {
        enemy = e;
        movement = move;
        target = t;
    }

    protected EnemyController enemy;
    protected AIPath movement;
    protected AIDestinationSetter target;
}
