using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DungeonData", menuName = "Scriptable Object/DungeonData")]
public class DungeonSO : ScriptableObject
{
    public string dungeonName;
    public List<StageSO> stages;
}
