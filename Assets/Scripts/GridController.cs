using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridController : MonoBehaviour
{
    Grid _grid;
    Tilemap _interactiveMap;
    Vector3Int _previousMousePos;

    public Tile hoverTile;

    void Awake()
    {
        _grid = GetComponent<Grid>();
        _interactiveMap = transform.GetChild(2).GetComponent<Tilemap>();
    }

    void Update()
    {
        SetInteractiveMap();
    }

    void SetInteractiveMap()
    {
        if (MouseManager.Instance.MousePos != _previousMousePos)
        {
            _interactiveMap.SetTile(_previousMousePos, null);
            _interactiveMap.SetTile(MouseManager.Instance.MousePos, hoverTile);
            _previousMousePos = MouseManager.Instance.MousePos;
        }
    }
}
