using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MouseManager : Singleton<MouseManager>
{
    Camera _mainCam;

    Vector3Int _gridPos;

    public Tilemap tilemap;

    public event Action<Vector3> OnMouseClicked;

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
            if (tilemap.HasTile(_gridPos))
            {
                Vector3 dest = tilemap.GetCellCenterWorld(_gridPos);
                // tilemap.SetTileFlags(gridPos, TileFlags.None);
                // tilemap.SetColor(gridPos, Color.red);
                OnMouseClicked?.Invoke(dest);
            }
        }
    }

    void TilePointAt()
    {
        Vector2 mousePos = _mainCam.ScreenToWorldPoint(Input.mousePosition);
        _gridPos = tilemap.WorldToCell(mousePos);
    }
}
