using System;
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

[System.Serializable]
public class Equip
{
    public int id;
    public string spritePath;
    public string equipName;
    public string type;
    public int attack;
    public string info;
}