using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 管理音频文件
/// </summary>
public class AudioManager : Singleton<AudioManager>
{
    public AudioSource bgmSource;
    public AudioSource speakSource;

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

    //设置 AudioSource 声音效果
    public void SetAudioSourceField(AudioSource audioSource)
    {
        //设置 3D 立体效果
        audioSource.spatialBlend = 1;
        audioSource.rolloffMode = AudioRolloffMode.Logarithmic;
        audioSource.maxDistance = 8;
    }

    #region 设置背景音乐
    public void SetBgm(string clipName, bool isLoop = false)
    {
        string path = "Audio/SE/" + clipName;
        bgmSource.clip = Resources.Load(path) as AudioClip;
        bgmSource.loop = isLoop;
        bgmSource.Play();
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
