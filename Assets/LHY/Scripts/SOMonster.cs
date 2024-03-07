using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SOMonster : ScriptableObject
{
    public Sprite sprite;
    public string name;

    public int maxHp;
    public int currentHp;
    public int attack;

    public int rewardExp;
}
