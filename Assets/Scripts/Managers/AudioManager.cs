using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 管理音频播放
/// </summary>
public class AudioManager : Singleton<AudioManager>
{
    #region 音频组件
    //Hope 挂载的 AudioSources
    private Hope hope;
    #endregion

    //音频列表
    public List<AudioClip> staticClips = new List<AudioClip>();
    public List<AudioClip> collideClips = new List<AudioClip>();

    protected override void Awake()
    {
        base.Awake();
        hope = GameObject.Find("Hope").GetComponent<Hope>();
        if (hope == null)
        {
            Debug.LogWarning("AudioManager: 在当前场景没有获取到 Hope 组件");
        }
    }

    #region 提供音频函数
    //获得随机静态语音
    public AudioClip GetStaticClip()
    {
        return null;
    }

    //获得随机碰撞语音
    public AudioClip GetCollideClip()
    {
        return collideClips[Random.Range(0, collideClips.Count)];
    }
    #endregion

    #region 设置角色声音
    public void SetFootstep(AudioClip clip)
    {
        if (hope != null)
        {
            hope.ChangeFootstep(clip);
        }
    }
    #endregion

    #region 设置背景音乐
    public AudioClip GetSE(string clipName)
    {
        string path = "Audio/SE/" + clipName;
        return Resources.Load(path) as AudioClip;
    }

    public void SetIntercomBgm()
    {
        if (hope != null)
        {
            hope.ChangeBgm(GetSE("对讲机呲啦呲啦音效"));
        }
    }
    #endregion
}
