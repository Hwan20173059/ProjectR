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

    [Header("Reward")]
    public List<Equip> EquipReward = new List<Equip>();
    public List<Item> ItemReward = new List<Item>();
    public int dungeonClearExp;
    //������ ���� ������ �����Ŵ����� �޾ƿͼ� ������ ���� ���ð� ������ ��.
}
