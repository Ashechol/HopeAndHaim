using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Haim : MonoBehaviour
{
    NavMeshAgent _agent;

    [Header("Hack Surveillance Camera")]
    public bool canHack;
    private bool _isHacking;
    public Surveillance cam;

    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
    }

    void Start()
    {
        GameManager.Instance.RegisterHaim(this);
    }
    void OnEnable()
    {
        MouseManager.Instance.OnMouseClicked += MoveToPosition;
    }

    void OnDisable()
    {
        MouseManager.Instance.OnMouseClicked -= MoveToPosition;
    }

    void Update()
    {
        HackCam();
    }

    void MoveToPosition(Vector3 pos)
    {
        if (!_isHacking)
        {
            _agent.isStopped = false;
            if (pos.x - transform.position.x == 0)
                pos.x += 0.01f;
            _agent.destination = pos;
        }
    }

    void HackCam()
    {
        if (Input.GetKeyDown(KeyCode.E) && canHack)
        {
            cam.TurnOn();
            cam.hacking = true;
            _isHacking = true;
        }

        else if (Input.GetKeyDown(KeyCode.Escape) && _isHacking)
        {
            cam.TurnOff();
            cam.hacking = false;
            _isHacking = false;
        }
    }
}
