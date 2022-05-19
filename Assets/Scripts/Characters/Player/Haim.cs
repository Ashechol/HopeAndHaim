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
    public float sightRadius = 1.5f;
    public bool hasKey = false;

    [Header("Hack Surveillance Camera")]
    public bool canHack;
    private bool _isHacking;
    public SecurityCamera securityCamera;
    public float camSightRadius;

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
        _selfLight.pointLightOuterRadius = sightRadius;
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

            Vector2 vec = (_dest - transform.position).normalized;
            float dot = Vector2.Dot(transform.up, vec);
            float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;

            if (angle <= 45)
                _direction = MoveDirection.Backward;
            else if (angle <= 135)
                _direction = MoveDirection.LeftRight;
            else
                _direction = MoveDirection.Forward;

            if (_dest.x - transform.position.x > 0)
                transform.localScale = new Vector3(-1, 1, 1);
            else
                transform.localScale = new Vector3(1, 1, 1);

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
            {
                Stop();
                _interactionId = ClampIndex(_interactionId);
                _interactions[_interactionId].Interact();
            }

            if (Input.GetKeyDown(KeyCode.Tab) && !_isHacking)
            {
                _interactionId = ClampIndex(_interactionId + 1);
            }
        }
    }

    int ClampIndex(int interactionId)
    {
        if (interactionId >= _interactions.Count)
            interactionId = 0;

        return interactionId;
    }


    public bool ArriveAtDest()
    {
        return (_dest - transform.position).sqrMagnitude < 0.1f;
    }

    /// <summary>
    /// 黑入摄像机
    /// </summary>
    public void HackCam()
    {
        if (canHack)
        {
            Stop();
            securityCamera.TurnOn(camSightRadius);
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

    public void GetSightBuff(float buffMultiplier)
    {
        sightRadius *= buffMultiplier;
        _selfLight.pointLightOuterRadius = sightRadius;
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
            _dest = (transform.position - _dest).normalized * 0.05f + transform.position;
        }
    }
}
