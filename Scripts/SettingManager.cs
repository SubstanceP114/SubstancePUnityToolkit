using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingManager : MonoBehaviour
{
    public static SettingManager Instance;
    public GlobalSettings globalSettings;
    void Awake()
    {
        DontDestroyOnLoad(this);
        Instance = this;
    }
}
