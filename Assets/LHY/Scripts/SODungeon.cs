using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SODungeon : ScriptableObject
{
    public int stage;
    public int currentStage;

    public GameObject[] spawnMonster;

    public int dungeonClearExp;
    public int dungeonClearGold;
}
