using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

/// <summary>
/// 管理游戏进程
/// </summary>
public class GameManager : Singleton<GameManager>
{
<<<<<<< HEAD
    List<IGameObserver> _observers;

=======
    public enum GameMode
    {
        Normal,         //操控角色
        GamePlay,       //播放动画
        DialogueMoment  //对话暂停
    }
    public GameMode gameMode;

    #region 组件
    public Hope hope;
    // Haim one of the main actors
>>>>>>> 190969af5d2a13b40b826c7061754fe878a77289
    public Haim haim;
    #endregion

    private void Start()
    {
        //根据 Director 状态设置 GameMode
        if (TimelineManager.Instance.currentDirector.playOnAwake) {
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
                TimelineManager.Instance.ResumeTimeline();
            }
        }
        //加速播放，调试用
        else if (gameMode == GameMode.GamePlay) {
            if (Input.GetKeyDown(KeyCode.Q)) {
                TimelineManager.Instance.SpeedUp();
            }
        }
    }

    public void RegisterHaim(Haim haim)
    {
        this.haim = haim;
    }

<<<<<<< HEAD
    public void AddObserver(IGameObserver observer)
    {
        _observers.Add(observer);
    }

    public void RemoveObserver(IGameObserver observer)
    {
        _observers.Remove(observer);
    }
=======
    #region GameMode 函数
    /// <summary>
    /// 判断当前的游戏模式是否接收用户输入
    /// </summary>
    public bool CanInput()
    {
        return gameMode == GameMode.Normal;
    }
    #endregion
>>>>>>> 190969af5d2a13b40b826c7061754fe878a77289
}
