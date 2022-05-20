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
    [Header("Episode One UI")]
    public GameObject episodeOneCanvas;
    //全屏黑幕
    public Image curtain;
    private bool _underCurtain = true;
    //对话框
    public Text dialogue;
    //提示框
    public Text hint;

    [Header("Episode Two UI")]
    public GameObject episodeTwoCanvas;
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

        if (SceneLoadManager.Instance.CurrentScene.name == "Episode-1") {
            GameObject.Instantiate(episodeOneCanvas);
            curtain = episodeOneCanvas.transform.GetChild(0).GetComponent<Image>();
            var panel = episodeOneCanvas.transform.GetChild(1);
            dialogue = panel.GetChild(0).GetComponent<Text>();
            hint = panel.GetChild(1).GetComponent<Text>();
        }

        if (SceneLoadManager.Instance.CurrentScene.name == "Episode-2") {
            GameObject.Instantiate(episodeTwoCanvas);
        }
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
    }

    private void EpisodeOneUpdate()
    {
        if (Input.GetKeyDown(KeyCode.M)) {
            _underCurtain = !_underCurtain;
            curtain.gameObject.SetActive(_underCurtain);
        }
    }

    //显示对话框
    public void DisplayDialogue(string txt, int size)
    {
        if (!dialogue.gameObject.activeSelf) {
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
        if (dialogue == null) {
            return;
        }

        if (dialogue.gameObject.activeSelf) {
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
}
