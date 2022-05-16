using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���߼�ⷽʽ�ĵ���
/// </summary>
public class RayDetectProp : DetectProp
{
    public enum Direction
    {
        UP,
        DOWN,
        LEFT,
        RIGHT
    }

    [Header("���߼��")]
    public Direction direction = Direction.UP;
    public float raycastLength = 1f;


    private Vector2 _dir;

    public override bool Detect()
    {
        switch (direction)
        {
            case Direction.UP:
                _dir = Vector2.up;
                break;
            case Direction.DOWN:
                _dir = Vector2.down;
                break;
            case Direction.LEFT:
                _dir = Vector2.left;
                break;
            default:
                _dir = Vector2.right;
                break;
        }

        Debug.DrawRay(transform.position, _dir * raycastLength, Color.green);
        return Physics2D.Raycast(transform.position, _dir, raycastLength, detectMask);
    }

    public override void DetectAction()
    {
        Debug.Log("ִ�м�⺯��");
    }
}
