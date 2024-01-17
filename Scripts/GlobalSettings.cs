using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="NewGlobalSettings",menuName ="Toolkits/Create New Global Settings")]
public class GlobalSettings : ScriptableObject
{
    public Dimension mode;
    public KeyCode left = KeyCode.A;
    public KeyCode right = KeyCode.D;
    public KeyCode forward = KeyCode.W;
    public KeyCode backward = KeyCode.S;
    public KeyCode attack = KeyCode.J;
    public KeyCode jump = KeyCode.K;
    public KeyCode sprint = KeyCode.L;
    public KeyCode interact = KeyCode.F;
}
public enum Dimension { _2D,_3D }