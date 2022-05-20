using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    void OnTriggerEnter2d(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {

        }
    }
}
