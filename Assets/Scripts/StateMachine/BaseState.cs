using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState<EState>
    where EState : Enum
{
    // public BaseState(EState key, StateMachine<EState> stateMachine)
    // {
    //     StateKey = key;
    //     sm = stateMachine;
    // }

    public void OnEnterState(StateMachine<EState> stateMachine)
    {
        sm = stateMachine;
        EnterState();
    }

    protected StateMachine<EState> sm;

    // public EState StateKey { get; private set; }
    public abstract void EnterState();
    public abstract void ExitState();
    public abstract void UpdateState();

    public abstract void OnCollisionEnter2D(Collision2D other);
    public abstract void OnTriggerEnter2D(Collider2D other);
    public abstract void OnTriggerStay2D(Collider2D other);
    public abstract void OnTriggerExit2D(Collider2D other);
}
