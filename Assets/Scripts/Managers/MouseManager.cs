using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MouseManager : Singleton<MouseManager>
{
    Camera _mainCam;
    Vector3Int _gridPos;
    public Texture2D cursorTexture;
    public Tilemap pathMap;

    public Vector3Int MousePos { get { return _gridPos; } }

    public event Action<Vector3> OnMouseClicked;

    protected override void Awake()
    {
        base.Awake();
        _mainCam = Camera.main;
    }

    void Start()
    {
        Cursor.SetCursor(cursorTexture, new Vector2(32, 32), CursorMode.Auto);
    }

    void Update()
    {
        if (GameManager.Instance.haim.ArriveAtDest() &&
            GameManager.Instance.gameMode == GameManager.GameMode.Gameplay)
        {
            // Cursor.visible = false;
            MouseControll();
        }
    }

    void MouseControll()
    {
        Vector2 mousePos = _mainCam.ScreenToWorldPoint(Input.mousePosition);
        _gridPos = pathMap.WorldToCell(mousePos);
        if (Input.GetMouseButtonDown(0))
        {
            if (pathMap.HasTile(_gridPos))
            {
                Vector3 dest = pathMap.GetCellCenterWorld(_gridPos);
                // tilemap.SetTileFlags(gridPos, TileFlags.None);
                // tilemap.SetColor(gridPos, Color.red);
                OnMouseClicked?.Invoke(dest);
            }
        }
    }
}
