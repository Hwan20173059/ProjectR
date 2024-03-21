using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    public ItemDatabase itemDatabase;
    public CharacterDatabase characterDatabase;

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
    public void LoadCharacterDatas()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("CharacterInfo");

        if (jsonFile != null)
        {
            string json = jsonFile.text;
            characterDatabase = JsonUtility.FromJson<CharacterDatabase>(json);
            characterDatabase.Initialize();
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
