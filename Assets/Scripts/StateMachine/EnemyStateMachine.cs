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
    [SerializeField]
    private EnemyController enemy;

    private void Awake()
    {
        InitialiseStates();
    }

    private void InitialiseStates()
    {
        States.Add(EEnemyState.Idle, new EnemyIdleState(this, enemy));
        States.Add(EEnemyState.Move, new EnemyMoveState(this, enemy));
        States.Add(EEnemyState.Attack, new EnemyAttackState(this, enemy));
        CurrentState = States[EEnemyState.Idle];
    }
}
