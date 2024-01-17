using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractStates
{
    protected FSM fsm;

    public virtual void OnEnter() { }
    public virtual void OnUpdate() { }
    public virtual void OnFixedUpdate() { }
    public virtual void OnExit() { }
    public virtual void OnDestroy() { }
    public virtual void OnCollisionEnter(Collision collision) { }
    public virtual void OnCollisionEnter2D(Collision2D collision) { }
}
public enum StateType { }