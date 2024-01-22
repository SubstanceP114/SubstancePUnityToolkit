using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : AbstractStates
{
    PlayerFSM fsm;
    public PlayerIdleState(AbstractFSM fsm) { this.fsm = fsm as PlayerFSM; }
    public override void OnUpdate()
    {
        base.OnUpdate();
        if (Input.GetKeyDown(SettingManager.Instance.jump)) fsm.SwitchState(StateType.Jump);
    }
}
public class PlayerPendState : AbstractStates
{
    PlayerFSM fsm;
    public PlayerPendState(AbstractFSM fsm) { this.fsm = fsm as PlayerFSM; }
    public override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
        fsm.SwitchState(StateType.Idle);
    }
    public override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        fsm.SwitchState(StateType.Idle);
    }
}
public class PlayerJumpState : AbstractStates
{
    PlayerFSM fsm;
    float timer;
    public PlayerJumpState(AbstractFSM fsm) { this.fsm = fsm as PlayerFSM; }
    public override void OnEnter()
    {
        base.OnEnter();
        fsm.OnJump();
        timer = 0;
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        timer += Time.deltaTime;
        if (!Input.GetKeyUp(SettingManager.Instance.jump) && timer <= fsm.keepJumpTime) fsm.KeepJump();
        else fsm.SwitchState(StateType.Pend);
    }
}