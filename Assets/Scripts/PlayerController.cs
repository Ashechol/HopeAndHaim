using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    NavMeshAgent _agent;

    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
    }

    void Start()
    {
        MouseManager.Instance.OnMouseClicked += MoveToPosition;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void MoveToPosition(Vector2 pos)
    {
        _agent.destination = pos;
    }
}
