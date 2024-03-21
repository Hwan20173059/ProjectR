using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Type
{
    Potion,
    Drop
}

[System.Serializable]
public class Consume
{
    public int id;
    public string spritePath;
    public string consumeName;
    public string type;
    public int value;
    public string info;
    public int price;
}