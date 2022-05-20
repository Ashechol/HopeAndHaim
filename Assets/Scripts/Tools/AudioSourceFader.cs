using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 控制 AudioSource 淡入淡出
/// </summary>
public class AudioSourceFader : MonoBehaviour
{
    //可自行拖拽，也可以自动获取
    public AudioSource source;
    //淡入淡出速度
    public float fSpeed = 1f;
    //是否为淡入
    public bool isFadeIn = true;
    //是否结束
    public bool isEnd = false;
    //结束阈值
    public float threshold = 0.08f;
    //结束时执行函数
    private event UnityAction _actions;

    //是否正在淡入淡出中
    private bool _isFading = false;

    private void Start()
    {
        if (source == null) {
            source = GetComponent<AudioSource>();
        }
        source.enabled = true;

        if (isFadeIn) {
            source.volume = 0;
        }
        else {
            source.volume = 1;
        }

        _isFading = true;
        source.Play();
    }

    private void Update()
    {
        if (_isFading) {
            if (isFadeIn) {
                FadeIn();
                if (source.volume >= 1 - threshold) {
                    source.volume = 1;
                    _isFading = false;
                }
            }
            else {
                FadeOut();
                if (source.volume <= threshold) {
                    source.volume = 0;
                    source.enabled = false;
                    _isFading = false;
                }
            }
        }
        else {

        }
    }

    private void FadeIn()
    {
        source.volume = Mathf.Lerp(source.volume, 1, fSpeed * Time.deltaTime);
    }

    private void FadeOut()
    {
        source.volume = Mathf.Lerp(source.volume, 0, fSpeed * Time.deltaTime);
    }

    public void AddEndAction(UnityAction action)
    {
        _actions += action;
    }
}
