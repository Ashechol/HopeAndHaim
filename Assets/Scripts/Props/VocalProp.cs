using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 发声道具
/// </summary>
public class VocalProp : MonoBehaviour
{

    //播放模式
    public bool isLoop = false;
    //检测层级
    public LayerMask detectMask;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            _audioSource = gameObject.AddComponent<AudioSource>();
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
        _audioSource.Stop();
    }
}
