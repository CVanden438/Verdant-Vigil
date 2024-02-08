using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EEnemyState
{
    Idle,
    Move,
    Attack
}

public class EnemyStateMachine : StateMachine<EEnemyState>
{
    private void Awake()
    {
        InitialiseStates();
    }

    private void InitialiseStates()
    {
        States.Add(EEnemyState.Idle, new EnemyIdleState());
        States.Add(EEnemyState.Move, new EnemyMoveState());
        States.Add(EEnemyState.Attack, new EnemyAttackState());
        CurrentState = States[EEnemyState.Idle];
    }
}
