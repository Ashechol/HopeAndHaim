using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 禁闭区
/// </summary>
public class FootstepArea : MonoBehaviour
{
    public AudioClip footClip;

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player")) {
            AudioManager.Instance.SetFootstep(footClip);
        }
    }
}
