using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Scriptable Object/character Data")]
public class Character : ScriptableObject
{
    [Header("Info")]
    public Sprite sprite;
    public string name;

    [Header("Level")]
    public int level;
    public int needExp;
    public int currentExp;

    [Header("State")]
    public int maxHp;
    public int currentHp;
    public int attack;
}
