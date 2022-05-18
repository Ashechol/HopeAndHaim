using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    Animator _anim;
    Collider2D _coll;

    [Header("Lock And Key")]
    public bool isLock;
    public int keyCode;

    void Awake()
    {
        _anim = GetComponent<Animator>();
        _coll = GetComponent<Collider2D>();
    }




}
