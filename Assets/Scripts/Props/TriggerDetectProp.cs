using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// ��������ⷽʽ�ĵ���
/// </summary>
public class TriggerDetectProp : DetectProp
{
    [Header("���������")]
    public new Collider2D collider;
    //��������
    protected ContactFilter2D _filter2D;

    //��⵽����ײ���б�
    protected List<Collider2D> _colliderResults;

    private void Start()
    {
        _filter2D = new ContactFilter2D();
        _colliderResults = new List<Collider2D>();
    }

    public override bool Detect()
    {
        if (collider != null)
        {
            //���ù���
            _filter2D.SetLayerMask(detectMask);
            //��Ⲣ���ظ���
            return collider.OverlapCollider(_filter2D, _colliderResults) != 0;
        }

        return false;
    }

    public override void DetectAction()
    {
        Debug.Log("ִ�м�⺯��");
    }
}
