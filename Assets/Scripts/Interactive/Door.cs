using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, ICanInteract
{
    Animator _anim;
    Collider2D _coll;
    SpriteRenderer _renderer;
    AudioSource _sound;
    bool _isOpen;

    [Header("Lock And Key")]
    public bool isLock;

    void Awake()
    {
        _anim = GetComponent<Animator>();
        _coll = GetComponent<Collider2D>();
        _renderer = GetComponent<SpriteRenderer>();
        _sound = GetComponent<AudioSource>();
    }

    void Update()
    {
        PlayerNearBy();
        _anim.SetBool("open", _isOpen);
    }

    void PlayerNearBy()
    {
        var collider = Physics2D.OverlapCircle(transform.position, 1.0f);

        if (collider.CompareTag("Player"))
        {
            GameManager.Instance.haim.AddInteraction(this);

            if ((GameManager.Instance.PlayerPosition
            - transform.position).sqrMagnitude < 0.5f && _isOpen)
                _renderer.sortingOrder = 3;
            else
                _renderer.sortingOrder = 1;
        }

        else if (_isOpen)
        {
            _sound.Play();
            _isOpen = false;
            _coll.enabled = true;
            GameManager.Instance.haim.RemoveInteraction(this);
        }
    }

    public void Interact()
    {
        if (!_isOpen && !isLock)
        {
            _isOpen = true;
            _coll.enabled = false;
            _sound.Play();
        }
    }
}
