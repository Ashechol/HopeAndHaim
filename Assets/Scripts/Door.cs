using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    Animator _anim;
    Collider2D _coll;
    SpriteRenderer _renderer;
    AudioSource _sound;
    bool _isOpen;

    [Header("Lock And Key")]
    public bool isLock;
    public int keyCode;

    void Awake()
    {
        _anim = GetComponent<Animator>();
        _coll = GetComponent<Collider2D>();
        _renderer = GetComponent<SpriteRenderer>();
        _sound = GetComponent<AudioSource>();
    }

    void Update()
    {
        OpenDoor();
        _anim.SetBool("open", _isOpen);
    }

    void OpenDoor()
    {
        if (PlayerNearBy() && Input.GetKeyDown(KeyCode.E) && !_isOpen)
        {
            _isOpen = true;
            _coll.enabled = false;
            _sound.Play();
        }

        if ((GameManager.Instance.PlayerPosition
            - transform.position).sqrMagnitude < 0.5f && _isOpen)
            _renderer.sortingOrder = 3;
        else
            _renderer.sortingOrder = 1;
    }

    bool PlayerNearBy()
    {
        var collider = Physics2D.OverlapCircle(transform.position, 1.0f);

        if (collider.CompareTag("Player"))
            return true;

        if (_isOpen)
        {
            _sound.Play();
            _isOpen = false;
            _coll.enabled = true;
        }

        return false;
    }


}
