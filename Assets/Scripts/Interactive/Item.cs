using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    bool _canInteract;
    bool _showing;

    public Sprite information;

    void Update()
    {
        if (_canInteract)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!_showing)
                {
                    UIManager.Instance.ShowInformation(information);
                    _showing = true;
                }
                else
                {
                    UIManager.Instance.CloseInformation();
                    _showing = false;
                }
            }

        }
    }


    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {
            _canInteract = true;
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {
            _canInteract = false;
        }
    }
}
