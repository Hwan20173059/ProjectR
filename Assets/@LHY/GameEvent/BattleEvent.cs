using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleEvent
{
    public event Action<int> onDungeonClear;
    public void DungeonClear(int id)
    {

        if (onDungeonClear != null)
        {
            onDungeonClear(id);
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

    public event Action<int> onKillMonster;
    public void KillMonster(int id)
    {
        if (onKillMonster != null)
        {
            onKillMonster(id);
        }
    }
}
