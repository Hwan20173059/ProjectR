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
    public Type type;
    public Equip data;
    public ValueChangeType singleChangeType;
    public ValueChangeType doubleChangeType;
    public ValueChangeType tripleChangeType;

    public EquipItem(Equip data)
    {
        this.data = data;
        equipSprite = Resources.Load(data.spritePath, typeof(Sprite)) as Sprite;
        type = (Type)Enum.Parse(typeof(Type), data.type);
        singleChangeType = (ValueChangeType)Enum.Parse(typeof(ValueChangeType), data.singleChangeType);
        doubleChangeType = (ValueChangeType)Enum.Parse(typeof(ValueChangeType), data.doubleChangeType);
        tripleChangeType = (ValueChangeType)Enum.Parse(typeof(ValueChangeType), data.tripleChangeType);
    }
}

public class ItemManager : MonoBehaviour
{
    public EquipItem baseItem;
    public List<EquipItem> eInventory;


    public void Init()
    {
        DataManager.Instance.Init();
        baseItem = new EquipItem(DataManager.Instance.itemDatabase.GetItemByKey(0));
        //baseItem.data = baseEquip;
    }
}
