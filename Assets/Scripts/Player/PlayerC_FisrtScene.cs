using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家控制器_第一幕
/// 一格一格移动，且移动过程中不接受输入
/// 需要玩家一开始的位置属于方格内
/// 目前仅检测 Wall
/// </summary>
public class PlayerC_FisrtScene : MonoBehaviour
{
    public float speed = 20f;

    private float _xVelocity, _yVelocity;
    private bool _isMoving = false;
    private float _obstacleCheck = 1f;
    private Vector3 _targetPos;

    private int _wallMask;

    private void Start()
    {
        _wallMask = LayerMask.GetMask("Wall");
    }

    private void Update()
    {
        _xVelocity = Input.GetAxisRaw("Horizontal");
        _yVelocity = Input.GetAxisRaw("Vertical");

        Movement();
    }

    private void FixedUpdate()
    {
        
    }

    private void Movement()
    {
        //静止，且有输入时
        if (!_isMoving && (_xVelocity != 0 || _yVelocity != 0))
        {
            //设置射线
            Vector3 direction;
            RaycastHit2D wallHit;
            if (_xVelocity != 0)
            {
                direction = Vector3.right * _xVelocity;
                wallHit = Physics2D.Raycast(transform.position, direction, _obstacleCheck, _wallMask);
                Debug.DrawRay(transform.position, Vector3.right * _xVelocity, Color.green);
            }
            else
            {
                direction = Vector3.up * _yVelocity;
                wallHit = Physics2D.Raycast(transform.position, direction, _obstacleCheck, _wallMask);
                Debug.DrawRay(transform.position, Vector3.up * _yVelocity, Color.green);
            }

            //碰撞检测
            if (wallHit)
            {
                Debug.Log("碰撞 Wall 层");
            }
            //没有碰撞，可以移动
            else
            {
                Debug.Log("没有碰撞");
                _isMoving = true;
                _targetPos = transform.position + direction;
            }
        }
        //移动中
        else if (_isMoving)
        {
            //抵达目的地
            if (transform.position == _targetPos)
            {
                Debug.Log("抵达目的地");
                _isMoving = false;
            }
            //正在移动
            else
            {
                Debug.Log("正在移动");
                transform.position = Vector3.MoveTowards(transform.position, _targetPos, Time.deltaTime * speed);
            }
        }
    }
}
