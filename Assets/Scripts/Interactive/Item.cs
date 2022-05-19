using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, ICanInteract
{
    bool _canInteract;
    bool _showing;

    public Sprite information;

    void Update()
    {

    }


    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {
            _canInteract = true;
            GameManager.Instance.haim.AddInteraction(this);
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {
            _canInteract = false;
            GameManager.Instance.haim.RemoveInteraction(this);
        }
    }

    public void Interact()
    {
        if (_canInteract)
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
