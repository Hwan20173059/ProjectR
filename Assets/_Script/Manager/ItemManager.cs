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

    public EquipItem(Equip data)
    {
        this.data = data;
        equipSprite = Resources.Load(data.spritePath, typeof(Sprite)) as Sprite;
        type = (Type)Enum.Parse(typeof(Type), data.type);
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
