using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, ICanInteract
{
    Animator _anim;
    SpriteRenderer _renderer;
    AudioSource _sound;
    Collider2D _coll;
    bool _isOpen;

    [Header("Lock And Key")]
    public bool isLock;

    void Awake()
    {
        _anim = GetComponent<Animator>();
        _renderer = GetComponent<SpriteRenderer>();
        _sound = GetComponent<AudioSource>();
        _coll = transform.GetChild(0).GetComponent<Collider2D>();
    }

    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {
            GameManager.Instance.haim.AddInteraction(this);

            if ((GameManager.Instance.PlayerPosition
            - transform.position).sqrMagnitude < 0.5f && _isOpen)
                _renderer.sortingOrder = 3;
            else
                _renderer.sortingOrder = 1;
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {
            if (_isOpen)
            {
                _sound.Play();
                _anim.SetBool("open", false);
                _isOpen = false;
                _coll.enabled = true;
            }
            Debug.Log("Remove");
            GameManager.Instance.haim.RemoveInteraction(this);
        }
    }

    public void Interact()
    {
        if (!_isOpen)
        {
            if (isLock && !GameManager.Instance.haim.hasKey)
            {
                UIManager.Instance.ShowDoorLockTip();
                return;
            }

            _anim.SetBool("open", true);
            _isOpen = true;
            _coll.enabled = false;
            _sound.Play();
        }
    }
}
