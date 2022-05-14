using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MouseManager : Singleton<MouseManager>
{
    Camera _mainCam;

    public event Action<Vector2> OnMouseClicked;

    protected override void Awake()
    {
        base.Awake();
        _mainCam = Camera.main;
    }

    void Update()
    {
        MouseControll();
    }

    void MouseControll()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = _mainCam.ScreenToWorldPoint(Input.mousePosition);

            Debug.Log(mousePos);

            OnMouseClicked?.Invoke(mousePos);
        }
    }
}
