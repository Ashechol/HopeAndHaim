using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MouseManager : Singleton<MouseManager>
{
    Camera _mainCam;

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
            Vector2 mousePos = _mainCam.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int gridPos = tilemap.WorldToCell(mousePos);

            if (tilemap.HasTile(gridPos))
            {
                Vector3 dest = tilemap.GetCellCenterWorld(gridPos);
                // tilemap.SetTileFlags(gridPos, TileFlags.None);
                // tilemap.SetColor(gridPos, Color.red);
                OnMouseClicked?.Invoke(dest);
            }
        }
    }
}
