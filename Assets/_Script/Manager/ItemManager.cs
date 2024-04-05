using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class EquipItem
{
    public bool isEquipped;
    public Sprite equipSprite;
    public Equip data;
    public ValueChangeType singleChangeType;
    public ValueChangeType doubleChangeType;
    public ValueChangeType tripleChangeType;

    public EquipItem(Equip data)
    {
        this.data = data;
        equipSprite = Resources.Load(data.spritePath, typeof(Sprite)) as Sprite;
        singleChangeType = (ValueChangeType)Enum.Parse(typeof(ValueChangeType), data.singleChangeType);
        doubleChangeType = (ValueChangeType)Enum.Parse(typeof(ValueChangeType), data.doubleChangeType);
        tripleChangeType = (ValueChangeType)Enum.Parse(typeof(ValueChangeType), data.tripleChangeType);
    }
}

[System.Serializable]
public class ConsumeItem
{
    public int count;
    public Sprite consumeSprite;
    public Type type;
    public Consume data;

    public ConsumeItem(Consume data)
    {
        this.data = data;
        consumeSprite = Resources.Load(data.spritePath, typeof(Sprite)) as Sprite;
        type = (Type)Enum.Parse(typeof(Type), data.type);
    }
}

public class ItemManager : Singleton<ItemManager>
{
    private ItemDatabase itemDatabase;

    public EquipItem baseItem;
    public List<EquipItem> eInventory = new List<EquipItem>();
    public List<ConsumeItem> cInventory = new List<ConsumeItem>();

    public void Init()
    {
        DataManager.Instance.Init();
        itemDatabase = DataManager.Instance.itemDatabase;
        baseItem = new EquipItem(itemDatabase.GetItemByKey(0));
        //baseItem.data = baseEquip;
    }

    public void AddEquipItem(int id)
    {
        EquipItem eItem = new EquipItem(itemDatabase.GetItemByKey(id));
        eInventory.Add(eItem);
    }
    public void AddConsumeItem(int id)
    {
        ConsumeItem cItem = new ConsumeItem(itemDatabase.GetCItemByKey(id));
        if (cInventory.Contains(cItem))
        {
            cItem.count++;
        }
        else
        {
            cItem.count = 1;
            cInventory.Add(cItem);
        }
    }

    public void ReduceConsumeItem(int id)
    {
        ConsumeItem cItem = new ConsumeItem(itemDatabase.GetCItemByKey(id));
        if (cInventory.Contains(cItem))
        {
            if(cItem.count > 1)
            {
                cItem.count--;
            }
            else
            {
                cInventory.Remove(cItem);
            }
        }
    }
}
