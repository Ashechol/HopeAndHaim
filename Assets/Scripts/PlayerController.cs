using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    NavMeshAgent _agent;

    public float speed;
    Vector3 destination;

    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
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
        // transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
    }

    void MoveToPosition(Vector3 pos)
    {
        _agent.isStopped = false;
        if (pos.x - transform.position.x == 0)
            pos.x += 0.01f;
        _agent.destination = pos;
        Debug.Log(_agent.destination);
        Debug.Log(_agent.velocity);


        // destination = pos;

    }
}
