using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipsUI : MonoBehaviour
{
    public InteractionTips interactionTips;
    public BeginingTip beginingTip;
    public Text doorTip;

    void Awake()
    {
        UIManager.Instance.tipsUI = this;
    }

    public IEnumerator DoorLockTip()
    {
        float timer = 5;

        doorTip.enabled = true;

        while (timer > 0)
        {
            timer -= Time.deltaTime;
            yield return null;
        }

        doorTip.enabled = false;
    }
}
