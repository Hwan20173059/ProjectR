using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EquipReward
{
    public int itemID;
    [Range(0, 100)]
    public float itemProbability;
}

[System.Serializable]
public class ConsumeReward
{
    public int itemID;
    [Range(0, 100)]
    public float itemProbability;
}

[System.Serializable]
public class Reward
{
    public int gold;
    public int exp;
    public List<int> consumeItemRewardID = new();
}
