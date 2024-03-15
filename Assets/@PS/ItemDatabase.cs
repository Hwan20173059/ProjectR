using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemDatabase
{
    public List<Equip> equipDatas;
    public Dictionary<int, Equip> itemDic = new();

    public void Initialize()
    {
        foreach (Equip item in equipDatas)
        {
            itemDic.Add(item.id, item);
        }
    }

    public Equip GetItemByKey(int id)
    {
        if (itemDic.ContainsKey(id))
            return itemDic[id];

        return null;
    }
}
