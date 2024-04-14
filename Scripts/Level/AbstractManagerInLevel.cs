using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 关卡内的各种Manager继承此类，将初始化顺序和内容分别写进<see cref="Order"/>和<see cref="Init"/>中
/// <br/>不要把初始化的内容写在Awake里面，以免打乱初始化顺序
/// <br/>示例：
/// <code>
/// public class SampleManager : AbstractManagerInLevel
/// {
///     private static SampleManager instance;
///     public static SampleManager Instance
///     {
///         get
///         {
///             if (instance) return instance;
///             else throw new Exception("找不到SampleManager单例，请检查场景或初始化顺序");
///         }
///     }
///     public override int Order => 1;
///     public override void Init()
///     {
///         instance = this;
///     }
/// }
/// </code>
/// </summary>
public abstract class AbstractManagerInLevel : MonoBehaviour
{
    /// <summary>
    /// 指定该Manager在<see cref="LevelManager.InitManagers"/>中的初始化顺序，越大越靠后
    /// </summary>
    public abstract int Order { get; }
    /// <summary>
    /// Manager初始化方法，由<see cref="LevelManager.InitManagers"/>调用
    /// </summary>
    public abstract void Init();
}
