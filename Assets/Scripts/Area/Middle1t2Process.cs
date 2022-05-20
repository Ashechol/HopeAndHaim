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
        if (_canSkip) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                //跳过处理
                video.Stop();
            }
        }
        else if (video.isPlaying && video.time >= 10) {
            _canSkip = true;
            _showHint = true;
        }

        if (_showHint && video.isPlaying) {
            if (video.time <= 20) {
                hint.StartFading();
            }
            else {
                hint.isFadeIn = false;
                hint.Reset();
                hint.StartFading();
            }
        }

        if (_canSkip && !video.isPlaying) {
            LoadNextScene();
        }
    }

    private void LoadNextScene()
    {
        Debug.Log("加载下一场景");
        //TODO: 加载场景
    }
}
