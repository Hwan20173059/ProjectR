using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MonsterData", menuName = "Scriptable Object/MonsterData")]
public class MonsterSO : ScriptableObject
{
    public int hp;
    public int atk;
    public int exp;
    public float actionCoolTime;
}
