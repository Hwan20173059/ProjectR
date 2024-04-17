using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MonsterGroupData
{
    public int id;
    public string groupName;
    public int[] spawnMonsters;
    public int[] randomSpawnMonsters;
    public int randomSpawnMinAmount;
    public int randomSpawnMaxAmount;
    public int rewardGold;
    public int rewardExp;
    public int rewardItemId;
}