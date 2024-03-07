using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SODungeon : ScriptableObject
{
    public enum DungeonType
    {
        normal,
        boss
    }
    public DungeonType dungeonType;
    public List<SOStage> stage = new List<SOStage>();
    public int dungeonClearExp;
    //던전에 관한 정보를 던전매니저에 받아와서 던전에 관한 세팅과 진행을 함.
}
