using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

/// <summary>
/// 管理 Timeline 播放
/// </summary>
public class TimelineManager : Singleton<TimelineManager>
{
    #region 组件
    public PlayableDirector currentDirector;
    private Hope hope;
    #endregion

    #region 状态参数
    //调试加速
    private bool isSpeedUp = false;
    #endregion

    protected override void Awake()
    {
        base.Awake();
        hope = GameObject.Find("Hope").GetComponent<Hope>();
        if (hope == null) {
            Debug.LogWarning("AudioManager: 在当前场景没有获取到 Hope 组件");
        }
    }

    #region Timeline 剧情函数
    //FirstScene_16 语音后剧情
    public void FirstScenePlot16()
    {

    }
    #endregion

    #region Timeline 通用函数
    //暂停 TL
    public void PauseTimeline()
    {
        //设置为对话暂停模式
        GameManager.Instance.gameMode = GameManager.GameMode.DialogueMoment;
        //设置播放速度为 0, 即暂停
        currentDirector.playableGraph.GetRootPlayable(0).SetSpeed(0);
    }

    //恢复 TL
    public void ResumeTimeline()
    {
        GameManager.Instance.gameMode = GameManager.GameMode.GamePlay;
        //恢复播放
        currentDirector.playableGraph.GetRootPlayable(0).SetSpeed(1);
        //关闭对话框
        UIManager.Instance.CleanDialogue();
    }

    //恢复游戏
    public void ResumeGame()
    {
        Debug.Log("恢复游戏");
        GameManager.Instance.gameMode = GameManager.GameMode.Normal;
        currentDirector.playOnAwake = false;
    }

    //调试用，加速播放 Timeline
    public void SpeedUp()
    {
        isSpeedUp = !isSpeedUp;
        if (isSpeedUp) {
            currentDirector.playableGraph.GetRootPlayable(0).SetSpeed(5);
        }
        else {
            currentDirector.playableGraph.GetRootPlayable(0).SetSpeed(1);
        }
    }
    #endregion
}
