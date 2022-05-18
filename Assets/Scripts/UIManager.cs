using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// UI 管理器
/// </summary>
public class UIManager : Singleton<UIManager>
{
    private Scene _currentScene;

    //对话框
    public Text dialogue;
    //全屏黑幕
    public Image curtain;

    private bool _underCurtain = true;

    private void Start()
    {
        _currentScene = SceneManager.GetActiveScene();
    }

    private void Update()
    {
        if (_currentScene.name == "Episode One")
            EpisodeOneUpdate();
    }

    private void EpisodeOneUpdate()
    {
        if (Input.GetKeyDown(KeyCode.C)) {
            _underCurtain = !_underCurtain;
            curtain.gameObject.SetActive(_underCurtain);
        }
    }

    //显示对话框
    public void DisplayDialogue(string txt, int size)
    {
        if (!dialogue.gameObject.activeSelf) {
            Debug.Log("显示对话框");
            dialogue.gameObject.SetActive(true);
        }

        dialogue.text = txt;
        dialogue.fontSize = size;
    }

    //隐藏对话框
    public void CleanDialogue()
    {
        if (dialogue.gameObject.activeSelf) {
            dialogue.text = "";

            //不知道为什么会出现空引用
            dialogue?.gameObject.SetActive(false);
        }
    }
}
