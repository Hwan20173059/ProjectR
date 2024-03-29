using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StageData
{
    public int id;
    public string stageName;
    public int[] spawnMonsters;
    public int[] randomSpawnMonsters;
    public int randomSpawnMinAmount;
    public int randomSpawnMaxAmount;
}
