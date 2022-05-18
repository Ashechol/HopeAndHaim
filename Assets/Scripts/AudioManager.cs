using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 管理音频播放
/// </summary>
public class AudioManager : Singleton<AudioManager>
{
    //Hope 挂载的 AudioSources
    private Hope _hope;

    public AudioClip intercomClip;

    public List<AudioClip> staticClips = new List<AudioClip>();
    public List<AudioClip> collideClips = new List<AudioClip>();

    private void Start()
    {
        _hope = GameManager.Instance.hope;
    }

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

    public void SetFootstep(AudioClip clip)
    {
        if (_hope != null) {
            _hope.ChangeFootstep(clip);
        }
    }

    public void SetIntercomBgm()
    {
        if (_hope != null) {
            _hope.ChangeBgm(intercomClip);
        }
    }
}
