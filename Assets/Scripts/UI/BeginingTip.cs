using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginingTip : MonoBehaviour
{
    public GameObject tip1;
    public GameObject tip2;

    public float tip1Timer = 6;
    public float tip2Timer = 4;

    public IEnumerator ShowTip()
    {
        yield return StartCoroutine(TimerCountDown(tip1Timer, tip1));

        yield return StartCoroutine(TimerCountDown(tip2Timer, tip2));
    }

    IEnumerator TimerCountDown(float timer, GameObject tip)
    {
        tip.SetActive(true);

        while (timer > 0)
        {
            timer -= Time.deltaTime;
            yield return null;
        }

        tip.SetActive(false);
    }
}
