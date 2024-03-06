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
public class Equip : MonoBehaviour
{
    public Sprite sprite;
    public string name;
    public Type type;
    public int attack;
    public string info;
}
