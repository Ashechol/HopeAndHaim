using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��ҿ�����_��һĻ
/// һ��һ���ƶ������ƶ������в���������
/// ��Ҫ���һ��ʼ��λ�����ڷ�����
/// Ŀǰ����� Wall
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
        //��ֹ����������ʱ
        if (!_isMoving && (_xVelocity != 0 || _yVelocity != 0))
        {
            //��������
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

            //��ײ���
            if (wallHit)
            {
                Debug.Log("��ײ Wall ��");
            }
            //û����ײ�������ƶ�
            else
            {
                Debug.Log("û����ײ");
                _isMoving = true;
                _targetPos = transform.position + direction;
            }
        }
        //�ƶ���
        else if (_isMoving)
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
                Debug.Log("�����ƶ�");
                transform.position = Vector3.MoveTowards(transform.position, _targetPos, Time.deltaTime * speed);
            }
        }
    }
}
