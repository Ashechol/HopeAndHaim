using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

/// <summary>
/// 第一幕第二幕之间间幕流程
/// </summary>
public class Middle1t2Process : MonoBehaviour
{
    public VideoPlayer video;
    public CanvasGroupFader hint;

    //是否接收跳过
    private bool _canSkip = false;
    //是否显示提示
    private bool _showHint = false;

    private void Update()
    {
        //能跳过
        if (_canSkip)
        {
            //接收跳过按键
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SkipAction();
            }
        }
        //不能跳过，则判断何时能够跳过并显示提示——当视频播放 10s 后
        else if (video.isPlaying && video.time >= 10)
        {
            _canSkip = true;
            _showHint = true;
            hint.StartFading();
        }

        if (_showHint && video.isPlaying)
        {
            if (video.time <= 20)
            {
                hint.StartFading();
            }
            else
            {
                hint.isFadeIn = false;
                hint.Reset();
                hint.StartFading();
            }
        }

        if (_canSkip && !video.isPlaying)
        {
            //判断提示是否不再显示
            if (_showHint && video.isPlaying && video.time >= 20)
            {
                _showHint = false;
                hint.isFadeIn = false;
                hint.Reset();
                hint.StartFading();
            }

            //视频不再播放时载入下一场景
            if (!video.isPlaying)
            {
                LoadNextScene();
            }
        }
    }

    //跳过时的行为
    private void SkipAction()
    {
        //TODO: 间幕动画淡出？
        video.Stop();
    }

    private void LoadNextScene()
    {
        //TODO: 加载场景
    }
}
