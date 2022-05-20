﻿using System.Collections;
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
    private BoxCollider2D _collider;

    private AudioSource _hearSource;
    public AudioSource HearSource => _hearSource;
    private AudioSource _speakSource;
    public AudioSource SpeakSource => _speakSource;
    private AudioSource _bgmSource;
    public AudioSource BgmSource => _bgmSource;
    private AudioSource _footSource;
    public AudioSource FootSource => _footSource;
    #endregion

    #region 输入参数
    //键盘输入
    private bool _isForward, _isBackward;
    private bool _isLeft, _isRight;
    public bool IsForward => _isForward;
    public bool IsBackward => _isBackward;
    #endregion

    #region 移动参数
    //移动速度
    public float speed = 100f;
    //旋转速度
    public float rSpeed = 1f;
    //朝向
    private Direction _direction = Direction.Up;
    public Direction HopeDirection => _direction;
    #endregion

    #region 状态参数
    //转向中
    private bool _isRotating;
    public bool IsRotating => _isRotating;
    //移动中
    private bool _isMoving;
    public bool IsMoving => _isMoving;
    //静置行为
    private bool _isInStatic = false;
    public bool IsInStatic => _isInStatic;
    #endregion

    #region 剧情参数
    //静止时长
    [SerializeField]
    private float _staticTime = 0;
    //静态语音触发事件
    public float staticPlotThreshold = 4f;
    //剧情移动状态
    private bool _isPlotMoving = false;
    //剧情移动目标
    private Vector3 _plotTarget;
    //剧情移动时长
    private float _plotDuration;
    //剧情移动速度
    private float _plotSpeed;
    //记录原本的速度
    private float _originalSpeed;
    #endregion

    #region 提供外部
    public void ChangeFootstep(AudioClip clip)
    {
        _footSource.clip = clip;
        _footSource.loop = true;
        if (_isMoving) {
            _footSource.Play();
        }
    }
    public void ChangeBgm(AudioClip clip, bool isLoop = true)
    {
        _bgmSource.clip = clip;
        _bgmSource.loop = isLoop;
        _bgmSource.Play();
    }
    public void SetDirection(Direction direction)
    {
        _direction = direction;
        _collider.transform.rotation = DirectionUtility.GetRotationQuaternion(_direction);
    }
    public void StartHopeMovement()
    {
        _isMoving = true;
        _isForward = true;
    }
    public void MoveToTarget(Vector3 target, float duration)
    {
        _isPlotMoving = true;
        _plotTarget = target;
        _plotDuration = duration;
        //根据时长计算速度
        float length = (target - transform.position).magnitude;
        _plotSpeed = length / _plotDuration;
    }
    //剧情降速
    public void LowSpeed()
    {
        _originalSpeed = speed;
        speed = 10f;
    }
    public void OriginalSpeed()
    {
        speed = _originalSpeed;
    }
    #endregion

    #region 功能函数

    //接收玩家输入
    private void PlayerInput()
    {
        _isForward = Input.GetKey(KeyCode.W);
        _isBackward = Input.GetKey(KeyCode.S);
        //使用 GetKeyDown 可能出现按键被吞的感觉
        _isLeft = Input.GetKey(KeyCode.A);
        _isRight = Input.GetKey(KeyCode.D);
    }

    //停止 Hope
    public void StopHope()
    {
        _isMoving = false;
        //由于触发 Timeline 后会禁止输入，所以需要修改输入标记
        _isForward = false;
        _isBackward = false;
        _rigidbody.velocity = Vector2.zero;
        _footSource.Stop();
    }

    //播放 Hope 语音
    private void VoiceWallCollide()
    {
        //碰撞语音有点过长，效果不太好，临时修改
        if (_speakSource.isPlaying && _speakSource.time >= 2.5f) {
            _speakSource.Stop();
        }

        if (!_speakSource.isPlaying) {
            Debug.Log($"Hope 触发碰撞语音。SpeakSource:{_speakSource.isPlaying}");
            _speakSource.clip = AudioManager.Instance.GetCollideClip();
            _speakSource.loop = false;
            _speakSource.time = 0.6f;
            _speakSource.Play();
        }
    }

    //播放 Hope 静态语音
    private void VoiceStatic()
    {
        //适合触发时：没有播放剧情语音，也没有播放撞墙语音
        if (!_hearSource.isPlaying && !_speakSource.isPlaying) {
            Debug.Log("Hope 触发静置语音");
            _speakSource.clip = AudioManager.Instance.GetHopeStaticClip();
            _speakSource.loop = false;
            _speakSource.volume = 1;
            _speakSource.Play();
        }

    }

    private void StaticInput()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            GameManager.Instance.isEpisodeOneEnd = true;
        }
    }

    //Hope 移动逻辑
    private void HopeMovement()
    {
        //静止
        if (!_isRotating && !_isMoving) {
            //先检查转向
            if (_isLeft || _isRight) {
                _isRotating = true;
                //改变方向
                _direction = DirectionUtility.ChangeDirection(_direction, _isLeft);
            }
            //再检查移动
            else if (_isForward || _isBackward) {
                _isMoving = true;
            }
        }

        //先检查旋转
        if (_isRotating) {
            //Debug.Log("Hope 正在转向");
            //旋转插值
            _collider.transform.rotation = Quaternion.Slerp(_collider.transform.rotation,
                DirectionUtility.GetRotationQuaternion(_direction), rSpeed * Time.fixedDeltaTime);
            //解决 Slerp 问题：其运行到最后一点角度会变得极慢，因此当到一定角度内直接改变角度
            float rz = Quaternion.Angle(_collider.transform.rotation, DirectionUtility.GetRotationQuaternion(_direction));
            //结束检查
            if (Mathf.Abs(rz) <= 10) {
                _collider.transform.rotation = DirectionUtility.GetRotationQuaternion(_direction);
                //Debug.Log("Hope 结束转向");
                _isRotating = false;
            }
        }
        //再检查移动
        else if (_isMoving) {
            //Debug.Log("Hope 正在移动");

            //启动声音
            if (!_footSource.isPlaying) {
                _footSource.Play();
            }

            //由于移动输入是持续的，因此要不停判断状态
            Vector2 dir = DirectionUtility.GetDirectionVector(_direction);
            //先判断前进
            if (_isForward) {
                _rigidbody.velocity = dir * speed * Time.fixedDeltaTime;
            }
            //再判断后退
            else if (_isBackward) {
                _rigidbody.velocity = dir * -speed * Time.fixedDeltaTime;
            }
            //最后判断静止
            else {
                //Debug.Log("Hope 结束移动");
                StopHope();
            }
        }
    }

    //Hope 静置逻辑
    private void HopeStatic()
    {
        //静置状态
        if (_isInStatic) {
            //接收静置输入
            StaticInput();

            //触发静置语音
            VoiceStatic();

            //静置状态解除
            if (_isMoving) {
                _isInStatic = false;
                //隐藏提示
                UIManager.Instance.CleanHint();
            }
        }
        //非静置状态
        else {
            //不移动增加时间
            if (!_isMoving) {
                _staticTime += Time.deltaTime;
            }
            //移动清空
            else {
                _staticTime = 0;
            }

            //进入静置
            if (_staticTime >= staticPlotThreshold) {
                _isInStatic = true;

                //静置状态下不记录静置时间
                _staticTime = 0;

                //显示静置提示
                UIManager.Instance.DisplayHint("点击空格键跳过该关卡", 30);
            }
        }
    }

    #endregion

    #region 循环函数

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = transform.Find("Collider").GetComponent<BoxCollider2D>();

        _hearSource = transform.Find("Hear").GetComponent<AudioSource>();
        _speakSource = transform.Find("Speak").GetComponent<AudioSource>();
        _bgmSource = transform.Find("Bgm").GetComponent<AudioSource>();
        _footSource = transform.Find("Footstep").GetComponent<AudioSource>();
    }

    private void Start()
    {
        GameManager.Instance.RegisterHope(this);
    }
    private void Update()
    {
        //Debug.Log("HearSource:" + _hearSource.isPlaying);
        //Debug.Log("SpeakSource:" + _speakSource.isPlaying);
        //在输入模式下才接收输入
        if (GameManager.Instance.CanInput()) {
            PlayerInput();
            //在非角色行动时不应该执行静置行为
            HopeStatic();
        }
        //检查剧情语音时，是否有静置语音
        if (_hearSource.isPlaying) {
            _speakSource.Stop();
        }

        //显示朝向
        Debug.DrawRay(transform.position, DirectionUtility.GetDirectionVector(_direction), Color.green);
    }

    private void FixedUpdate()
    {
        if (_isPlotMoving) {
            transform.position = Vector3.MoveTowards(transform.position, _plotTarget, _plotSpeed * Time.deltaTime);
            if (transform.position == _plotTarget) {
                _isPlotMoving = false;
            }
        }
        else {
            HopeMovement();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //检测前方碰撞
        //播放碰撞语音：碰撞层级为 Wall && 角色正在移动
        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall") && _isMoving) {
            VoiceWallCollide();
        }
    }

    #endregion
}
