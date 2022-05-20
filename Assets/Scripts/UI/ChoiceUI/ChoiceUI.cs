using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceUI : MonoBehaviour
{
    Button yesBtn, noBtn;

    void Awake()
    {
        yesBtn = transform.GetChild(1).GetComponent<Button>();
        noBtn = transform.GetChild(2).GetComponent<Button>();

        yesBtn.onClick.AddListener(ChooseYes);
        noBtn.onClick.AddListener(ChooseNo);
    }

    protected virtual void Start()
    {
        gameObject.SetActive(false);
    }

    protected virtual void ChooseYes()
    {

    }

    protected virtual void ChooseNo()
    {

    }
}
