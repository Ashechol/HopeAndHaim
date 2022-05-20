using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// UI 管理器
/// </summary>
public class UIManager : Singleton<UIManager>, IGameObserver
{
    //全屏黑幕
    public Image curtain;

    private bool _underCurtain = true;

    //对话框
    public Text dialogue;

    //提示框
    public Text hint;

    public GameObject gameOverPanel;
    public GameObject infomationPanel;
    public TipsUI tipsUI;

    protected override void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        GameManager.Instance.AddObserver(this);

        if (_currentScene.name == "Start Scene")
            StartScene_Start();
    }

    void OnDisale()
    {
        GameManager.Instance.RemoveObserver(this);
    }

    private void Update()
    {
        if (SceneLoadManager.Instance.CurrentScene.name == "Start Scene")
            StartScene_Update();
        else if (SceneLoadManager.Instance.CurrentScene.name == "Episode-1")
            EpisodeOneUpdate();
    }

    private void EpisodeOneUpdate()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            _underCurtain = !_underCurtain;
            curtain.gameObject.SetActive(_underCurtain);
        }
    }

    //显示对话框
    public void DisplayDialogue(string txt, int size)
    {
        if (!dialogue.gameObject.activeSelf)
        {
            //Debug.Log("显示对话框");
            dialogue.gameObject.SetActive(true);
        }

        dialogue.text = txt;
        dialogue.fontSize = size;
    }

    //隐藏对话框
    public void CleanDialogue()
    {
        //不知道为什么会出现空引用，显示 Text 已被摧毁
        if (dialogue == null)
        {
            return;
        }

        if (dialogue.gameObject.activeSelf)
        {
            dialogue.text = "";

            dialogue.gameObject.SetActive(false);
        }
    }

    //显示游戏提示
    public void DisplayHint(string txt, int size)
    {
        if (!hint.gameObject.activeSelf) {
            hint.gameObject.SetActive(true);
        }

        hint.text = txt;
        hint.fontSize = size;
    }

    //隐藏提示框
    public void CleanHint()
    {
        if (hint == null) {
            return;
        }

        if (hint.gameObject.activeSelf) {
            hint.text = "";

            hint.gameObject.SetActive(false);
        }
    }

    public void ShowInformation(Sprite information)
    {
        infomationPanel.SetActive(true);
        infomationPanel.GetComponent<InfomationUI>().SetInformation(information);
    }

    public void CloseInformation()
    {
        infomationPanel.SetActive(false);
    }

    public void ShowBeginingTip()
    {
        if (tipsUI != null)
            StartCoroutine(tipsUI.beginingTip.ShowTip());
    }

    public void ShowDoorLockTip()
    {
        StartCoroutine(tipsUI.DoorLockTip());
    }

    public void GameOverNotify()
    {
        gameOverPanel.SetActive(true);
        tipsUI.gameObject.SetActive(false);
        infomationPanel.SetActive(false);
    }

    #region 开始界面
    private Fader _faderStartScene;

    private Image _logoOfStartScene;

    private CanvasGroup _menuOfStartScene;

    private bool _isFadingStartScene = false;

    //标题渐入
    private void LogoFadeIn()
    {
        _logoOfStartScene.color = Color.Lerp(_logoOfStartScene.color, Color.white, 1f * Time.deltaTime);
    }

    //菜单渐入
    private void MenuFadeIn()
    {
        _menuOfStartScene.alpha = Mathf.Lerp(_menuOfStartScene.alpha, 1, 1f * Time.deltaTime);
    }

    private void ClickBtnStart()
    {
        Debug.Log("开始按钮");
    }

    private void ClickBtnQuit()
    {
        Debug.Log("退出按钮");
    }

    private void StartFadeIn()
    {
        //开始渐入
        _isFadingStartScene = true;
    }

    private void StartScene_Start()
    {
        _faderStartScene = GameObject.Find("Fader").GetComponent<Fader>();
        _faderStartScene.AddEndAction(StartFadeIn);

        _logoOfStartScene = GameObject.Find("Logo").GetComponent<Image>();

        GameObject menu = GameObject.Find("Menu");
        _menuOfStartScene = menu.GetComponent<CanvasGroup>();
        menu.transform.Find("BtnStart").GetComponent<Button>().onClick.AddListener(ClickBtnStart);
        menu.transform.Find("BtnQuit").GetComponent<Button>().onClick.AddListener(ClickBtnQuit);
    }

    private void StartScene_Update()
    {
        if (_isFadingStartScene) {
            //标题渐入
            LogoFadeIn();

            //菜单渐入
            if (_logoOfStartScene.color.a >= 0.9) {
                _logoOfStartScene.color = Color.white;

                MenuFadeIn();

                if (_menuOfStartScene.alpha >= 0.9) {
                    _menuOfStartScene.alpha = 1;
                    _isFadingStartScene = false;
                }
            }
        }
    }
    #endregion
}
