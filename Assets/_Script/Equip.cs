using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Type
{
    Normal,
    Fire,
    Water,
    Air,
    Ground,
    Darkness,
    Light
}

[CreateAssetMenu(fileName = "items", menuName = "data/Item")]
public class Equip : ScriptableObject
{
    public Sprite itemSprite;
    public string itemName;
    public Type type;
    public int attack;
    public string info;
}
