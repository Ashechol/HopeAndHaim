using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    Collider2D _coll;

    void Update()
    {
        if (_coll != null && _coll.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            GameManager.Instance.gameMode = GameManager.GameMode.Gameplay;
            UIManager.Instance.lastQuestionPanel.SetActive(false);

            Vector2 walkBack = new Vector2(-4.5f, 28.5f);
            GameManager.Instance.haim.SetDestination(walkBack);
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        _coll = coll;

        if (_coll.CompareTag("Player"))
        {
            GameManager.Instance.gameMode = GameManager.GameMode.Information;
            UIManager.Instance.lastQuestionPanel.SetActive(true);
        }
    }

}
