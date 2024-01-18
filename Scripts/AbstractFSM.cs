using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractFSM : MonoBehaviour
{
    protected AbstractStates currentState;
    protected Dictionary<StateType, AbstractStates> states = new();

    protected virtual void Update() { currentState?.OnUpdate(); }
    protected virtual void FixedUpdate() { currentState?.OnFixedUpdate(); }
    protected virtual void OnDestroy() { currentState?.OnDestroy(); }
    protected virtual void OnCollisionEnter(Collision collision) { currentState?.OnCollisionEnter(collision); }
    protected virtual void OnCollisionEnter2D(Collision2D collision) { currentState.OnCollisionEnter2D(collision); }

    public void AddState(StateType stateType, AbstractStates state)
    {
        if (!states.ContainsKey(stateType)) states.Add(stateType, state);
    }
    public void SwitchState(StateType targetState)
    {
        if (!states.ContainsKey(targetState)) return;
        currentState?.OnExit();
        currentState = states[targetState];
        currentState.OnEnter();
    }
}