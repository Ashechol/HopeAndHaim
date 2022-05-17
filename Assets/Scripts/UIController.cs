using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour, IGameObserver
{
    void Start()
    {
        GameManager.Instance.AddObserver(this);
    }

    public void GameOverNotify()
    {

    }
}
