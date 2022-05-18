using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    List<IGameObserver> _observers;

    public Haim haim;

    public Vector3 PlayerPosition { get { return haim.transform.position; } }

    public void RegisterHaim(Haim haim)
    {
        this.haim = haim;
    }

    public void AddObserver(IGameObserver observer)
    {
        _observers.Add(observer);
    }

    public void RemoveObserver(IGameObserver observer)
    {
        _observers.Remove(observer);
    }
}
