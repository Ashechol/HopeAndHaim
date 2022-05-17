using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Haim : MonoBehaviour
{
    AudioSource _audioSource;
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
        _audioSource = GetComponent<AudioSource>();
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
            _audioSource.Play();
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

        if (!ArriveAtDest())
        {
            _audioSource.loop = true;
        }
        else
            _audioSource.loop = false;
    }

    public bool ArriveAtDest()
    {
        return transform.position == _dest;
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
        }

        else if (Input.GetKeyDown(KeyCode.Escape) && _isHacking)
        {
            cam.TurnOff();
            cam.hacking = false;
            _isHacking = false;
            _selfLight.enabled = true;
        }
    }

    void OnCollisionStay2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Wall"))
        {
            _dest = (transform.position - _dest).normalized * 0.1f + transform.position;
        }
    }
}
