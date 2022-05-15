using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T instance;
    public static T Instance { get { return instance; } }
    public static bool IsInit { get { return instance != null; } }

    protected virtual void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this as T;
    }

    protected void OnDestory()
    {
        if (instance == this)
        {
            instance = null;
        }
    }

}
