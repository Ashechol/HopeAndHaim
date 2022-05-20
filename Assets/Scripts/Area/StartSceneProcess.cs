using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 开始界面流程
/// </summary>
public class StartSceneProcess : Singleton<StartSceneProcess>
{
    public ImageFader background;
    public AudioSourceFader bgm;
    public RawImageFader fader;
    public ImageFader logo;
    public CanvasGroupFader menu;
    public CanvasGroupFader loading;
    public CanvasGroupFader content;
    public CanvasGroupFader hint;
    public Button btnStart;
    public Button btnQuit;
    public Text main;
    public Text title;

    private AudioSource audioSource;

    public AudioClip mouseClip;
    public AudioClip spaceClip;

    private bool _isPreloading = false;
    private bool _isLoading = false;

    //加载界面随机显示内容
    private List<string> _contentList = new List<string>() {
        "天下没有不散的宴席。不过，只要一个人跟他自己没有走散，就没有什么可担心的。",
        "我们必须全力以赴，同时又不抱有任何希望。",
        "可能等你过完自己的一生，到最后却发现了解别人胜过了解你自己。",
        "我们可以做许多白日梦，可以失败，可以哭泣，可以万丈光芒。"
    };
    private List<string> _titleList = new List<string>() {
        "――《单独中的洞见》",
        "――里尔克",
        "――柏瑞尔・马卡姆",
        "――萨特"
    };

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        fader.endActions += logo.StartFading;
        logo.endActions += menu.StartFading;
        btnStart.onClick.AddListener(ButtonStart);
        btnQuit.onClick.AddListener(ButtonQuit);

        loading.endActions += content.StartFading;
    }

    private void Update()
    {
        if (_isPreloading) {
            //TODO: 预加载
            SceneLoadManager.Instance.PreloadLevel("Episode-1");
            bool finish = SceneLoadManager.Instance.finishPreload;

            if (content.isEnd && finish && !_isLoading) {
                hint.StartFading();
                if (Input.GetKeyDown(KeyCode.Space)) {
                    Debug.Log("按下空格");
                    _isLoading = true;

                    audioSource.clip = spaceClip;
                    audioSource.Play();
                    //背景音频渐渐淡出
                    bgm.endActions += LoadNextScene;
                    bgm.isFadeIn = false;
                    bgm.Reset();
                    bgm.StartFading();
                    //背景渐渐淡出
                    fader.endActions += LoadNextScene;
                    fader.isFadeIn = false;
                    fader.Reset();
                    fader.StartFading();
                }
            }
        }
    }

    private void ButtonStart()
    {
        Debug.Log("点击开始按钮");
        //播放音频
        audioSource.clip = mouseClip;
        audioSource.Play();

        StartLoading();
    }

    private void ButtonQuit()
    {
        Debug.Log("点击退出按钮");
        //播放音频
        audioSource.clip = mouseClip;
        audioSource.Play();

        //TODO: 退出游戏
    }

    private void StartLoading()
    {
        //失活 button
        btnStart.enabled = false;
        btnQuit.enabled = false;

        _isPreloading = true;

        background.StartFading();
        loading.gameObject.SetActive(true);
        //设置内容
        int index = Random.Range(0, _contentList.Count);
        main.text = _contentList[index];
        title.text = _titleList[index];

        loading.StartFading();
    }

    private void LoadNextScene()
    {
        if (bgm.isEnd && fader.isEnd) {
            //TODO: 进入下一场景
            SceneLoadManager.Instance.LoadIn();
        }
    }
}
