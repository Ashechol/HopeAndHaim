using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 管理音频文件
/// </summary>
public class AudioManager
{
    private static AudioManager instance = new AudioManager();
    public static AudioManager Instance => instance;

    //音频列表
    private List<AudioClip> staticClips = new List<AudioClip>();
    private List<AudioClip> collideClips = new List<AudioClip>();

    private AudioManager()
    {
        //获取音频
    }

    public AudioClip GetStaticClip()
    {
        return null;
    }

    public AudioClip GetCollideClip()
    {
        return null;
    }

    //设置 AudioSource 声音效果
    public void SetAudioSourceField(AudioSource audioSource)
    {
        //设置 3D 立体效果
        audioSource.spatialBlend = 1;
        audioSource.rolloffMode = AudioRolloffMode.Logarithmic;
        audioSource.maxDistance = 8;
    }
}
