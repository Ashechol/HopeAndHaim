using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Utils;

public class EnemyController : MonoBehaviour
{
    NavMeshAgent _agent;
    Transform _target;

    [Header("Sight")]
    public float sightRadius;
    [Range(0.0f, 180.0f)] public float sightAngle;


    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
    }

    void Update()
    {
        MoveToPlayer();
    }

    bool FoundPlayer()
    {
        var colliders = Physics2D.OverlapCircleAll(transform.position, sightRadius);

        foreach (var target in colliders)
        {
            if (target.CompareTag("Player"))
            {
                _target = target.transform;
                return true;
            }
        }

        return false;
    }

    void MoveToPlayer()
    {
        if (FoundPlayer())
        {
            var vectorToTarget = (_target.position - transform.position).normalized;
            float dot = Vector3.Dot(-transform.up, vectorToTarget);
            float threshold = Mathf.Cos(Mathf.Deg2Rad * sightAngle);
            if (dot >= threshold)
                _agent.destination = _target.position;
        }

    }


    void OnDrawGizmosSelected()
    {
        GizmosEx.DrawWireArc(transform, sightRadius, sightAngle, Color.red);
    }

}
