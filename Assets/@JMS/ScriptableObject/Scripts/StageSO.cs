using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StageData", menuName = "Scriptable Object/StageData")]
public class StageSO : ScriptableObject
{
    public string stageName;
    public List<GameObject> SpawnMonsters;
    public List<GameObject> RandomSpawnMonsters;
    public int randomSpawnMinAmount;
    public int randomSpawnMaxAmount;
}
