using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���߻���
/// �����Χ��ײ
/// ��ײʱִ�к���
/// </summary>
public abstract class DetectProp : MonoBehaviour
{
    //���㼶
    public LayerMask detectMask;

    private void Update()
    {
        if (Detect())
        {
            DetectAction();
        }
    }

    public abstract bool Detect();

    public abstract void DetectAction();
}
