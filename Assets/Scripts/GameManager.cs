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

    public Hope hope;

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

    #region GameMode 函数
    /// <summary>
    /// 判断当前的游戏模式是否接收用户输入
    /// </summary>
    public bool CanInput()
    {
        return gameMode == GameMode.Normal;
    }
    #endregion

    #region Timeline 函数
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
        //关闭对话框
        UIManager.Instance.CleanDialogue();
    }

    public void ResumeGame()
    {
        Debug.Log("恢复游戏");
        gameMode = GameMode.Normal;
        currentPlayableDirector.playOnAwake = false;
    }

    //调试用，加速播放 Timeline
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

    //16 语音后剧情
    public void Plot16()
    {
        if (hope == null) return;
        //获得起始位置
        Vector3 startPos = GameObject.Find("Plot16Pos").transform.position;
        //设置剧情移动
        //hope.transform.position = startPos;
        //hope.direction = Direction.Up;
        //hope.transform.rotation = Quaternion.Euler(Vector3.zero);
        //hope.IsMoving = true;
        //hope.IsForward = true;
    }
    #endregion
}
