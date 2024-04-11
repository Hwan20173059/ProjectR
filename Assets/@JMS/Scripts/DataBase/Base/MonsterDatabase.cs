using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDatabase : IDatabase
{
    public List<MonsterData> monsterDatas;
    public Dictionary<int, MonsterData> monsterDic = new();

    public void Initialize()
    {
        foreach (MonsterData data in monsterDatas)
        {
            monsterDic.Add(data.id, data);
        }
    }

    public MonsterData GetDataByKey(int id)
    {
        if (monsterDic.ContainsKey(id))
            return monsterDic[id];

        return null;
    }
}