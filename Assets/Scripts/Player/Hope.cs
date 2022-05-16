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
    #region 组件
    private new Rigidbody2D rigidbody;
    #endregion

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
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        UserInput();

        //显示朝向
        Debug.DrawRay(transform.position, GetDirectionVector(), Color.green);
    }

    private void FixedUpdate()
    {
        PlayerMove();
    }

    private void PlayerMove()
    {
        //旋转
        if (_isRotating) {
            Debug.Log("Hope 正在转向");
            //结束移动
            Invoke("ResetIsRotating", 0.2f);
        }
        //移动
        else if (_isMoving) {
            Debug.Log("Hope 正在移动");
            //检查输入，一旦无输入立即停下
            if (!_isForward && !_isBackward) {
                _isMoving = false;
                rigidbody.velocity = Vector2.zero;
            }
        }
        //静止
        else {
            //先检查转向
            if (_isLeft || _isRight) {
                _isRotating = true;
                //开始转向
                ChangeDirection(_isLeft);
                Debug.Log($"Hope 改变方向:{direction}");
            }
            //再检查移动
            else if (_isForward || _isBackward) {
                _isMoving = true;
                //设置移动速度
                Vector3 dir = GetDirectionVector();
                if (_isForward) {
                    rigidbody.velocity = new Vector2(dir.x, dir.y) * speed * Time.fixedDeltaTime;
                }
                else if (_isBackward) {
                    rigidbody.velocity = new Vector2(dir.x, dir.y) * -speed * Time.fixedDeltaTime;
                }
            }
        }
    }
    #endregion

    #region 功能函数
    private void UserInput()
    {
        _isForward = Input.GetKey(KeyCode.W);
        _isBackward = Input.GetKey(KeyCode.S);
        _isLeft = Input.GetKeyDown(KeyCode.A);
        _isRight = Input.GetKeyDown(KeyCode.D);
    }

    public Vector3 GetDirectionVector()
    {
        switch (direction) {
            case Direction.Left:
                return Vector3.left;
            case Direction.Right:
                return Vector3.right;
            case Direction.Up:
                return Vector3.up;
            case Direction.Down:
                return Vector3.down;
        }
        return Vector3.zero;
    }

    private void ChangeDirection(bool left)
    {
        switch (direction) {
            case Direction.Left:
                if (left) direction = Direction.Down;
                else direction = Direction.Up;
                break;
            case Direction.Right:
                if (left) direction = Direction.Up;
                else direction = Direction.Down;
                break;
            case Direction.Up:
                if (left) direction = Direction.Left;
                else direction = Direction.Right;
                break;
            case Direction.Down:
                if (left) direction = Direction.Right;
                else direction = Direction.Left;
                break;
        }
    }

    private void ResetIsRotating()
    {
        _isRotating = false;
    }
    #endregion
}
