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

    public GameObject gameOverPanel;
    public GameObject infomationPanel;
    public TipsUI tipsUI;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
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
        if (SceneLoadManager.Instance.CurrentScene.name == "Episode-1")
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
