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
        //视频在播放
        if (video.isPlaying) {
            //没有显示提示 && 视频过 10s
            if (!_showHint && video.time >= 10) {
                _canSkip = true;
                _showHint = true;
                hint.StartFading();
                //接收跳过输入
                StartCoroutine(MiddleSceneAction());
            }
            //显示了提示 && 视频过 20s
            else if (_showHint && video.time >= 20) {
                hint.isFadeIn = false;
                hint.Reset();
                hint.StartFading();
            }
        }
    }

    private IEnumerator MiddleSceneAction()
    {
        //能播放能跳过
        while (_canSkip && video.isPlaying) {
            if (Input.GetKeyDown(KeyCode.Space))
                SkipAction();
            yield return null;
        }

        LoadNextScene();
    }

    //跳过时的行为
    private void SkipAction()
    {
        _canSkip = false;
        video.Stop();
    }

    private void LoadNextScene()
    {
        //TODO: 加载场景
        Debug.Log("加载下一场景");
    }
}
