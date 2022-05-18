using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UI 管理器
/// </summary>
public class UIManager : Singleton<UIManager>
{
    //对话框
    public Text dialogue;

    //显示对话框
    public void DisplayDialogue(string txt, int size)
    {
        Debug.Log("显示对话框");
        dialogue.gameObject.SetActive(true);

        dialogue.text = txt;
        dialogue.fontSize = size;
    }

    //隐藏对话框
    public void CleanDialogue()
    {
        dialogue.text = "";

        //不知道为什么会出现空引用
        dialogue?.gameObject.SetActive(false);
    }
}
