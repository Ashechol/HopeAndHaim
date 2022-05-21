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
    private bool _skip = false;
    //是否显示提示
    private bool _showHint = false;

    private void Start()
    {

    }

    private void Update()
    {
        //视频在播放
        if (video.isPlaying) {
            //没有显示提示 && 视频过 10s (只会执行一次)
            if (!_showHint && video.time >= 10) {
                _showHint = true;
                hint.StartFading();
                //接收跳过输入
                StartCoroutine(MiddleSceneAction());
            }
            //显示了提示 && 视频过 20s (只会执行一次)
            else if (_showHint && video.time >= 20) {
                hint.isFadeIn = false;
                hint.Reset();
                hint.StartFading();
            }

            if (Input.GetKeyDown(KeyCode.Space))
                _skip = true;
        }
    }

    private IEnumerator MiddleSceneAction()
    {
        //能播放不能跳过
        while (video.isPlaying && !_skip) {
            yield return null;
        }

        video.Stop();

        hint.transform.Find("Hint").gameObject.SetActive(false);

        LoadNextScene();
    }


    private void LoadNextScene()
    {
        Debug.Log("加载下一场景");
        SceneLoadManager.Instance.LoadLevel("Episode-2");
    }
}
