using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

/// <summary>
/// 管理 Timeline 播放
/// </summary>
public class TimelineManager : Singleton<TimelineManager>
{
    public PlayableDirector currentDirector;
    private Hope _hope;

    //调试加速
    private bool isSpeedUp = false;

    private void Start()
    {
        _hope = GameManager.Instance.hope;
    }

    #region Timeline 通用函数
    //暂停 TL
    public void PauseTimeline()
    {
        //设置为对话暂停模式
        GameManager.Instance.gameMode = GameManager.GameMode.Dialog;
        //设置播放速度为 0, 即暂停
        currentDirector.playableGraph.GetRootPlayable(0).SetSpeed(0);
    }

    //恢复 TL
    public void ResumeTimeline()
    {
        GameManager.Instance.gameMode = GameManager.GameMode.Timeline;
        //恢复播放
        currentDirector.playableGraph.GetRootPlayable(0).SetSpeed(1);
        //关闭对话框
        UIManager.Instance.CleanDialogue();
    }

    //恢复游戏
    public void ResumeGame()
    {
        //Debug.Log("恢复游戏");
        GameManager.Instance.gameMode = GameManager.GameMode.Gameplay;
        //currentDirector.playOnAwake = false;
    }

    //调试用，加速播放 Timeline
    public void SpeedUp()
    {
        isSpeedUp = !isSpeedUp;
        if (isSpeedUp)
        {
            currentDirector.playableGraph.GetRootPlayable(0).SetSpeed(5);
        }
        else
        {
            currentDirector.playableGraph.GetRootPlayable(0).SetSpeed(1);
        }
    }

    //解决 Timeline bug, 手动置剧情语音 Source 停止播放
    public void StopAudioSource()
    {
        if (_hope != null)
        {
            _hope.HearSource.Stop();
        }
    }
    #endregion
}
