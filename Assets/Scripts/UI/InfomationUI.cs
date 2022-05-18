using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfomationUI : MonoBehaviour
{
    void Start()
    {
        UIManager.Instance.gameOverPanel = this.gameObject;
    }
}
