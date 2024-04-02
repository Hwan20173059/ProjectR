using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData
{
    public int id;
    public int playerLevel;
    public int needExp;
    public int currentExp;
    public int gold;
    public int selectTownID;
    public string characterListID;
    public string characterListLevel;
    public string characterListExp;
    public string equipitemListID;
    public string itemListID;
    public string itemListCount;
}

public class SaveDataBase
{
    public List<SaveData> saveDatas;
    public Dictionary<int, SaveData> saveDic = new();

    public void Initialize()
    {
        foreach (SaveData saveDataBase in saveDatas)
        {
            saveDic.Add(saveDataBase.id, saveDataBase);
        }
    }

    public SaveData GetSaveByKey(int id)
    {
        if (saveDic.ContainsKey(id))
            return saveDic[id];

        return null;
    }
}
