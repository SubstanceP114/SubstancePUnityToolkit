using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerIdleState : AbstractStates
{
    private PlayerFSM fsm;
    public PlayerIdleState(AbstractFSM fsm) { this.fsm = fsm as PlayerFSM; }

    public override void OnEnter()
    {
        base.OnEnter();
        InputManager.Instance.playerInput.Control.Jump.started += Jump;
    }
    public override void OnExit()
    {
        base.OnExit();
        InputManager.Instance.playerInput.Control.Jump.started -= Jump;
    }

    private void Jump(InputAction.CallbackContext ctx) => fsm.SwitchState(typeof(PlayerJumpState));
}

public class PlayerPendState : AbstractStates
{
    private PlayerFSM fsm;
    public PlayerPendState(AbstractFSM fsm) { this.fsm = fsm as PlayerFSM; }

    private bool IsGrounded2D => Physics2D.OverlapBoxAll
                   ((Vector2)fsm.Col2D.bounds.center - Vector2.up * fsm.Col2D.bounds.extents.y,
                   new Vector2(fsm.Col2D.bounds.size.x, .1f), 0, LayerMask.GetMask("Ground")).Length > 0;
    private bool IsGrounded => Physics.OverlapBox
                   (fsm.Col.bounds.center - Vector3.up * fsm.Col.bounds.extents.y,
                   new Vector3(fsm.Col.bounds.size.x, .1f, fsm.Col.bounds.size.z),
                   Quaternion.identity, LayerMask.GetMask("Ground")).Length > 0;

    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
        switch (SettingManager.Instance.mode)
        {
            case Dimension.Horizontal2D:
                if (IsGrounded2D) fsm.SwitchState(typeof(PlayerIdleState));
                break;
            case Dimension.Normal3D:
                if (IsGrounded) fsm.SwitchState(typeof(PlayerIdleState));
                break;
            default:
                break;
        }
    }
}
public class PlayerJumpState : AbstractStates
{
    private float timer;

    private PlayerFSM fsm;
    public PlayerJumpState(AbstractFSM fsm) { this.fsm = fsm as PlayerFSM; }

    public override void OnEnter()
    {
        base.OnEnter();
        InputManager.Instance.playerInput.Control.Jump.started += Pend;
        
        fsm.OnJump();
        timer = 0;
    }
    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
        timer += Time.deltaTime;
        if (timer <= fsm.KeepJumpTime) fsm.KeepJump();
        else fsm.SwitchState(typeof(PlayerPendState));
    }
    public override void OnExit()
    {
        base.OnExit();
        InputManager.Instance.playerInput.Control.Jump.started -= Pend;
    }

    private void Pend(InputAction.CallbackContext ctx) => fsm.SwitchState(typeof(PlayerPendState));
}