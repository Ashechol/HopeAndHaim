using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, ICanInteract
{
    public enum ItemType { Normal, Buffer, Key }
    AudioSource _interactSound;
    bool _canInteract;
    bool _showing;
    bool _hasAudioSource;

    [Header("Settings")]
    public Sprite information;
    public AudioClip dialog;
    public ItemType itemType;
    public float buffMutiplier;

    void Awake()
    {
        _interactSound = GetComponent<AudioSource>();
    }


    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {
            GameManager.Instance.haim.AddInteraction(this);
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {
            GameManager.Instance.haim.RemoveInteraction(this);
        }
    }

    public void Interact()
    {

        _interactSound.Play();
        if (!_showing)
        {
            UIManager.Instance.ShowInformation(information);
            GameManager.Instance.gameMode = GameManager.GameMode.DialogueMoment;
            _showing = true;
        }
        else
        {
            UIManager.Instance.CloseInformation();
            GameManager.Instance.gameMode = GameManager.GameMode.Normal;
            _showing = false;

            if (dialog != null)
                GameManager.Instance.dialog.PlayDiaglog(dialog);

        }

    }
}
