using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 第一幕剧情节点 16
/// </summary>
public class Plot16 : MonoBehaviour
{
    public Transform startPos;
    public Transform endPos;

    public float duration;

    private Hope _hope;

    public void SetStartPos()
    {
        _hope = GameManager.Instance.hope;
        _hope.transform.position = startPos.position;
        _hope.SetDirection(Direction.Up);
    }

    public void Plot()
    {
        //开启脚步声
        _hope.FootSource.Play();
        //设置剧情移动
        _hope.MoveToTarget(endPos.position, duration);
    }
}
