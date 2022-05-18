using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 发声道具，使用射线检测方式
/// </summary>
public class VocalRayProp : RayDetectProp
{
    //播放模式
    public bool isLoop = false;
    //离开时是否结束
    public bool isStopOnExit = true;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null) {
            _audioSource = gameObject.AddComponent<AudioSource>();
            //AudioManager.Instance.SetAudioSourceField(_audioSource);
        }
    }

    public override void DetectEnter()
    {
        base.DetectEnter();
        if (!_audioSource.isPlaying) {
            _audioSource.loop = isLoop;
            _audioSource.Play();
        }
    }

    public override void DetectExit()
    {
        base.DetectExit();
        if (isStopOnExit) {
            _audioSource.Stop();
        }
        else {
            _audioSource.loop = false;
        }
    }
}
