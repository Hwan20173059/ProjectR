using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBuffHandler
{
    public int atkBuff;
    public int atkDuration;
    public int speedBuff;
    public int speedDuration;

    public void BuffDurationReduce()
    {
        if (atkDuration > 0)
        {
            --atkDuration;
            if(atkDuration <= 0) { atkBuff = 0; }
        }
        if (speedDuration > 0)
        {
            --speedDuration;
            if (speedDuration <= 0) { speedBuff = 0; }
        }
    }
}