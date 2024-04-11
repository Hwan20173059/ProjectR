using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDatabase : IDatabase
{
    public List<CharacterData> characterDatas;
    public Dictionary<int, CharacterData> characterDic = new();

    public void Initialize()
    {
        foreach (CharacterData data in characterDatas)
        {
            characterDic.Add(data.id, data);
        }
    }

    public CharacterData GetDataByKey(int id)
    {
        if (characterDic.ContainsKey(id))
            return characterDic[id];

        return null;
    }
}
