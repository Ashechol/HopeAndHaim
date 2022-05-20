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
    public int InteractionsCnt { get { return _interactions.Count; } }

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
        }

        else if (GameManager.Instance.gameMode == GameManager.GameMode.Hacking)
        {
            securityCamera.TurnOff();
            securityCamera.hacking = false;
            _selfLight.enabled = true;
            canHack = true;

            GameManager.Instance.gameMode = GameManager.GameMode.Gameplay;
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
