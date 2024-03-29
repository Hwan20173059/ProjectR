using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleEvent
{
    public event Action onDungeonClear;
    public void DungeonClear()
    {

        if (onDungeonClear != null)
        {
            onDungeonClear();
        }
    }

    public event Action onKillSlime;
    public void KillSlime()
    {
        if (onKillSlime != null)
        {
            onKillSlime();
        }
    }

}
