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

    public void ShowTip()
    {
        if (_currentTip != null)
            _currentTip.SetActive(false);

        _currentTip = ChooseTip();

        if (_currentTip != null)
            _currentTip.SetActive(true);
    }

    GameObject ChooseTip()
    {
        if (GameManager.Instance.gameMode == GameManager.GameMode.Gameplay)
        {
            if (GameManager.Instance.haim.InteractionsCnt == 1)
                return interactTip;

            if (GameManager.Instance.haim.InteractionsCnt > 1)
                return switchTip;
        }

        if (GameManager.Instance.gameMode == GameManager.GameMode.Information)
        {
            if (GameManager.Instance.haim.InteractionsCnt > 0)
                return closeTip;
        }

        if (GameManager.Instance.gameMode == GameManager.GameMode.Hacking)
            return camControlTip;


        return null;
    }
}
