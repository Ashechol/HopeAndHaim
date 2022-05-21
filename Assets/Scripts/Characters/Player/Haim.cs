using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using Cinemachine;
using System;

public enum MoveDirection { Forward, Backward, LeftRight }
public class Haim : MonoBehaviour
{
    AudioSource _footStep;
    Vector3 _dest;
    Light2D _selfLight;
    Animator _anim;
    MoveDirection _direction;
    bool _walk;

    // Player Interact
    List<ICanInteract> _interactions = new List<ICanInteract>();
    int _interactionId;
    public int InteractionsCnt { get { return _interactions.Count; } }

    [Header("Basic")]
    public float speed;
    public float sightRadius = 1.5f;
    public bool hasKey = false;
    public float idleTimer = 10;
    public bool isIdle;
    private bool _idleChecking;

    [Header("Hack Surveillance Camera")]
    public bool canHack;
    public SecurityCamera securityCamera;
    public float camSightRadius;
    public CinemachineVirtualCamera followCamera;
    public float farDistance = 4;
    public float nearDistance = 2.6f;

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

        if (GameManager.Instance.gameMode == GameManager.GameMode.Gameplay)
            IdleTimeCheck();

        // idle dialog
        if (isIdle)
        {
            GameManager.Instance.dialog.PlayRandomIdle();
            isIdle = false;
        }
    }

    void IdleTimeCheck()
    {
        if (!_idleChecking && !isIdle)
        {
            StartCoroutine(TimeCheck());
            _idleChecking = true;
        }
    }

    IEnumerator TimeCheck()
    {
        float timer = idleTimer;

        while (timer > 0 && !Input.anyKeyDown)
        {
            timer -= Time.deltaTime;
            yield return null;
        }

        isIdle = timer < 0 ? true : false;
        _idleChecking = false;
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

    public void SetDestination(Vector3 pos)
    {
        GameManager.Instance.dialog.PlayRandomDialog();
        if (GameManager.Instance.gameMode == GameManager.GameMode.Gameplay)
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
        UIManager.Instance.tipsUI.interactionTips.ShowTip();

        if (CanInteract())
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Stop();
                _interactionId = ClampIndex(_interactionId);
                _interactions[_interactionId].Interact();
            }

            if (Input.GetKeyDown(KeyCode.Tab) &&
                GameManager.Instance.gameMode == GameManager.GameMode.Gameplay)
            {
                _interactionId = ClampIndex(_interactionId + 1);
            }
        }
    }

    bool CanInteract()
    {
        if (_interactions.Count != 0)
        {
            switch (GameManager.Instance.gameMode)
            {
                case GameManager.GameMode.Dialog:
                    return false;
                case GameManager.GameMode.GameOver:
                    return false;
                case GameManager.GameMode.Information:
                    return true;
                case GameManager.GameMode.Gameplay:
                    return true;
                case GameManager.GameMode.Hacking:
                    return true;
            }
        }

        return false;
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
            _selfLight.enabled = false;
            canHack = false;

            GameManager.Instance.gameMode = GameManager.GameMode.Hacking;
            followCamera.Follow = securityCamera.transform;
            followCamera.m_Lens.OrthographicSize = farDistance;
            // StartCoroutine(FollowCameraAway());
        }

        else if (GameManager.Instance.gameMode == GameManager.GameMode.Hacking)
        {
            securityCamera.TurnOff();
            securityCamera.hacking = false;
            _selfLight.enabled = true;
            canHack = true;

            GameManager.Instance.gameMode = GameManager.GameMode.Gameplay;
            followCamera.Follow = transform;
            followCamera.m_Lens.OrthographicSize = nearDistance;
            // StartCoroutine(FollowCameraClose());
        }
    }

    //TODO: 镜头平滑切换
    // IEnumerator FollowCameraAway()
    // {
    //     StopCoroutine(FollowCameraClose());
    //     while (farDistance - followCamera.m_Lens.OrthographicSize > 0.05f)
    //     {
    //         followCamera.m_Lens.OrthographicSize = Mathf.Lerp(followCamera.m_Lens.OrthographicSize,
    //                                                         farDistance, 5 * Time.deltaTime);
    //         yield return null;
    //     }
    // }

    // IEnumerator FollowCameraClose()
    // {
    //     StopCoroutine(FollowCameraAway());
    //     while (followCamera.m_Lens.OrthographicSize - closeDistance > 0.05f)
    //     {
    //         followCamera.m_Lens.OrthographicSize = Mathf.Lerp(followCamera.m_Lens.OrthographicSize,
    //                                                         closeDistance, 5 * Time.deltaTime);
    //         yield return null;
    //     }
    // }

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
