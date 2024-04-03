using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveDataBase
{
    public List<SaveData> saveDatas;
    public Dictionary<int, SaveData> saveDic = new();

    public void Initialize()
    {
        foreach (SaveData saveData in saveDatas)
        {
            saveDic.Add(saveData.id, saveData);
        }
    }

    public SaveData GetSaveByKey(int id)
    {
        if (saveDic.ContainsKey(id))
            return saveDic[id];

        return null;
    }
}
