using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UI 管理器
/// </summary>
public class UIManager : Singleton<UIManager>, IGameObserver
{
    //对话框
    public Text dialogue;
    public GameObject gameOverPanel;
    public GameObject infomationPanel;

    void Start()
    {
        GameManager.Instance.AddObserver(this);
    }

    void OnDisale()
    {
        GameManager.Instance.RemoveObserver(this);
    }

    //显示对话框
    public void DisplayDialogue(string txt, int size)
    {
        dialogue.gameObject.SetActive(true);

        dialogue.text = txt;
        dialogue.fontSize = size;
    }

    //隐藏对话框
    public void CleanDialogue()
    {
        dialogue.text = "";

        dialogue.gameObject.SetActive(false);
    }

    public void GameOverNotify()
    {
        gameOverPanel.SetActive(true);
    }
}
