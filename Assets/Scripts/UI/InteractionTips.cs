using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionTips : MonoBehaviour
{
    GameObject _currentTip;

    public GameObject interactTip;
    public GameObject closeTip;
    public GameObject switchTip;
    public GameObject camControlTip;
    public GameObject pickUpTip;

    public void ShowTip()
    {
        if (_currentTip != null)
            _currentTip.SetActive(false);

        _currentTip = ChooseTip();

        if (_currentTip != null)
            interactTip.SetActive(true);
    }

    GameObject ChooseTip()
    {
        if (GameManager.Instance.gameMode == GameManager.GameMode.Normal)
        {
            if (GameManager.Instance.haim.InteractionsCnt == 1)
                return interactTip;
        }

        return null;
    }
}
