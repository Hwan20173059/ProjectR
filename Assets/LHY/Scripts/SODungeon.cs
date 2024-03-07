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
    //������ ���� ������ �����Ŵ����� �޾ƿͼ� ������ ���� ���ð� ������ ��.
}
