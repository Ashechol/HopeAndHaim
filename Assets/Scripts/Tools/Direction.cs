using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    Up,
    Down,
    Left,
    Right
}

/// <summary>
/// 方向有关函数
/// </summary>
public static class DirectionUtility
{
    /// <summary>
    /// 改变方向
    /// </summary>
    /// <param name="direction">当前方向</param>
    /// <param name="left">是否左转</param>
    static public Direction ChangeDirection(Direction direction, bool left)
    {
        switch (direction)
        {
            case Direction.Left:
                if (left)
                {
                    return Direction.Down;
                }
                else
                {
                    return Direction.Up;
                }
            case Direction.Right:
                if (left)
                {
                    return Direction.Up;
                }
                else
                {
                    return Direction.Down;
                }
            case Direction.Up:
                if (left)
                {
                    return Direction.Left;
                }
                else
                {
                    return Direction.Right;
                }
            case Direction.Down:
                if (left)
                {
                    return Direction.Right;
                }
                else
                {
                    return Direction.Left;
                }
        }
        //此处不应该执行到
        Debug.LogError("不明错误！Direction 出现了未知值");
        return Direction.Up;
    }
    /// <summary>
    /// 获得方向相应的向量
    /// </summary>
    /// <param name="direction"></param>
    static public Vector3 GetDirectionVector(Direction direction)
    {
        switch (direction)
        {
            case Direction.Left:
                return Vector3.left;
            case Direction.Right:
                return Vector3.right;
            case Direction.Up:
                return Vector3.up;
            case Direction.Down:
                return Vector3.down;
        }
        //此处不应该执行到
        Debug.LogError("不明错误！Direction 出现了未知值");
        return Vector3.zero;
    }
    /// <summary>
    /// 获得方向相应的旋转
    /// </summary>
    /// <param name="direction"></param>
    static public Quaternion GetRotationQuaternion(Direction direction)
    {
        switch (direction)
        {
            case Direction.Left:
                return Quaternion.Euler(0, 0, 90);
            case Direction.Right:
                return Quaternion.Euler(0, 0, -90);
            case Direction.Up:
                return Quaternion.Euler(0, 0, 0);
            case Direction.Down:
                return Quaternion.Euler(0, 0, 180);
        }
        //此处不应该执行到
        Debug.LogError("不明错误！Direction 出现了未知值");
        return Quaternion.Euler(0, 0, 0);
    }
}
