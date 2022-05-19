using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public enum MoveDirection { Forward, Backward, LeftRight }
public class Haim : MonoBehaviour
{
    AudioSource _footStep;
    Vector3 _dest;
    Light2D _selfLight;
    Animator _anim;
    bool _walk;
    MoveDirection _direction;

    // Player Interact
    List<ICanInteract> _interactions = new List<ICanInteract>();
    int _interactionId;

    [Header("Basic")]
    public float speed;
    public bool hasKey = false;

    [Header("Hack Surveillance Camera")]
    public bool canHack;
    private bool _isHacking;
    public SecurityCamera securityCamera;

    void Awake()
    {
        _footStep = GetComponent<AudioSource>();
        _selfLight = GetComponentInChildren<Light2D>();
        _anim = GetComponent<Animator>();
        _direction = MoveDirection.Forward;
    }

    void Start()
    {
        GameManager.Instance.haim = this;
        _dest = transform.position;
    }

    void Update()
    {
        MoveToDestination();
        Interact();
        _anim.SetBool("walk", _walk);
        _anim.SetInteger("direction", (int)_direction);
    }

    void OnEnable()
    {
        MouseManager.Instance.OnMouseClicked += SetDestination;
    }

    void OnDisable()
    {
        MouseManager.Instance.OnMouseClicked -= SetDestination;
        GameManager.Instance.haim = null;
    }

    void SetDestination(Vector3 pos)
    {
        if (!_isHacking)
        {
            _dest = pos;

            if (_dest.y - transform.position.y > 0)
                _direction = MoveDirection.Backward;

            else if (_dest.y - transform.position.y < 0)
                _direction = MoveDirection.Forward;

            else if (_dest.x - transform.position.x < 0)
            {
                _direction = MoveDirection.LeftRight;
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (_dest.x - transform.position.x > 0)
            {
                _direction = MoveDirection.LeftRight;
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
    }

    void MoveToDestination()
    {
        transform.position = Vector3.MoveTowards(transform.position, _dest, speed * Time.deltaTime);

        if (!ArriveAtDest())
        {
            _walk = true;

            if (!_footStep.isPlaying)
                _footStep.Play();
        }
        else
            _walk = false;
    }

    void Interact()
    {
        if (_interactions.Count != 0)
        {
            if (Input.GetKeyDown(KeyCode.E))
                _interactions[_interactionId].Interact();

            if (Input.GetKeyDown(KeyCode.Tab))
            {
                _interactionId = ChooseInteraction(_interactionId);
            }
        }
    }

    int ChooseInteraction(int interactionId)
    {
        interactionId++;
        if (interactionId >= _interactions.Count)
            interactionId = 0;

        return interactionId;
    }


    public bool ArriveAtDest()
    {
        return (_dest - transform.position).sqrMagnitude < 0.3f;
    }

    /// <summary>
    /// 黑入摄像机
    /// </summary>
    public void HackCam()
    {
        if (canHack)
        {
            Stop();
            securityCamera.TurnOn();
            securityCamera.hacking = true;
            _isHacking = true;
            _selfLight.enabled = false;
            canHack = false;
        }

        else if (_isHacking)
        {
            securityCamera.TurnOff();
            securityCamera.hacking = false;
            _isHacking = false;
            _selfLight.enabled = true;
            canHack = true;
        }
    }


    public void AddInteraction(ICanInteract interaction)
    {
        _interactions.Add(interaction);
    }

    public void RemoveInteraction(ICanInteract interaction)
    {
        _interactions.Remove(interaction);
    }

    public void Stop()
    {
        _dest = transform.position;
    }

    public void Die()
    {
        Stop();
    }

    void OnCollisionStay2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Wall"))
        {
            _dest = (transform.position - _dest).normalized * 0.1f + transform.position;
        }
    }
}
