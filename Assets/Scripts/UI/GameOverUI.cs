using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    Button yesBtn, noBtn;

    void Awake()
    {
        yesBtn = transform.GetChild(1).GetComponent<Button>();
        noBtn = transform.GetChild(2).GetComponent<Button>();

        yesBtn.onClick.AddListener(SceneLoadManager.Instance.LoadEndingOne);
        noBtn.onClick.AddListener(SceneLoadManager.Instance.ReLoadScene);
    }
    void Start()
    {

    }
}
