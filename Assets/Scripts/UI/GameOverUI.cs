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

        yesBtn.onClick.AddListener(ChooseYes);
        noBtn.onClick.AddListener(ChooseNo);
    }
    void Start()
    {
        UIManager.Instance.gameOverPanel = this.gameObject;
        gameObject.SetActive(false);
    }

    void ChooseYes()
    {
        SceneLoadManager.Instance.LoadEnding(GameManager.GameEnding.Obey);
    }

    void ChooseNo()
    {
        SceneLoadManager.Instance.ReLoadScene();
    }
}
