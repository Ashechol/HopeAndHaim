using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    //对话框
    public Text dialogue;

    public void DisplayDialogue(string txt, int size)
    {
        dialogue.gameObject.SetActive(true);

        dialogue.text = txt;
        dialogue.fontSize = size;
    }

    public void CleanDialogue()
    {
        dialogue.text = "";

        dialogue.gameObject.SetActive(false);
    }
}
