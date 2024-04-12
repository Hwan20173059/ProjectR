using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Type
{
    AttackBuffPotion,
    SpeedBuffPotion,
    HpPotion,
    Drop,
    DungeonItem,
    CharacterPiece,
    ExpItem
}

[System.Serializable]
public class Consume
{
    public int id;
    public string spritePath;
    public string consumeName;
    public string type;
    public int value;
    public int turnCount;
    public string info;
    public int price;
}