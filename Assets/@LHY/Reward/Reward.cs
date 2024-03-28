using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Reward
{
    public ItemType itemType;
    public string itemID;
    [Range(0, 100)]
    public float itemProbability;
}

public enum ItemType
{
    Equip,
    Consume
}
