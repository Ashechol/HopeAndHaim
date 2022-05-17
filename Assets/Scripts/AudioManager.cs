using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 管理音频文件
/// </summary>
public class AudioManager : Singleton<AudioManager>
{
    public Hope hope;

    //音频列表
    public List<AudioClip> staticClips = new List<AudioClip>();
    public List<AudioClip> collideClips = new List<AudioClip>();

    public AudioClip GetStaticClip()
    {
        return null;
    }

    public AudioClip GetCollideClip()
    {
        return collideClips[Random.Range(0, collideClips.Count)];
    }

    #region 设置角色声音
    public void SetFootstep(AudioClip clip)
    {
        if (hope != null) {
            hope.footSource.clip = clip;
            if (hope.IsMoving) {
                hope.footSource.Play();
            }
        }
    }
    #endregion

    #region 设置背景音乐，暂不使用
    public void SetBgm(string clipName, bool isLoop = false)
    {
        string path = "Audio/SE/" + clipName;
        hope.bgmSource.clip = Resources.Load(path) as AudioClip;
        hope.bgmSource.loop = isLoop;
        hope.bgmSource.Play();
    }

    public void SetDropBgm()
    {
        SetBgm("水滴", true);
    }

    public void SetIntercomBgm()
    {
        SetBgm("对讲机呲啦呲啦音效");
    }
    #endregion
}
