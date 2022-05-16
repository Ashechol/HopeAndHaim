using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 发声道具，使用物体自身的碰撞体进行检测
/// </summary>
public class VocalProp : MonoBehaviour
{

    //播放模式
    public bool isLoop = false;
    //离开时是否结束
    public bool isStopOnExit = true;
    //检测层级
    public LayerMask detectMask;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            _audioSource = gameObject.AddComponent<AudioSource>();
            AudioManager.Instance.SetAudioSourceField(_audioSource);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("进入触发器");
        if (((1 << collision.gameObject.layer) & detectMask) != 0)
        {
            Debug.Log("触发层级");
            if (!_audioSource.isPlaying)
            {
                _audioSource.loop = isLoop;
                _audioSource.Play();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("离开触发器");
        if (isStopOnExit)
        {
            _audioSource.Stop();
        }
        else
        {
            _audioSource.loop = false;
        }
    }
}
