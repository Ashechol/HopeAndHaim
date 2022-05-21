using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    public Button btnEpisodeOne;
    public Button btnEpisodeTwo;
    public Button btnQuit;
    public Text main;
    public Text title;

    private AudioSource audioSource;

    public AudioClip mouseClip;
    public AudioClip spaceClip;

    //加载场景名
    private string nextLevel = "Episode-1";

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

        //是否激活第二幕按钮
        if (PlayerPrefs.GetInt(GameManager.Instance.episodeOneEndName, 0) != 0) {
            btnEpisodeTwo.gameObject.SetActive(true);
        }

        //淡入后，标体淡入
        fader.endActions += logo.StartFading;
        //淡入后，菜单淡入
        logo.endActions += menu.StartFading;
        //菜单开始淡入时，才激活按钮
        logo.endActions += () => {
            BtnEnable();
        };

        btnStart.onClick.AddListener(ButtonStart);
        btnEpisodeOne.onClick.AddListener(BtnEpisodeOne);
        btnEpisodeTwo.onClick.AddListener(BtnEpisodeTwo);
        btnQuit.onClick.AddListener(ButtonQuit);

        loading.endActions += content.StartFading;
        content.endActions += () => { StartCoroutine(PreLoadFirstScene()); };
    }

    private void BtnEnable()
    {
        btnStart.enabled = true;
        btnEpisodeOne.enabled = true;
        btnEpisodeTwo.enabled = true;
        btnQuit.enabled = true;
    }

    private void BtnDisable()
    {
        btnStart.enabled = false;
        btnEpisodeOne.enabled = false;
        btnEpisodeTwo.enabled = false;
        btnQuit.enabled = false;
    }

    private void ButtonStart()
    {
        Debug.Log("点击开始按钮");
        //播放音频
        PlayMouseClip();

        StartLoading();
    }

    private void BtnEpisodeOne()
    {
        Debug.Log("点击第一幕按钮");
        PlayMouseClip();

        nextLevel = "Episode-1";
        StartLoading();
    }

    private void BtnEpisodeTwo()
    {
        Debug.Log("点击第二幕按钮");
        PlayMouseClip();

        nextLevel = "Episode-2";
        StartLoading();
    }

    private void ButtonQuit()
    {
        Debug.Log("点击退出按钮");
        //播放音频
        PlayMouseClip();

        // 退出游戏
        Application.Quit();
    }

    private void PlayMouseClip()
    {
        audioSource.clip = mouseClip;
        audioSource.Play();
    }

    private void StartLoading()
    {
        //失活 button
        BtnDisable();

        //背景淡出
        background.StartFading();
        //激活加载界面
        loading.gameObject.SetActive(true);

        //设置内容
        int index = Random.Range(0, _contentList.Count);
        main.text = _contentList[index];
        title.text = _titleList[index];

        loading.StartFading();

        //开启预加载
        //StartCoroutine(PreLoadFirstScene());
    }

    IEnumerator PreLoadFirstScene()
    {
        yield return null;

        // 开始预载
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(nextLevel);
        // 预载完成不进行场景切换
        asyncOperation.allowSceneActivation = false;

        //加载中
        while (!asyncOperation.isDone) {

            //如果内容还没显示完，则不进行下一步
            while (!content.isEnd) {
                yield return null;
            }

            //加载完成
            if (asyncOperation.progress >= 0.9f) {
                //显示空格提示
                hint.StartFading();
                if (Input.GetKeyDown(KeyCode.Space)) {
                    //播放空格音效
                    audioSource.clip = spaceClip;
                    audioSource.Play();
                    //BGM 淡出
                    bgm.isFadeIn = false;
                    bgm.Reset();
                    bgm.StartFading();
                    //画面淡出
                    fader.isFadeIn = false;
                    fader.Reset();
                    fader.StartFading();

                    //等待淡出结束
                    while (!bgm.isEnd || !fader.isEnd) {
                        yield return null;
                    }

                    //淡出结束，切换场景
                    asyncOperation.allowSceneActivation = true;
                }
            }

            yield return null;
        }
    }
}
