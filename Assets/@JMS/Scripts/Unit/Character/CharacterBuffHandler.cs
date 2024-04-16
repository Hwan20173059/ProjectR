using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct Buff
{
    public BuffType type;
    public int value;
    public int duration;

    public Buff(BuffType type, int value, int duration)
    {
        this.type = type;
        this.value = value;
        this.duration = duration;
    }
}

public enum BuffType
{
    Atk,
    Speed
}

public class CharacterBuffHandler
{
    List<List<Buff>> buffs;

    List<Buff> atkBuffs;
    List<Buff> speedBuffs;

    public void Init()
    {
        buffs = new List<List<Buff>>();

        atkBuffs = new List<Buff>();
        speedBuffs = new List<Buff>();

        buffs.Add(atkBuffs);
        buffs.Add(speedBuffs);
    }

    public void AddBuff(BuffType type, int value, int duration)
    {
        switch (type)
        {
            case BuffType.Atk: atkBuffs.Add(new Buff(type, value, duration + 1)); break;
            case BuffType.Speed: speedBuffs.Add(new Buff(type, value, duration + 1)); break;
        }
        
    }

    public int GetBuffValue(BuffType type)
    {
        switch (type)
        {
            case BuffType.Atk: return GetAtkBuffValue();
            case BuffType.Speed: return GetSpeedBuffValue();
        }
        return 0;
    }

    int GetAtkBuffValue()
    {
        if (atkBuffs.Count <= 0)
            return 0;
        int totalValue = 0;
        foreach (Buff buff in atkBuffs)
        {
            totalValue += buff.value;
        }
        return totalValue;
    }

    int GetSpeedBuffValue()
    {
        if (speedBuffs.Count <= 0)
            return 0;
        int totalValue = 0;
        foreach (Buff buff in speedBuffs)
        {
            totalValue += buff.value;
        }
        return totalValue;
    }

    public void ReduceBuffDuration()
    {
        foreach (List<Buff> buffs in buffs)
        {
            for (int i = 0; i < buffs.Count; i++)
            {
                if (buffs[i].duration == 1)
                {
                    RemoveBuff(buffs, i);
                    break;
                }
                Buff temp = buffs[i];
                --temp.duration;
                buffs[i] = temp;
            }
        }
    }

    void RemoveBuff(List<Buff> buffs, int curIdx)
    {
        buffs.Remove(buffs[curIdx]);

        for (int i = curIdx; i < buffs.Count; i++)
        {
            if (buffs[i].duration == 1)
            {
                RemoveBuff(buffs, i);
                break;
            }
            Buff temp = buffs[i];
            --temp.duration;
            buffs[i] = temp;
        }
    }
}