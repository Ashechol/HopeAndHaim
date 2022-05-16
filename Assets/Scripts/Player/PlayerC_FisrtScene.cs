using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 玩家控制器_第一幕
/// 一格一格移动，且移动过程中不接受输入
/// 需要玩家一开始的位置属于方格内
/// 目前仅检测 Wall
/// </summary>
public class PlayerC_FisrtScene : MonoBehaviour
{
    public float speed = 20f;
    //播放静止语音的时间阈值
    public float audioPlayTime = 5f;

    //移动检测
    private float _xVelocity, _yVelocity;
    private bool _isMoving = false;
    private float _obstacleCheck = 1f;
    private Vector3 _targetPos;
    //前进方向
    private Vector3 _direction;

    //层级
    private int _wallMask;

    //按 E 行动
    private bool _pressedAction = false;
    public event UnityAction triggeAction;

    //静止开始事件
    private float _staticTime = 0;

    private void Start()
    {
        _wallMask = LayerMask.GetMask("Wall");
    }

    private void Update()
    {
        UserInput();

        Movement();
        PlayerAction();
        StaticAction();
    }

    private void UserInput()
    {
        if (!_isMoving)
        {
            _xVelocity = Input.GetAxisRaw("Horizontal");
            _yVelocity = Input.GetAxisRaw("Vertical");
        }

        _pressedAction = Input.GetKeyDown(KeyCode.E);
    }

    /// <summary>
    /// 根据 x, y 轴的输入，获得一个方向。
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns>上下左右其中一个方向</returns>
    private Vector3 GetDirection(float x, float y)
    {
        if (x != 0)
        {
            return new Vector3(x, 0, 0);
        }
        else
        {
            return new Vector3(0, y, 0);
        }
    }

    private RaycastHit2D GetHit(int layerMask)
    {
        _direction = GetDirection(_xVelocity, _yVelocity);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, _direction, _obstacleCheck, layerMask);
        Debug.DrawRay(transform.position, new Vector3(_direction.x * Mathf.Abs(_xVelocity), _direction.y * Mathf.Abs(_yVelocity), 0), Color.green);
        return hit;
    }

    private void Movement()
    {
        //静止
        if (!_isMoving)
        {
            //有输入
            if (_xVelocity != 0 || _yVelocity != 0)
            {
                //碰撞检测
                RaycastHit2D wallHit = GetHit(_wallMask);

                if (wallHit)
                {
                    Debug.Log("碰撞 Wall 层");
                }
                else
                {
                    Debug.Log("没有碰撞，开始移动");
                    _isMoving = true;
                    _targetPos = transform.position + _direction;
                }
            }
        }
        //移动
        else
        {
            //抵达目的地
            if (transform.position == _targetPos)
            {
                Debug.Log("抵达目的地");
                _isMoving = false;
            }
            //继续移动
            else
            {
                Debug.Log("移动中");
                transform.position = Vector3.MoveTowards(transform.position, _targetPos, Time.deltaTime * speed);
            }
        }
    }

    private void PlayerAction()
    {
        if (_pressedAction)
        {
            triggeAction();
        }
    }

    //静止行为
    private void StaticAction()
    {
        if (_isMoving)
        {
            _staticTime = 0;
        }
        else
        {
            _staticTime += Time.deltaTime;
        }

        if (_staticTime >= audioPlayTime)
        {
            Debug.Log("达到静止语音播放阈值");
        }
    }
}
