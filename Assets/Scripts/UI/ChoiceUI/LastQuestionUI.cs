using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastQuestionUI : ChoiceUI
{
    protected override void Start()
    {
        UIManager.Instance.lastQuestionPanel = gameObject;
        base.Start();
    }

    protected override void ChooseYes()
    {
        SceneLoadManager.Instance.LoadEnding(GameManager.GameEnding.NewLife);
    }

    protected override void ChooseNo()
    {
        SceneLoadManager.Instance.LoadEnding(GameManager.GameEnding.Exile);
    }
}
