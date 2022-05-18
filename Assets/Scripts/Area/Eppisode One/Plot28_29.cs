using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 第一幕剧情节点 28-29
/// </summary>
public class Plot28_29 : MonoBehaviour
{
    public AudioSource controllerSource;
    public AudioSource soundSource;

    public List<GameObject> plotList = new List<GameObject>();

    //关闭门控制器音效
    public void Plot28_CloseControllerSE()
    {
        controllerSource.Stop();
    }

    //打开最终大门处的音响
    public void Plot29_OpenSound()
    {
        soundSource.Play();
        //同时激活最终大门区域
        foreach (GameObject item in plotList) {
            item.SetActive(true);
        }
    }
}
