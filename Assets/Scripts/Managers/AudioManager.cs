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
    public AudioClip openDoorClip;

    public List<AudioClip> hopeStaticClips = new List<AudioClip>();
    private int _hopeClipPointer = 0;

    public List<AudioClip> collideClips = new List<AudioClip>();

    private void Start()
    {
        _hope = GameManager.Instance.hope;
    }

    //获得随机 Hope 静态语音
    public AudioClip GetHopeStaticClip()
    {
        //不能采取随机播放，由于语音太少，随机同一句的频率太高，改为循环
        if (_hopeClipPointer >= hopeStaticClips.Count) {
            _hopeClipPointer = 0;
        }
        return hopeStaticClips[_hopeClipPointer++];
    }

    //更换 Hope 静置语音
    public void ChangeHopeStaticClip(List<AudioClip> clips)
    {
        hopeStaticClips = clips;
        _hopeClipPointer = 0;
    }

    //获得随机碰撞语音
    public AudioClip GetCollideClip()
    {
        return collideClips[Random.Range(0, collideClips.Count)];
    }

    public void SetFootstep(AudioClip clip)
    {
        if (_hope == null) {
            _hope = GameManager.Instance.hope;
        }

        if (_hope != null) {
            _hope.ChangeFootstep(clip);
        }
    }

    public void SetIntercomBgm()
    {
        if (_hope == null) {
            _hope = GameManager.Instance.hope;
        }

        if (_hope != null) {
            Debug.Log("播放对讲机音效");
            _hope.ChangeBgm(intercomClip, false);
        }
    }
}
