using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Surveillance : MonoBehaviour
{
    Light2D _camLight;

    [Header("Settings")]
    public float angulerSpeed = 5.0f;
    public bool canBeHack;
    public bool hacking;

    void Awake()
    {
        _camLight = GetComponentInChildren<Light2D>();
        _camLight.enabled = false;
    }

    void Update()
    {
        if (hacking)
        {
            RotateCam();
        }
    }

    public void RotateCam()
    {
        float input = Input.GetAxisRaw("Horizontal");
        Vector3 euler = new Vector3(0.0f, 0.0f, -input * angulerSpeed * Time.deltaTime);
        transform.Rotate(euler);
    }

    public void TurnOn()
    {
        _camLight.enabled = true;
    }

    public void TurnOff()
    {
        _camLight.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {
            GameManager.Instance.haim.canHack = true;
            GameManager.Instance.haim.cam = this;
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {
            GameManager.Instance.haim.canHack = false;
            GameManager.Instance.haim.cam = null;
        }
    }
}
