using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[System.Serializable]
public class EquipItem
{
    public bool isEquipped;
    public Equip data;
}

public class ItemManager : MonoBehaviour
{
    public EquipItem baseItem;
    [SerializeField] private Equip baseEquip;

    public List<EquipItem> eInventory;


    private void Start()
    {
        PlayerManager.Instance.itemManager = this;
        baseItem.data = baseEquip;
    }
}
