using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleDatabase : IDatabase
{
    public List<BattleData> battleDatas;
    public Dictionary<int, BattleData> dungeonDic = new();

    public void Initialize()
    {
        foreach (BattleData data in battleDatas)
        {
            dungeonDic.Add(data.id, data);
        }
    }

    public BattleData GetDataByKey(int id)
    {
        if (dungeonDic.ContainsKey(id))
            return dungeonDic[id];

        return null;
    }
}
