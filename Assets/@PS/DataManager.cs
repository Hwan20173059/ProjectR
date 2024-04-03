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
    public SaveData saveData;

    new private void Awake()
    {
        LoadBattleDatas();
        LoadSaveData();
    }

    public void Init()
    {
        LoadEquipDatas();
    }

    // 데이터를 불러와서 딕셔너리에 값을 저장하는 메소드
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

    public void LoadSaveData()
    {
        string filePath = Application.persistentDataPath + "/SaveDatas.json";

        if (File.Exists(filePath))
        {
            string FromJsonData = File.ReadAllText(filePath);
            saveData = JsonUtility.FromJson<SaveData>(FromJsonData);
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
