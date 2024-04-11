using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageDatabase : IDatabase
{
    public List<StageData> stageDatas;
    public Dictionary<int, StageData> stageDic = new();

    public void Initialize()
    {
        foreach (StageData data in stageDatas)
        {
            stageDic.Add(data.id, data);
        }
    }

    public StageData GetDataByKey(int id)
    {
        if (stageDic.ContainsKey(id))
            return stageDic[id];

        return null;
    }
}
