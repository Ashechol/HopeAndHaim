using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 角色 Hope 的控制器
/// 
/// ArnoClare
/// </summary>
public class Hope : MonoBehaviour
{
    #region 移动参数
    //移动速度
    public float speed = 8f;
    //朝向
    public Direction direction = Direction.Up;
    #endregion

    #region 输入参数
    //键盘输入
    private bool _isForward, _isBackward;
    private bool _isLeft, _isRight;
    #endregion

    #region 状态参数
    //正在转向
    private bool _isRotating;
    //正在移动
    private bool _isMoving;
    #endregion

    #region 循环函数
    private void Update()
    {
        UserInput();
        PlayerMove();
    }

    private void PlayerMove()
    {
        //旋转
        if (_isRotating) {

        }
        //移动
        else if (_isMoving) {

        }
        //静止
        else {
            //先检查转向
            if (_isLeft || _isRight) {

            }
            //再检查移动
            else if (_isForward || _isBackward) {

            }
        }
    }
    #endregion

    #region 功能函数
    private void UserInput()
    {
        //没有转向时可以移动
        if (!_isRotating) {
            _isForward = Input.GetKey(KeyCode.W);
            _isBackward = Input.GetKey(KeyCode.S);
            //同时，没有移动的话，可以转向
            if (!_isMoving) {
                _isLeft = Input.GetKey(KeyCode.A);
                _isRight = Input.GetKey(KeyCode.D);
            }
        }
    }
    #endregion
}
