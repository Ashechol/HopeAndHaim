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
    bool hasBeenNotified;

    [Header("Episode One UI")]
    //全屏黑幕
    private Image _curtain;
    private bool _underCurtain = true;
    //对话框
    private Text _dialogue;
    //提示框
    private Text _hint;

    [Header("Episode Two UI")]
    public GameObject gameOverPanel;
    public GameObject lastQuestionPanel;
    public GameObject infomationPanel;
    public TipsUI tipsUI;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        GameManager.Instance.AddObserver(this);
    }

    void OnDisale()
    {
        GameManager.Instance.RemoveObserver(this);
    }

    private void Update()
    {
        //第一幕中检测 M 键按下，关闭幕布
        if (SceneLoadManager.Instance.CurrentScene.name == "Episode-1")
            EpisodeOneUpdate();

        if (SceneLoadManager.Instance.CurrentScene.name == "Episode-2") {
            if (GameManager.Instance.gameMode == GameManager.GameMode.Gameplay)
                hasBeenNotified = false;
        }
    }

    private void EpisodeOneUpdate()
    {
        if (Input.GetKeyDown(KeyCode.M)) {
            _underCurtain = !_underCurtain;
            _curtain.gameObject.SetActive(_underCurtain);
        }
    }

    public void RegisterHope(Hope hope)
    {
        _curtain = hope.curtain;
        _dialogue = hope.dialogue;
        _hint = hope.hint;
    }

    //显示对话框
    public void DisplayDialogue(string txt, int size)
    {
        if (!_dialogue.gameObject.activeSelf) {
            //Debug.Log("显示对话框");
            _dialogue.gameObject.SetActive(true);
        }

        _dialogue.text = txt;
        _dialogue.fontSize = size;
    }

    //隐藏对话框
    public void CleanDialogue()
    {
        //不知道为什么会出现空引用，显示 Text 已被摧毁
        if (_dialogue == null) {
            return;
        }

        if (_dialogue.gameObject.activeSelf) {
            _dialogue.text = "";

            _dialogue.gameObject.SetActive(false);
        }
    }

    //显示游戏提示
    public void DisplayHint(string txt, int size)
    {
        if (!_hint.gameObject.activeSelf) {
            _hint.gameObject.SetActive(true);
        }

        _hint.text = txt;
        _hint.fontSize = size;
    }

    //隐藏提示框
    public void CleanHint()
    {
        if (_hint == null) {
            return;
        }

        if (_hint.gameObject.activeSelf) {
            _hint.text = "";

            _hint.gameObject.SetActive(false);
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

    public void ShowSkipTip()
    {
        if (tipsUI != null)
            tipsUI.beginingTip.ShowSkipTip(2);
    }

    public void ShowDoorLockTip()
    {
        StartCoroutine(tipsUI.DoorLockTip());
    }

    public void GameOverNotify()
    {
        if (!hasBeenNotified) {
            GameManager.Instance.music.PlayDeadMusic();
            gameOverPanel.SetActive(true);
            tipsUI.gameObject.SetActive(false);
            infomationPanel.SetActive(false);

            hasBeenNotified = true;
        }
    }
}
