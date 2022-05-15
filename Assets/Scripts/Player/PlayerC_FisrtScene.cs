using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// ��ҿ�����_��һĻ
/// һ��һ���ƶ������ƶ������в���������
/// ��Ҫ���һ��ʼ��λ�����ڷ�����
/// Ŀǰ����� Wall
/// </summary>
public class PlayerC_FisrtScene : MonoBehaviour
{
    public float speed = 20f;
    //���ž�ֹ������ʱ����ֵ
    public float audioPlayTime = 5f;

    //�ƶ����
    private float _xVelocity, _yVelocity;
    private bool _isMoving = false;
    private float _obstacleCheck = 1f;
    private Vector3 _targetPos;

    private int _wallMask;

    //�� E �ж�
    private bool _pressedAction = false;
    public event UnityAction triggeAction;

    //��ֹ��ʼ�¼�
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
    /// ���� x, y ������룬���һ������
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns>������������һ������</returns>
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

    private void Movement()
    {
        //��ֹ
        if (!_isMoving)
        {
            //������
            if (_xVelocity != 0 || _yVelocity != 0)
            {
                //��ײ���
                Vector3 direction = GetDirection(_xVelocity, _yVelocity);
                RaycastHit2D wallHit = Physics2D.Raycast(transform.position, direction, _obstacleCheck, _wallMask);
                Debug.DrawRay(transform.position, new Vector3(direction.x * Mathf.Abs(_xVelocity), direction.y * Mathf.Abs(_yVelocity), 0), Color.green);

                //��⴦��
                if (wallHit)
                {
                    Debug.Log("��ײ Wall ��");
                }
                else
                {
                    Debug.Log("û����ײ����ʼ�ƶ�");
                    _isMoving = true;
                    _targetPos = transform.position + direction;
                }
            }
        }
        //�ƶ�
        else
        {
            //�ִ�Ŀ�ĵ�
            if (transform.position == _targetPos)
            {
                Debug.Log("�ִ�Ŀ�ĵ�");
                _isMoving = false;
            }
            //�����ƶ�
            else
            {
                Debug.Log("�ƶ���");
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

    //��ֹ��Ϊ
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
            Debug.Log("�ﵽ��ֹ����������ֵ");
        }
    }
}
