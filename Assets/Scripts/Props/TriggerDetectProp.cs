using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 触发器检测方式的道具
/// </summary>
public class TriggerDetectProp : DetectProp
{
    [Header("触发器检测")]
    public new Collider2D collider;
    //检测过滤器
    protected ContactFilter2D _filter2D;

    //检测到的碰撞器列表
    protected List<Collider2D> _colliderResults;

    private void Start()
    {
        _filter2D = new ContactFilter2D();
        _colliderResults = new List<Collider2D>();
    }

    public override bool Detect()
    {
        if (collider != null)
        {
            //设置过滤
            _filter2D.SetLayerMask(detectMask);
            //检测并返回个数
            return collider.OverlapCollider(_filter2D, _colliderResults) != 0;
        }

        return false;
    }

    public override void DetectAction()
    {
        Debug.Log("执行检测函数");
    }
}
