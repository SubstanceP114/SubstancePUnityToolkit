using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingManager : AbstractManagerInGame
{
    private static SettingManager instance;
    public static SettingManager Instance
    {
        get
        {
            if (instance) return instance;
            else throw new System.Exception("�Ҳ���SettingManager���������鳡�����ʼ��˳��");
        }
    }
    public override int Order => 1;
    public override void Init()
    {
        instance = this;
    }

    public Dimension mode;
}
public enum Dimension { Horizontal2D, Vertical2D, Normal3D }