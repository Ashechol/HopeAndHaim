using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 第一幕的玩家控制器
/// 键盘输入，一格一格移动
/// </summary>
public class Hope_Ray : MonoBehaviour
{
    #region 移动相关字段
    //移动速度
    public float speed = 8f;
    #endregion

    #region 输入相关字段
    //按键方向
    private bool _isUp = false, _isDown = false;
    private bool _isLeft = false, _isRight = false;
    //行动键
    private bool _hasAction = false;
    #endregion

    #region 检测相关字段
    //射线检测长度
    private float _rayLength = 1f;
    //射线碰撞
    private RaycastHit2D _rayHit;
    #endregion

    #region 状态相关字段
    //是否在移动
    private bool _isMoving = false;
    //移动目标位置
    private Vector3 _targetPos;
    #endregion

    #region 音频相关字段
    //静止音频播放时间
    public float audioPlayTime = 4f;
    #endregion

    //行动键触发函数
    public event UnityAction actionCallBack;

    #region 循环函数
    private void Start()
    {
        actionCallBack += () => Debug.Log("Hope 执行行动函数");
    }

    private void Update()
    {
        UserInput();
        Movement();
        PlayerAction();
    }

    private void UserInput()
    {
        //移动
        if (!_isMoving) {
            _isUp = Input.GetKeyDown(KeyCode.W);
            _isDown = Input.GetKeyDown(KeyCode.S);
            _isLeft = Input.GetKeyDown(KeyCode.A);
            _isRight = Input.GetKeyDown(KeyCode.D);
        }

        //行动
        if (!_hasAction) {
            _hasAction = Input.GetKeyDown(KeyCode.E);
        }
    }

    private void Movement()
    {
        //静止
        if (!_isMoving) {
            //有移动输入
            if (GetMoveDirection() != Vector3.zero) {
                //碰撞检测
                GetRayHit(LayerMask.GetMask("Wall") | LayerMask.GetMask("Props"));
                //不同的碰撞物，不同的行为
                MoveHitAction();
            }
        }
        //移动
        else {
            //抵达目的地
            if (transform.position == _targetPos) {
                Debug.Log("Hope 抵达目的地");
                _isMoving = false;
            }
            //继续移动
            else {
                Debug.Log("Hope 移动中");
                transform.position = Vector3.MoveTowards(transform.position, _targetPos, Time.deltaTime * speed);
            }
        }
    }

    //按下行动键的玩家行动
    private void PlayerAction()
    {
        if (_hasAction) {
            actionCallBack();
            _hasAction = false;
        }
    }
    #endregion

    #region 功能函数
    private Vector3 GetMoveDirection()
    {
        if (_isUp) return Vector3.up;
        else if (_isDown) return Vector3.down;
        else if (_isLeft) return Vector3.left;
        else if (_isRight) return Vector3.right;

        return Vector3.zero;
    }

    //获得射线碰撞信息
    private RaycastHit2D GetRayHit(int layerMask = -1)
    {
        //碰撞
        _rayHit = Physics2D.Raycast(transform.position, GetMoveDirection(), _rayLength, layerMask);
        Debug.DrawRay(transform.position, GetMoveDirection() * _rayLength, Color.green);
        return _rayHit;
    }

    //不同碰撞行为
    private void MoveHitAction()
    {
        //碰撞
        if (_rayHit) {
            int layerIndex = _rayHit.collider.gameObject.layer;
            Debug.Log($"Hope 碰撞层级:{LayerMask.LayerToName(layerIndex)}");
            //碰撞墙壁
            if (layerIndex == LayerMask.NameToLayer("Wall")) {
                //TODO: 碰撞音频触发
                Debug.Log("Hope 触发碰撞音频");
            }
        }
        //没有碰撞
        else {
            Debug.Log("Hope 没有碰撞，下一帧开始移动");
            _isMoving = true;
            _targetPos = transform.position + GetMoveDirection();
        }
    }
    #endregion
}
