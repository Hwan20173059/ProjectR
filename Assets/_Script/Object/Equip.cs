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

[CreateAssetMenu(fileName = "EquipData", menuName = "Scriptable Object/equip Data")]
public class Equip : ScriptableObject
{
    public Sprite sprite;
    public string equipName;
    public Type type;
    public int attack;
    public string info;
}
