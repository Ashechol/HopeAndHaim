using System.Collections;
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
        Gameplay,       // 可操控模式
        Timeline,       // 播放动画
        Dialog,         // 对话
        Information,    // 物品信息
        Hacking,        // 玩家黑入摄像机
        GameOver        // 游戏结束
    }

    public enum GameEnding { NewLife, Exile, Obey }
    List<IGameObserver> _observers = new List<IGameObserver>();

    public GameMode gameMode;
    public GameEnding gameEnding;
    public Hope hope;
    // Haim one of the main actors
    public Haim haim;
    public DialogController dialog;
    public MusicController music;

    private bool _firstInEpisodeOne = true;

    //PP 持久化 第一幕结束过
    [HideInInspector]
    public string episodeOneEndName = "EpisodeOne_End";

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    //第一幕进入结束状态
    public bool isEpisodeOneEnd = false;

    void Start()
    {
        //Debug.Log($"当前场景:{SceneLoadManager.Instance.CurrentScene.name}");
        if (SceneLoadManager.Instance.CurrentScene.name == "Episode-1")
            EpisodeOneStart();

        // if (SceneLoadManager.Instance.CurrentScene.name == "Episode-2")
        //     EpisodeTwoStart();
    }

    void Update()
    {
        if (SceneLoadManager.Instance.CurrentScene.name == "Episode-1")
            EpisodeOneUpdate();

        if (SceneLoadManager.Instance.CurrentScene.name == "Episode-2")
            EpisodeTwoUpdate();
    }

    public Vector3 PlayerPosition { get { return haim.transform.position; } }

    public void RegisterHope(Hope hope)
    {
        this.hope = hope;
        //重置第一次标识
        _firstInEpisodeOne = true;
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
        if (TimelineManager.Instance.currentDirector.playOnAwake)
        {
            gameMode = GameMode.Timeline;
        }
        else
        {
            gameMode = GameMode.Gameplay;
        }
    }

    void EpisodeOneUpdate()
    {
        //第一次进入执行 Start
        if (_firstInEpisodeOne)
        {
            _firstInEpisodeOne = false;
            EpisodeOneStart();
        }

        //对话暂停时，需要按键恢复
        if (gameMode == GameMode.Dialog)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                TimelineManager.Instance.ResumeTimeline();
            }
        }
        //加速播放，调试用
        else if (gameMode == GameMode.Timeline)
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                TimelineManager.Instance.SpeedUp();
            }
        }
    }

    public IEnumerator EpisodeOneEnd()
    {
        //中断玩家输入
        Debug.Log("第一幕最后演出");
        gameMode = GameMode.Timeline;

        //记录第一幕结束
        if (PlayerPrefs.GetInt(episodeOneEndName, 0) == 0)
        {
            PlayerPrefs.SetInt(episodeOneEndName, 1);
        }

        hope.StopHope();
        hope.HearSource.clip = AudioManager.Instance.openDoorClip;
        hope.HearSource.loop = false;
        hope.HearSource.Play();

        while (hope.HearSource.time >= 3.16)
        {
            yield return null;
        }

        //开门语音播放完毕

        Debug.Log("第一幕结束");

        SceneLoadManager.Instance.LoadLevel("Middle-1-2");

    }

    void EpisodeTwoStart()
    {
        gameMode = GameMode.Gameplay;
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

        if (GameManager.Instance.gameMode == GameManager.GameMode.Dialog)
        {
            if (!UIManager.Instance.SkipTipActivated() &&
                Input.anyKeyDown)
            {
                UIManager.Instance.ShowSkipTip();
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                GameManager.Instance.dialog.StopBegining();
            }
        }
    }

    //第一幕结束逻辑


    /// <summary>
    /// 判断当前的游戏模式是否接收用户输入
    /// </summary>
    public bool CanInput()
    {
        return gameMode == GameMode.Gameplay;
    }

}
