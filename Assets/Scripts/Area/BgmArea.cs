using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 设置区域内播放的 BGM
/// </summary>
public class BgmArea : MonoBehaviour
{
    public AudioSource source;
    public AudioClip clip;
    public float volume = 0.5f;
    [Header("离开区域时是否停止")]
    public bool isStop = true;

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        StartBgm();
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (isStop)
        {
            StopBgm();
        }
    }

    protected void StartBgm()
    {
        source.clip = clip;
        source.volume = volume;
        source.loop = true;
        source.Play();
    }

    protected void StopBgm()
    {
        source.Stop();
    }
}
