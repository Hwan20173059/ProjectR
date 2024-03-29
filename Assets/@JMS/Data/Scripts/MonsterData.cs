using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MonsterData
{
    public int id;
    public string monsterName;
    public string spritePath;
    public string animatorPath;
    public int hp;
    public int atk;
    public int exp;
    public float actionCoolTime;
    public int[] actions;
}
