using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 离开禁闭室就播放地下环境音
/// 因为第一幕只有禁闭室不播放
/// </summary>
public class GuardhouseArea : BgmArea
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        StopBgm();
    }

    protected override void OnTriggerExit2D(Collider2D collision)
    {
        StartBgm();
    }
}
