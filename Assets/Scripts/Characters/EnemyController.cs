using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Utils;

public class EnemyController : MonoBehaviour
{
    NavMeshAgent _agent;
    Transform _target;
    Animator _anim;
    MoveDirection _direction;
    bool _walk = false;

    [Header("Sight")]
    public float sightRadius;
    [Range(0.0f, 180.0f)] public float sightAngle;


    void Awake()
    {
        _anim = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;

        _direction = MoveDirection.Forward;
    }

    void Start()
    {

    }

    void Update()
    {
        MoveToPlayer();
        _anim.SetInteger("direction", (int)_direction);
        _anim.SetBool("walk", _walk);
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
            {
                _walk = true;
                _agent.destination = _target.position;
            }
        }

        if (_agent.destination.y - transform.position.y > 0)
            _direction = MoveDirection.Backward;

        else if (_agent.destination.y - transform.position.y < 0)
            _direction = MoveDirection.Forward;

        else if (_agent.destination.x - transform.position.x < 0)
        {
            _direction = MoveDirection.LeftRight;
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (_agent.destination.x - transform.position.x > 0)
        {
            _direction = MoveDirection.LeftRight;
            transform.localScale = new Vector3(-1, 1, 1);
        }

    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {
            GameManager.Instance.gameMode = GameManager.GameMode.GameOver;
            GameManager.Instance.haim.Die();
        }

    }

    void OnDrawGizmosSelected()
    {
        GizmosEx.DrawWireArc(transform, sightRadius, sightAngle, Color.red);
    }

}
