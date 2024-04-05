using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

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

    private void Start()
    {
        itemDatabase = DataManager.Instance.itemDatabase;
        baseItem = new EquipItem(itemDatabase.GetItemByKey(0));
        LoadEquipData();
        //baseItem.data = baseEquip;
    }

    public void AddEquipItem(int id)
    {
        EquipItem eItem = new EquipItem(itemDatabase.GetItemByKey(id));
        eInventory.Add(eItem);
    }

    public void AddConsumeItem(int id)
    {
        int index = cInventory.FindIndex(c => c.data.id == id);
        if (index != -1)
        {
            cInventory[index].count++;
        }
        else
        {
            ConsumeItem cItem = new ConsumeItem(itemDatabase.GetCItemByKey(id));
            cItem.count = 1;
            cInventory.Add(cItem);
        }
    }

    public void AddConsumeItem(int id, int count)
    {
        ConsumeItem cItem = new ConsumeItem(itemDatabase.GetCItemByKey(id));
        if (cInventory.Contains(cItem))
        {
            cItem.count += count;
        }
        else
        {
            cItem.count = count;
            cInventory.Add(cItem);
        }
    }

    public void ReduceConsumeItem(ConsumeItem consumeItem)
    {
        if (cInventory.Contains(consumeItem))
        {
            if(consumeItem.count > 1)
            {
                consumeItem.count--;
            }
            else
            {
                cInventory.Remove(consumeItem);
            }
        }
    }

    public void LoadEquipData()
    {
        SaveData saveData = DataManager.Instance.saveData;

        if (saveData != null)
        {
            string[] equipitemListID = saveData.equipitemListID.Split(" ");

            for (int i = 0; i < equipitemListID.Length - 1; i++)
                AddEquipItem(int.Parse(equipitemListID[i]));


            string[] itemListID = saveData.itemListID.Split(" ");
            string[] itemListCount = saveData.itemListCount.Split(" ");

            for (int i = 0; i < itemListID.Length - 1; i++)
                AddConsumeItem(int.Parse(itemListID[i]), int.Parse(itemListCount[i]));
        }
        else
        {

        }
    }
}
