using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public abstract class StateMachine<EState> : MonoBehaviour
    where EState : Enum
{
    protected Dictionary<EState, BaseState<EState>> States = new();

    protected BaseState<EState> CurrentState;

    void Start()
    {
        CurrentState.EnterState();
    }

    void Update()
    {
        CurrentState?.UpdateState();
    }

    public void ChangeState(EState newState)
    {
        CurrentState?.ExitState();
        CurrentState = States[newState];
        CurrentState.EnterState();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        CurrentState.OnTriggerEnter2D(other);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        CurrentState.OnCollisionEnter2D(other);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        CurrentState.OnTriggerStay2D(other);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        CurrentState.OnTriggerExit2D(other);
    }
}
