using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemReward
{
    public ItemType ItemType;

    public int itemID;
    [Range(0, 100)]
    public float itemProbability;
}

public enum ItemType
{
    Equip,
    Consume,
}