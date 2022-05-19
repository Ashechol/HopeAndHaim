﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

/// <summary>
/// 管理游戏进程
/// </summary>
public class GameManager : Singleton<GameManager>
{
    public enum GameMode
    {
        Normal,         // 操控角色
        GamePlay,       // 播放动画
        DialogueMoment, // 对话暂停
        GameOver        // 游戏结束
    }
    List<IGameObserver> _observers = new List<IGameObserver>();

    public GameMode gameMode;
    public Hope hope;
    // Haim one of the main actors
    public Haim haim;

    void Start()
    {
        if (SceneLoadManager.Instance.CurrentScene.name == "Episode One")
            EpisodeOneStart();
    }

    void Update()
    {
        if (SceneLoadManager.Instance.CurrentScene.name == "Episode One")
            EpisodeOneUpdate();

        if (SceneLoadManager.Instance.CurrentScene.name == "Episode Two")
            EpisodeTwoUpdate();
    }

    public Vector3 PlayerPosition { get { return haim.transform.position; } }

    public void RegisterHope(Hope hope)
    {
        this.hope = hope;
    }

    public void RegisterHaim(Haim haim)
    {
        this.haim = haim;
    }

    public void AddObserver(IGameObserver observer)
    {
        _observers.Add(observer);
    }

    public void RemoveObserver(IGameObserver observer)
    {
        _observers.Remove(observer);
    }

    void EpisodeOneStart()
    {
        //根据 Director 状态设置 GameMode
        if (TimelineManager.Instance.currentDirector.playOnAwake) {
            gameMode = GameMode.GamePlay;
        }
        else {
            gameMode = GameMode.Normal;
        }
    }

    void EpisodeOneUpdate()
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

    void EpisodeTwoUpdate()
    {
        if (gameMode == GameMode.GameOver)
        {
            foreach (var observer in _observers)
            {
                observer.GameOverNotify();
            }
        }
    }

    #region GameMode 函数
    /// <summary>
    /// 判断当前的游戏模式是否接收用户输入
    /// </summary>
    public bool CanInput()
    {
        return gameMode == GameMode.Normal;
    }
}
