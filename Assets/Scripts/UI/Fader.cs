using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// 持有 RawImage 设置透明度以实现淡入淡出
/// </summary>
public class Fader : MonoBehaviour
{
    private RawImage _image;
    //是否为淡入(黑-->无)
    public bool isFadeIn = true;
    public float speed = 1f;
    //标记是否在淡入淡出中
    private bool _isInFading = false;
    //结束阈值
    public float threshold = 0.08f;
    //是否结束
    public bool isEnd = false;
    //结束时执行的函数
    private event UnityAction _actions;

    private void Awake()
    {
        _image = GetComponent<RawImage>();
    }

    private void Start()
    {
        //设置淡入或淡出的初始状态
        if (isFadeIn) {
            _image.color = Color.black;
        }
        else {
            _image.color = Color.clear;
        }
        //开始
        _image.enabled = true;
        _isInFading = true;
    }

    private void Update()
    {
        //淡入淡出
        if (_isInFading) {
            if (isFadeIn) {
                FadeIn();
                //结束判定
                if (_image.color.a <= threshold) {
                    _image.color = Color.clear;
                    _image.enabled = false;
                    _isInFading = false;
                }
            }
            else {
                FadeOut();
                //结束判定
                if (_image.color.a >= 1 - threshold) {
                    _image.color = Color.black;
                    _isInFading = false;
                }
            }
        }
        else {
            _actions?.Invoke();
            isEnd = true;
        }
    }

    private void FadeIn()
    {
        _image.color = Color.Lerp(_image.color, Color.clear, speed * Time.deltaTime);
    }

    private void FadeOut()
    {
        _image.color = Color.Lerp(_image.color, Color.black, speed * Time.deltaTime);
    }

    //添加结束执行函数
    public void AddEndAction(UnityAction action)
    {
        _actions += action;
    }
}
