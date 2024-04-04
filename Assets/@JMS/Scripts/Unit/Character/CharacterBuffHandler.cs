using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBuffHandler
{
    public int atkBuff;
    public int atkDuration { get { return atkDuration; } set { atkDuration = value; if (atkDuration <= 0) atkBuff = 0; } }

    public int speedBuff;
    public int speedDuration { get { return speedDuration; } set { speedDuration = value; if (speedDuration <= 0) speedBuff = 0; } }

    public void BuffDurationReduce()
    {
        if (atkDuration > 0) { atkDuration--; }
        if (speedDuration > 0) { speedDuration--; }
    }
}