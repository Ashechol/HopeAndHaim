using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// 淡入淡出基类
/// </summary>
public abstract class Fader<T> : MonoBehaviour
{
    //是否开始
    public bool startOnWake = false;

    //组件，可以拖拽也能自行获取
    public T target;
    //淡入淡出速度
    public float fSpeed = 1f;
    //结束阈值
    public float threshold = 0.05f;
    //是否为淡入
    public bool isFadeIn = true;
    //是否结束
    public bool isEnd = false;
    //结束时执行函数
    public event UnityAction endActions;

    //是否为第一次执行
    private bool _isFirstTime = true;
    //是否正在淡入淡出中
    private bool _isFading = false;
    public bool isFading => _isFading;

    //重置状态
    public void Reset()
    {
        if (isFadeIn) {
            FadeInInit();
        }
        else {
            FadeOutInit();
        }
        isEnd = false;
        _isFirstTime = false;
    }

    //淡入初始化
    protected abstract void FadeInInit();

    //淡出初始化
    protected abstract void FadeOutInit();

    //开始淡入
    protected virtual void FadeInStart()
    {
        //Debug.Log("开始淡入");
    }

    //开始淡出
    protected virtual void FadeOutStart()
    {
        //Debug.Log("开始淡出");
    }

    protected virtual void Start()
    {
        if (target == null) {
            target = GetComponent<T>();
        }

        //target 初始化
        if (isFadeIn) {
            FadeInInit();
        }
        else {
            FadeOutInit();
        }

        if (startOnWake) {
            StartFading();
        }
    }

    //淡入逻辑
    protected abstract void FadeIn();

    //淡出逻辑
    protected abstract void FadeOut();

    //淡入结束判断
    protected abstract bool IsFadeInEnd();

    //淡出结束判断
    protected abstract bool IsFadeOutEnd();

    //淡入结束处理
    protected abstract void FadeInEnd();

    //淡出结束处理
    protected abstract void FadeOutEnd();

    //淡入淡出结束
    protected virtual void AllEnd()
    {
        //Debug.Log("淡入淡出结束");
        endActions?.Invoke();
    }

    public void StartFading()
    {
        _isFading = true;
    }

    protected virtual void Update()
    {
        //开始淡入淡出
        if (_isFirstTime && _isFading) {
            if (isFadeIn) {
                FadeInStart();
            }
            else {
                FadeOutStart();
            }
            _isFirstTime = false;
        }

        //淡入淡出
        if (_isFading) {
            //淡入
            if (isFadeIn) {
                FadeIn();
                if (IsFadeInEnd()) {
                    FadeInEnd();
                    _isFading = false;

                    //结束
                    isEnd = true;
                    AllEnd();
                }
            }
            //淡出
            else {
                FadeOut();
                if (IsFadeOutEnd()) {
                    FadeOutEnd();
                    _isFading = false;

                    //结束
                    isEnd = true;
                    AllEnd();
                }
            }
        }
    }
}
