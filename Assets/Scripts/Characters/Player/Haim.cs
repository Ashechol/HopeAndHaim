using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Haim : MonoBehaviour
{
    AudioSource _footStep;
    Vector3 _dest;
    Light2D _selfLight;

    [Header("Movement")]
    public float speed;

    [Header("Hack Surveillance Camera")]
    public bool canHack;
    private bool _isHacking;
    public Surveillance cam;

    void Awake()
    {
        _footStep = GetComponent<AudioSource>();
        _selfLight = GetComponentInChildren<Light2D>();
    }

    void Start()
    {
        GameManager.Instance.RegisterHaim(this);
        _dest = transform.position;
    }

    void Update()
    {
        HackCam();
        MoveToDestination();
    }

    void SetDestination(Vector3 pos)
    {
        if (!_isHacking)
        {
            _dest = pos;
        }
    }

    void OnEnable()
    {
        MouseManager.Instance.OnMouseClicked += SetDestination;
    }

    void OnDisable()
    {
        MouseManager.Instance.OnMouseClicked -= SetDestination;
    }

    void MoveToDestination()
    {
        transform.position = Vector3.MoveTowards(transform.position, _dest, speed * Time.deltaTime);

        if (!ArriveAtDest() && !_footStep.isPlaying)
        {
            _footStep.Play();
        }
    }

    public bool ArriveAtDest()
    {
        return (_dest - transform.position).sqrMagnitude < 0.3f;
    }

    /// <summary>
    /// 黑入摄像机
    /// </summary>
    void HackCam()
    {
        if (Input.GetKeyDown(KeyCode.E) && canHack)
        {
            cam.TurnOn();
            cam.hacking = true;
            _isHacking = true;
            _selfLight.enabled = false;
            canHack = false;
        }

        else if (Input.GetKeyDown(KeyCode.Escape) && _isHacking)
        {
            cam.TurnOff();
            cam.hacking = false;
            _isHacking = false;
            _selfLight.enabled = true;
            canHack = true;
        }
    }

    public void Die()
    {
        _dest = transform.position;
    }

    void OnCollisionStay2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Wall"))
        {
            _dest = (transform.position - _dest).normalized * 0.1f + transform.position;
        }
    }
}
