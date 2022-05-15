using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// �������ߡ�
/// ���������߼�⣬һ����⵽��Ӧ�����򴥷�������
/// </summary>
public class TriggerProp : MonoBehaviour
{
    public enum Direction
    {
        UP,
        DOWN,
        LEFT,
        RIGHT
    }

    /// <summary>
    /// ��ײ���ģʽ
    /// </summary>
    public enum Mode
    {
        RAY,
        TRIGGER
    }

    public Mode mode = Mode.RAY;
    public LayerMask checkMask;

    [Header("���߼��")]
    public Direction direction = Direction.UP;
    public float raycastLength = 1f;
    protected RaycastHit2D _hit;

    [Header("���������")]
    public new Collider2D collider;
    protected ContactFilter2D _filter2D;
    protected List<Collider2D> _colliderResults;

    private void Start()
    {
        checkMask = LayerMask.GetMask("Player");
        _filter2D = new ContactFilter2D();
        _colliderResults = new List<Collider2D>();
    }

    private void Update()
    {
        if (CollisionDetect())
        {
            OnHit();
        }
    }

    /// <summary>
    /// �ж�������ײ
    /// </summary>
    /// <returns></returns>
    public bool CollisionDetect()
    {
        if (mode == Mode.RAY)
        {
            //Debug.Log("���߽Ӵ�����ײ��");
            return HitObject();
        }
        else if (mode == Mode.TRIGGER)
        {
            int l = CollideObject();
            //Debug.Log($"�������Ӵ�����ײ��:{l}");
            //if (l != 0)
            //{
            //    foreach (Collider2D item in _colliderResults)
            //    {
            //        Debug.Log(item);
            //    }
            //}
            return l != 0;
        }

        return false;
    }

    private bool HitObject()
    {
        Vector2 dir = Vector2.up;
        switch (direction)
        {
            case Direction.UP:
                dir = Vector2.up;
                break;
            case Direction.DOWN:
                dir = Vector2.down;
                break;
            case Direction.LEFT:
                dir = Vector2.left;
                break;
            case Direction.RIGHT:
                dir = Vector2.right;
                break;
        }
        _hit = Physics2D.Raycast(transform.position, dir, raycastLength, checkMask);
        Debug.DrawRay(transform.position, dir * raycastLength, Color.green);

        return _hit;
    }

    private int CollideObject()
    {
        if (collider != null)
        {
            //��Ϊ������
            //collider.isTrigger = true;
            //���ù���
            _filter2D.SetLayerMask(checkMask);
            //��Ⲣ���ظ���
            return collider.OverlapCollider(_filter2D, _colliderResults);
        }

        return 0;
    }

    public virtual void OnHit()
    {
        Debug.Log("ִ����ײ����");
    }
}
