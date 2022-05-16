using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    // Haim one of the main actors
    public Haim haim;

    public void RegisterHaim(Haim haim)
    {
        this.haim = haim;
    }
}
