using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfomationUI : MonoBehaviour
{
    Image _image;

    void Awake()
    {
        _image = GetComponent<Image>();
    }
    void Start()
    {
        UIManager.Instance.infomationPanel = this.gameObject;
    }

    public void SetInformation(Sprite information)
    {
        _image.sprite = information;
    }
}
