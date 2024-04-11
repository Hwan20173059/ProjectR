using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonDatabase : IDatabase
{
    public List<DungeonData> dungeonDatas;
    public Dictionary<int, DungeonData> dungeonDic = new();

    public void Initialize()
    {
        foreach (DungeonData data in dungeonDatas)
        {
            dungeonDic.Add(data.id, data);
        }
    }

    public DungeonData GetDataByKey(int id)
    {
        if (dungeonDic.ContainsKey(id))
            return dungeonDic[id];

        return null;
    }
}
