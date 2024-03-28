using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    public ItemDatabase itemDatabase;
    public BattleDataBase battleDatabase;

    private void Start()
    {
        LoadBattleDatas();
    }

    public void Init()
    {
        LoadEquipDatas();
    }

    // �����͸� �ҷ��ͼ� ��ųʸ��� ���� �����ϴ� �޼ҵ�
    public void LoadEquipDatas()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("ItemInfo");

        if (jsonFile != null)
        {
            string json = jsonFile.text;
            itemDatabase = JsonUtility.FromJson<ItemDatabase>(json);
            itemDatabase.Initialize();
        }
    }
    public void LoadBattleDatas()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("BattleDatas");

        if (jsonFile != null)
        {
            string json = jsonFile.text;
            battleDatabase = JsonUtility.FromJson<BattleDataBase>(json);
            battleDatabase.Initialize();
        }
    }

    /*
    public Equip GetEquipData(int id)
    {
        return this.dicEquips[id];
    }

    public Dictionary<int, Equip> GetEquipDatas()
    {
        return this.dicEquips;
    }*/
}
