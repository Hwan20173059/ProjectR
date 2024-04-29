using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum BuffType
{
    ATK,
    SPD
}

public struct Buff
{
    public BuffType type;
    public string buffName;
    public int value;
    public int turnCount;

    public Buff(BuffType type, string buffName, int value, int turnCount)
    {
        this.buffName = buffName;
        this.type = type;
        this.value = value;
        this.turnCount = turnCount;
    }
}

public class CharacterBuffController
{
    List<List<Buff>> buffs;

    List<Buff> atkBuffs;
    List<Buff> speedBuffs;

    List<BuffIcon> buffIcons;

    BattleManager battleManager;

    public void Init(BattleManager battleManager)
    {
        this.battleManager = battleManager;

        buffs = new List<List<Buff>>();

        buffIcons = new List<BuffIcon>();

        atkBuffs = new List<Buff>();
        speedBuffs = new List<Buff>();

        buffs.Add(atkBuffs);
        buffs.Add(speedBuffs);
    }

    public void AddBuff(BuffType type, string buffName, int value, int turnCount)
    {
        Buff buff = new Buff(type, buffName, value, turnCount);

        switch (type)
        {
            case BuffType.ATK: atkBuffs.Add(buff); break;
            case BuffType.SPD: speedBuffs.Add(buff); break;
        }

        buffIcons.Add(battleManager.battleCanvas.SetBuff(buff));

    }

    public int GetBuffValue(BuffType type)
    {
        switch (type)
        {
            case BuffType.ATK: return GetAtkBuffValue();
            case BuffType.SPD: return GetSpeedBuffValue();
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
                if (buffs[i].turnCount == 1)
                {
                    RemoveBuff(buffs, i);
                    break;
                }
                Buff temp = buffs[i];
                --temp.turnCount;
                buffs[i] = temp;
            }
        }

        for (int i = 0; i < buffIcons.Count; i++)
        {
            if (buffIcons[i].buff.turnCount == 1)
            {
                RemoveBuffIcon(i);
                break;
            }
            else
            {
                --buffIcons[i].buff.turnCount;
            }
        }

        battleManager.battleCanvas.BuffDescriptionPanelOff();
    }

    void RemoveBuff(List<Buff> buffs, int curIdx)
    {
        buffs.Remove(buffs[curIdx]);

        for (int i = curIdx; i < buffs.Count; i++)
        {
            if (buffs[i].turnCount == 1)
            {
                RemoveBuff(buffs, i);
                break;
            }
            Buff temp = buffs[i];
            --temp.turnCount;
            buffs[i] = temp;
        }
    }

    void RemoveBuffIcon(int curIdx)
    {
        buffIcons[curIdx].gameObject.SetActive(false);
        buffIcons.Remove(buffIcons[curIdx]);

        for (int i = curIdx; i < buffIcons.Count; i++)
        {
            if (buffIcons[i].buff.turnCount == 1)
            {
                RemoveBuffIcon(i);
                break;
            }
            else
            {
                --buffIcons[i].buff.turnCount;
            }
        }
    }

}