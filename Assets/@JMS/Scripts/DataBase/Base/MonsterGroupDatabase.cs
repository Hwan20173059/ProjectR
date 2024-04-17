using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGroupDatabase : IDatabase
{
    public List<MonsterGroupData> monsterGroupDatas;
    public Dictionary<int, MonsterGroupData> stageDic = new();

    public void Initialize()
    {
        foreach (MonsterGroupData data in monsterGroupDatas)
        {
            stageDic.Add(data.id, data);
        }
    }

    public MonsterGroupData GetDataByKey(int id)
    {
        if (stageDic.ContainsKey(id))
            return stageDic[id];

        return null;
    }
}
