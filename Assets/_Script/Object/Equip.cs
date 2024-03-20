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

public enum ValueChangeType
{
    ADD,
    MUL,
}

[System.Serializable]
public class Equip
{
    public int id;
    public string spritePath;
    public string equipName;
    public string type;
    public int singleValue;
    public string singleChangeType;
    public int doubleValue;
    public string doubleChangeType;
    public int tripleValue;
    public string tripleChangeType;
    public string info;
}