using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class SecurityCamera : MonoBehaviour, ICanInteract
{
    Light2D _camLight;
    AudioSource _camSound;
    bool _isPlaying;

    [Header("Settings")]
    public float angulerSpeed = 5.0f;
    public bool canBeHack;
    public bool hacking;
    public float minAngle;
    public float maxAngle;

    [Header("Audio")]
    public List<AudioClip> rotateSound;
    public AudioClip switchSound;

    void Awake()
    {
        _camSound = GetComponent<AudioSource>();
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

        if (input != 0 && !_isPlaying)
            StartCoroutine(PlayRotateSound());

        Vector3 euler = new Vector3(0.0f, 0.0f, -input * angulerSpeed * Time.deltaTime);
        transform.Rotate(euler);
    }

    public void TurnOn(float sightRadius)
    {
        _camLight.pointLightOuterRadius = sightRadius;
        _camLight.enabled = true;
        _camSound.clip = switchSound;
        _camSound.Play();
    }

    public void TurnOff()
    {
        _camLight.enabled = false;
        _camSound.clip = switchSound;
        _camSound.Play();
    }

    IEnumerator PlayRotateSound()
    {
        _isPlaying = true;
        _camSound.clip = rotateSound[0];
        _camSound.Play();

        while (_camSound.isPlaying)
            yield return null;

        _camSound.clip = rotateSound[1];
        _camSound.loop = true;
        _camSound.Play();

        while (Input.GetAxisRaw("Horizontal") != 0)
            yield return null;

        _camSound.clip = rotateSound[2];
        _camSound.loop = false;
        _camSound.Play();
        _isPlaying = false;

    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {
            GameManager.Instance.haim.canHack = true;
            GameManager.Instance.haim.securityCamera = this;
            GameManager.Instance.haim.AddInteraction(this);
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {
            GameManager.Instance.haim.canHack = false;
            GameManager.Instance.haim.securityCamera = null;
            GameManager.Instance.haim.RemoveInteraction(this);
        }
    }

    public void Interact()
    {
        GameManager.Instance.haim.HackCam();
    }
}
