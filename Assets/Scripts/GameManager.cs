using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class GameManager : Singleton<GameManager>
{
    #region Timeline 字段
    public enum GameMode
    {
        Normal,         //操控角色
        GamePlay,       //播放动画
        DialogueMoment  //对话暂停
    }

    public GameMode gameMode;

    public PlayableDirector currentPlayableDirector;
    //调试加速
    private bool isSpeedUp = false;
    #endregion

    // Haim one of the main actors
    public Haim haim;

    private void Start()
    {
        if (currentPlayableDirector.playOnAwake) {
            gameMode = GameMode.GamePlay;
        }
        else {
            gameMode = GameMode.Normal;
        }
    }

    private void Update()
    {
        //对话暂停时，需要按键恢复
        if (gameMode == GameMode.DialogueMoment) {
            if (Input.GetKeyDown(KeyCode.E)) {
                ResumeTimeline();
            }
        }
        //加速播放，调试用
        else if (gameMode == GameMode.GamePlay) {
            if (Input.GetKeyDown(KeyCode.Q)) {
                SpeedUp();
            }
        }
    }

    public void RegisterHaim(Haim haim)
    {
        this.haim = haim;
    }

    #region Timeline 函数
    public void RegisterDirector(PlayableDirector director)
    {
        currentPlayableDirector = director;
    }

    public void PauseTimeline()
    {
        //获得 director 以控制播放，如果不暂停则不需要获得组件
        //currentPlayableDirector = director;
        //设置为对话暂停模式
        gameMode = GameMode.DialogueMoment;
        //设置播放速度为 0, 即暂停
        currentPlayableDirector.playableGraph.GetRootPlayable(0).SetSpeed(0);
    }

    public void ResumeTimeline()
    {
        gameMode = GameMode.GamePlay;
        //恢复播放
        currentPlayableDirector.playableGraph.GetRootPlayable(0).SetSpeed(1);
    }

    public void ResumeGame()
    {
        Debug.Log("恢复游戏");
        gameMode = GameMode.Normal;
        currentPlayableDirector.playOnAwake = false;
    }

    public void SpeedUp()
    {
        isSpeedUp = !isSpeedUp;
        gameMode = GameMode.GamePlay;
        if (isSpeedUp) {
            currentPlayableDirector.playableGraph.GetRootPlayable(0).SetSpeed(5);
        }
        else {
            currentPlayableDirector.playableGraph.GetRootPlayable(0).SetSpeed(1);
        }
    }

    public void SignalTest()
    {
        Debug.Log("Signal 测试");
    }
    #endregion
}
