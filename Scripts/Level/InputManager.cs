using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.Windows;

public class InputManager : AbstractManagerInLevel
{
    private static InputManager instance;
    public static InputManager Instance
    {
        get
        {
            if (instance) return instance;
            else throw new System.Exception("找不到InputManager单例，请检查场景或初始化顺序");
        }
    }
    public override int Order => 1;
    public override void Init()
    {
        instance = this;
        playerInput = new PlayerControl();
        playerInput.Enable();
    }


    public PlayerControl playerInput;
    private void OnDisable()
    {
        playerInput.Disable();
    }

    private float xInput;
    private float yInput;
    private float zInput;
    private void FixedUpdate()
    {
        InputAxis();
        Player.Current.MoveByAxis(xInput, yInput, zInput);
    }
    private void InputAxis()
    {
        xInput = playerInput.Control.Horizontal.ReadValue<float>();
        switch (SettingManager.Instance.mode)
        {
            case Dimension.Vertical2D:
                yInput = playerInput.Control.Vertical.ReadValue<float>();
                break;
            case Dimension.Normal3D:
                zInput = playerInput.Control.Vertical.ReadValue<float>();
                break;
            default:
                break;
        }
    }
}
