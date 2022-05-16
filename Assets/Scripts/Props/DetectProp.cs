using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 道具基类
/// 检测周围碰撞
/// 碰撞时执行函数
/// </summary>
public abstract class DetectProp : MonoBehaviour
{
    //检测层级
    public LayerMask detectMask;

    private void Update()
    {
        if (Detect())
        {
            DetectAction();
        }
    }

    public abstract bool Detect();

    public abstract void DetectAction();
}
