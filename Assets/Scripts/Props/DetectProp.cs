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

    //是否已经检测到，用于执行 enter 和 exit 函数
    private bool _isDetecting = false;

    private void Update()
    {
        if (Detect())
        {
            if (!_isDetecting)
            {
                _isDetecting = true;
                DetectEnter();
            }
            DetectAction();
        }
        else if (_isDetecting)
        {
            _isDetecting = false;
            DetectExit();
        }
    }

    public abstract bool Detect();

    public virtual void DetectEnter()
    {
        Debug.Log("DetectProp: 进入检测");
    }

    public virtual void DetectAction()
    {
        Debug.Log("DetectProp: 检测中");
    }

    public virtual void DetectExit()
    {
        Debug.Log("DetectProp: 退出检测");
    }
}
