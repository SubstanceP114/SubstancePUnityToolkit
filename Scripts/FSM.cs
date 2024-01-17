using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM
{
    public AbstractStates currentState;
    public Dictionary<StateType, AbstractStates> states = new();
    public Blackboard blackboard;
    public FSM(Blackboard blackboard) { this.blackboard = blackboard; }

    public void Update() { currentState?.OnUpdate(); }
    public void FixedUpdate() { currentState?.OnFixedUpdate(); }
    public void OnDestroy() { currentState?.OnDestroy(); }
    public void OnCollisionEnter(Collision collision) { currentState?.OnCollisionEnter(collision); }
    public void OnCollisionEnter2D(Collision2D collision) { currentState.OnCollisionEnter2D(collision); }

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
[Serializable]
public class Blackboard
{

}