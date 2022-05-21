using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridController : MonoBehaviour, IGameObserver
{
    Grid _grid;
    Tilemap _interactiveMap;
    Vector3Int _previousMousePos;
    bool _gameOver;

    public Tile hoverTile;

    void Awake()
    {
        _grid = GetComponent<Grid>();
        _interactiveMap = transform.GetChild(3).GetComponent<Tilemap>();
    }

    void OnEnable()
    {
        GameManager.Instance.AddObserver(this);
    }

    void OnDisable()
    {
        GameManager.Instance.RemoveObserver(this);
    }

    void Update()
    {
        if (!_gameOver && GameManager.Instance.haim.ArriveAtDest())
        {
            SetInteractiveMap();
        }
    }

    void SetInteractiveMap()
    {
        if (MouseManager.Instance.MousePos != _previousMousePos &&
            MouseManager.Instance.pathMap.HasTile(MouseManager.Instance.MousePos))
        {
            _interactiveMap.SetTile(_previousMousePos, null);
            _interactiveMap.SetTile(MouseManager.Instance.MousePos, hoverTile);
            _previousMousePos = MouseManager.Instance.MousePos;

        }
    }

    public void GameOverNotify()
    {
        _interactiveMap.SetTile(_previousMousePos, null);
        _interactiveMap.SetTile(MouseManager.Instance.MousePos, null);
        _gameOver = true;
    }
}
