using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������Ƶ�ļ�
/// </summary>
public class AudioManager
{
    private static AudioManager instance = new AudioManager();
    public static AudioManager Instance => instance;

    //��Ƶ�б�
    private List<AudioClip> staticClips = new List<AudioClip>();
    private List<AudioClip> collideClips = new List<AudioClip>();

    private AudioManager()
    {
        //��ȡ��Ƶ
    }

    public AudioClip GetStaticClip()
    {
        return null;
    }

    public AudioClip GetCollideClip()
    {
        return null;
    }
}
