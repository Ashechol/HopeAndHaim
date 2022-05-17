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
    private Rigidbody2D _rigidbody;
    [HideInInspector]
    public AudioSource hearSource;
    [HideInInspector]
    public AudioSource speakSource;
    [HideInInspector]
    public AudioSource bgmSource;
    [HideInInspector]
    public AudioSource footSource;
    private BoxCollider2D _collider;
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
    public bool IsForward { get; set; }
    #endregion

    #region 状态参数
    //正在转向
    private bool _isRotating;
    //正在移动
    private bool _isMoving;
    public bool IsMoving { get; set; }
    #endregion

    #region 循环函数
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        hearSource = transform.Find("Hear").GetComponent<AudioSource>();
        speakSource = transform.Find("Speak").GetComponent<AudioSource>();
        bgmSource = transform.Find("Bgm").GetComponent<AudioSource>();
        footSource = transform.Find("Footstep").GetComponent<AudioSource>();
        _collider = transform.Find("Collider").GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        //判断 GameManager 的 GameMode
        if (GameManager.Instance.gameMode == GameManager.GameMode.Normal) {
            UserInput();
        }

        //显示朝向
        Debug.DrawRay(transform.position, GetDirectionVector(), Color.green);
    }

    private void FixedUpdate()
    {
        PlayerMove();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall") && _isMoving) {
            WallCollideVoice();
        }
    }

    private void PlayerMove()
    {
        //旋转
        if (_isRotating) {
            Debug.Log("Hope 正在转向");
            Invoke("ResetIsRotating", 0.1f);
        }
        //移动
        else if (_isMoving) {
            Debug.Log("Hope 正在移动");
            //检查输入，保持速度或停下
            ChangeVelocity();
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
                //开启脚步
                footSource.Play();
                //设置移动速度
                ChangeVelocity();
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
                if (left) {
                    direction = Direction.Down;
                }
                else {
                    direction = Direction.Up;
                }
                break;
            case Direction.Right:
                if (left) {
                    direction = Direction.Up;
                }
                else {
                    direction = Direction.Down;
                }
                break;
            case Direction.Up:
                if (left) {
                    direction = Direction.Left;
                }
                else {
                    direction = Direction.Right;
                }
                break;
            case Direction.Down:
                if (left) {
                    direction = Direction.Right;
                }
                else {
                    direction = Direction.Left;
                }
                break;
        }
        if (left) {
            _collider.transform.Rotate(Vector3.forward, 90);
        }
        else {
            _collider.transform.Rotate(Vector3.forward, -90);
        }
    }

    //设置移动速度
    private void ChangeVelocity()
    {
        Vector3 dir = GetDirectionVector();
        if (_isForward) {
            _rigidbody.velocity = new Vector2(dir.x, dir.y) * speed * Time.fixedDeltaTime;
        }
        else if (_isBackward) {
            _rigidbody.velocity = new Vector2(dir.x, dir.y) * -speed * Time.fixedDeltaTime;
        }
        else {
            StopHope();
        }
    }

    private void ResetIsRotating()
    {
        _isRotating = false;
    }

    public void StopHope()
    {
        _isMoving = false;
        _isForward = false;
        _isBackward = false;
        _rigidbody.velocity = Vector2.zero;
        footSource.Pause();
    }

    private void WallCollideVoice()
    {
        Debug.Log("Hope 撞到墙壁");
        if (!speakSource.isPlaying) {
            speakSource.clip = AudioManager.Instance.GetCollideClip();
            speakSource.loop = false;
            speakSource.time = 0.6f;
            speakSource.Play();
        }
    }
    #endregion
}
