using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverUI : ChoiceUI
{
    protected override void Start()
    {
        UIManager.Instance.gameOverPanel = gameObject;
        base.Start();
    }

    protected override void ChooseYes()
    {
        SceneLoadManager.Instance.LoadEnding(GameManager.GameEnding.Obey);
    }

    protected override void ChooseNo()
    {
        SceneLoadManager.Instance.ReLoadScene();
    }
}
