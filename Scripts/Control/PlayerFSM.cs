using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFSM : AbstractFSM
{
    [SerializeField] private bool isDefaultPlayer;
    [Header("Move")]
    [SerializeField] private float moveSpeed;
    [Header("Jump")]
    [SerializeField] private bool disableJump;
    [SerializeField] private float impulse;
    [SerializeField] private float force;
    [SerializeField] private float keepJumpTime;
    public float KeepJumpTime => keepJumpTime;

    private Rigidbody rb;
    private Rigidbody2D rb2d;

    public Collider Col { get; private set; }
    public Collider2D Col2D { get; private set; }

    private void Awake()
    {
        if (isDefaultPlayer) Player.Current = this;
        switch (SettingManager.Instance.mode)
        {
            case Dimension.Vertical2D:
            case Dimension.Horizontal2D:
                rb2d = GetComponent<Rigidbody2D>();
                Col2D = GetComponent<Collider2D>();
                break;
            case Dimension.Normal3D:
                rb = GetComponent<Rigidbody>();
                Col = GetComponent<Collider>();
                break;
            default:
                break;
        }
        AddState(typeof(PlayerIdleState), new PlayerIdleState(this));
        AddState(typeof(PlayerPendState), new PlayerPendState(this));
        if (!disableJump) AddState(typeof(PlayerJumpState), new PlayerJumpState(this));
        SwitchState(typeof(PlayerPendState));
    }

    public void MoveByAxis(float x, float y, float z)
    {
        switch (SettingManager.Instance.mode)
        {
            case Dimension.Vertical2D:
            case Dimension.Horizontal2D:
                rb2d.MovePosition(transform.position +
                    Vector3.left * x * Time.fixedDeltaTime * moveSpeed +
                    Vector3.up * y * Time.fixedDeltaTime * moveSpeed +
                    Vector3.forward * z * Time.fixedDeltaTime * moveSpeed);
                break;
            case Dimension.Normal3D:
                rb.MovePosition(transform.position +
                    Vector3.left * x * Time.fixedDeltaTime * moveSpeed +
                    Vector3.up * y * Time.fixedDeltaTime * moveSpeed +
                    Vector3.forward * z * Time.fixedDeltaTime * moveSpeed);
                break;
            default:
                break;
        }
    }

    public void OnJump() => MatchJump(impulse, 1);
    public void KeepJump() => MatchJump(force, 0);
    private void MatchJump(float value, int mode)
    {
        switch (SettingManager.Instance.mode)
        {
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