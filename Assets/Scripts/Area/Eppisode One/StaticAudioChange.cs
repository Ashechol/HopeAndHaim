using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 切换静置语音
/// </summary>
public class StaticAudioChange : MonoBehaviour
{
    public List<AudioClip> clipList;

    //是否在进入触发器时切换
    public bool enterChange = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (enterChange) {
            AudioManager.Instance.ChangeHopeStaticClip(clipList);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!enterChange) {
            AudioManager.Instance.ChangeHopeStaticClip(clipList);
        }
    }
}
