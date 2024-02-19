using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public enum EEnemyState
{
    Idle,
    Move,
    Attack
}

public class EnemyStateMachine : StateMachine<EEnemyState>
{
    [SerializeField]
    private EnemyController enemy;

    [SerializeField]
    private AIPath movement;

    [SerializeField]
    private AIDestinationSetter target;

    private void Awake()
    {
        InitialiseStates();
    }

    private void InitialiseStates()
    {
        States.Add(EEnemyState.Idle, new EnemyIdleState(this, enemy, movement, target));
        States.Add(EEnemyState.Move, new EnemyMoveState(this, enemy, movement, target));
        States.Add(EEnemyState.Attack, new EnemyAttackState(this, enemy, movement, target));
        CurrentState = States[EEnemyState.Move];
    }
}
