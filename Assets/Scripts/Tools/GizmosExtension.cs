using UnityEngine;

namespace Utils
{
    public class GizmosEx
    {
        public static void DrawWireArc(Transform origin, float radius, float angle, Color color,
                                       int segments = 50)
        {
            Gizmos.color = color;
            Vector3 dir = Quaternion.AngleAxis(angle, origin.forward) * -origin.up * radius;

            float mdeg = angle * 2 / segments;
            Vector3 currentPos = origin.position + dir;
            Vector3 oldPos;

            if (angle != 180)
                Gizmos.DrawLine(origin.position, currentPos);

            for (int i = 0; i <= segments; i++)
            {
                oldPos = currentPos;
                currentPos = origin.position + Quaternion.AngleAxis(-i * mdeg, origin.forward) * dir;
                Gizmos.DrawLine(oldPos, currentPos);
            }

            if (angle != 180)
                Gizmos.DrawLine(origin.position, currentPos);

        }
    }
}