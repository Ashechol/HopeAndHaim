using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginingTip : MonoBehaviour
{
    public GameObject tip1;
    public GameObject tip2;
    public GameObject tip3;
    public bool skipTipActivated;

    public float tip1Timer = 6;
    public float tip2Timer = 4;

    void Start()
    {

    }

    public IEnumerator ShowTip()
    {
        tip3.SetActive(false);
        yield return StartCoroutine(TimerCountDown(tip1Timer, tip1));

        yield return StartCoroutine(TimerCountDown(tip2Timer, tip2));
    }

    public void ShowSkipTip(int timer)
    {
        if (!tip3.activeInHierarchy)
            StartCoroutine(TimerCountDown(timer, tip3));
    }

    IEnumerator TimerCountDown(float timer, GameObject tip)
    {
        tip.SetActive(true);
        skipTipActivated = true;

        while (timer > 0)
        {
            timer -= Time.deltaTime;
            yield return null;
        }

        tip.SetActive(false);
        skipTipActivated = false;
    }
}
