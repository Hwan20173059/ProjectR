using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SOMonster : ScriptableObject
{
    public enum MonsterType
    {
        Fire,
        Water,
        Wind,
        Dark,
        Light,
        Ground
    }
    public MonsterType monsterType;
    public int hp;
    public int attack;
    public int exp;
    public GameObject monster;
}
