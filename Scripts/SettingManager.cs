using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingManager : MonoBehaviour
{
    public static SettingManager Instance;

    public Dimension mode;
    [Header("Key")]
    public KeyCode left = KeyCode.A;
    public KeyCode right = KeyCode.D;
    public KeyCode forward = KeyCode.W;
    public KeyCode backward = KeyCode.S;
    public KeyCode attack = KeyCode.J;
    public KeyCode jump = KeyCode.K;
    public KeyCode sprint = KeyCode.L;
    public KeyCode interact = KeyCode.F;
    void Awake()
    {
        DontDestroyOnLoad(this);
        Instance = this;
    }
}
public enum Dimension { Horizontal2D, Vertical2D, Normal3D }