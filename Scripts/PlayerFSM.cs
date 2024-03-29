using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFSM : AbstractFSM
{
    [SerializeField] private bool isDefaultPlayer;
    [SerializeField, Header("Move")] private float moveSpeed;
    [SerializeField, Header("Jump")] private bool disableJump;
    [SerializeField] private float impulse;
    [SerializeField] private float force;
    public float keepJumpTime;

    private Rigidbody rb;
    private Rigidbody2D rb2d;
    private int xInput;
    private int yInput;
    private int zInput;
    private void Awake()
    {
        if (isDefaultPlayer) Player.Current = this;
        switch (SettingManager.Instance.mode)
        {
            case Dimension.Vertical2D:
            case Dimension.Horizontal2D:
                rb2d = GetComponent<Rigidbody2D>();
                break;
            case Dimension.Normal3D:
                rb = GetComponent<Rigidbody>();
                break;
            default:
                break;
        }
        AddState(StateType.Idle, new PlayerIdleState(this));
        AddState(StateType.Pend, new PlayerPendState(this));
        if (!disableJump) AddState(StateType.Jump, new PlayerJumpState(this));
        SwitchState(StateType.Pend);
    }
    protected override void Update()
    {
        base.Update();
        InputAxis();
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        MoveByAxis();
    }
    private void InputAxis()
    {
        xInput = AdjustAxis(SettingManager.Instance.left, SettingManager.Instance.right, xInput);
        switch (SettingManager.Instance.mode)
        {
            case Dimension.Vertical2D:
                yInput = AdjustAxis(SettingManager.Instance.forward, SettingManager.Instance.backward, yInput);
                break;
            case Dimension.Normal3D:
                zInput = AdjustAxis(SettingManager.Instance.forward, SettingManager.Instance.backward, zInput);
                break;
            default:
                break;
        }
    }
    private int AdjustAxis(KeyCode positive, KeyCode negative, int axis)
    {
        if (Input.GetKeyDown(positive) && axis != 1) axis = 1;
        if (Input.GetKeyDown(negative) && axis != -1) axis = -1;
        if (axis == 1 && Input.GetKeyUp(positive) && !Input.GetKey(negative)) axis = 0;
        if (axis == 1 && Input.GetKeyUp(positive) && Input.GetKey(negative)) axis = -1;
        if (axis == -1 && Input.GetKeyUp(negative) && !Input.GetKey(positive)) axis = 0;
        if (axis == -1 && Input.GetKeyUp(negative) && Input.GetKey(positive)) axis = 1;
        return axis;
    }
    private void MoveByAxis() =>
        rb.MovePosition(transform.position +
            Vector3.left * xInput * Time.fixedDeltaTime * moveSpeed +
            Vector3.up * yInput * Time.fixedDeltaTime * moveSpeed +
            Vector3.forward * zInput * Time.fixedDeltaTime * moveSpeed);
    public void OnJump() => MatchJump(impulse, 1);
    public void KeepJump() => MatchJump(force, 0);
    private void MatchJump(float value, int mode)
    {
        switch (SettingManager.Instance.mode)
        {
            case Dimension.Vertical2D:
            case Dimension.Horizontal2D:
                rb2d.AddForce(Vector2.up * value, (ForceMode2D)mode);
                break;
            case Dimension.Normal3D:
                rb.AddForce(Vector3.up * value, (ForceMode)mode);
                break;
            default:
                break;
        }
    }
}