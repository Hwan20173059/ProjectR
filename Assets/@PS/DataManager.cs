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
    public CharacterDatabase characterDatabase;
    public MonsterDatabase monsterDatabase;
    public StageDatabase stageDatabase;
    public DungeonDatabase dungeonDatabase;

    new private void Awake()
    {
        LoadSaveData();
        LoadEquipDatas();

        LoadDatabase(ref characterDatabase);
        LoadDatabase(ref monsterDatabase);
        LoadDatabase(ref stageDatabase);
        LoadDatabase(ref dungeonDatabase);
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

    public void LoadDatabase<T>(ref T database) where T : IDatabase
    {
        TextAsset jsonFile = Resources.Load<TextAsset>($"{typeof(T).Name}");

        if (jsonFile != null)
        {
            string json = jsonFile.text;
            database = JsonUtility.FromJson<T>(json);
            database.Initialize();
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
        else
        {
            Debug.Log("첫 플레이");
            return;
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
