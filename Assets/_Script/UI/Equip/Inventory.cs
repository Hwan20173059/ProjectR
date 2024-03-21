using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using Unity.VisualScripting;
using UnityEngine;

public class Inventory : MonoBehaviour //Inventory
{
    [SerializeField] private GameObject bag;
    [SerializeField] protected Transform slotParent;
    [SerializeField] protected List<EquipSlot> slots;

    [SerializeField] private GameObject cbag;
    [SerializeField] protected Transform cslotParent;
    [SerializeField] protected List<ConsumeSlot> cslots;

    public ItemManager itemManager;

    private void OnValidate() // change slots if changed by editor
    {
        slotParent.GetComponentsInChildren<EquipSlot>(includeInactive: true, result: slots);
        cslotParent.GetComponentsInChildren<ConsumeSlot>(includeInactive: true, result: cslots);
    }

    protected virtual void Start()
    {
        itemManager.Init();
        for (int i = 1; i <= 4; i++)
        {
            EquipItem eItem = new EquipItem(DataManager.Instance.itemDatabase.GetItemByKey(i));
            itemManager.eInventory.Add(eItem);
        }
        for (int i = 0; i <= 2; i++)
        {
            ConsumeItem cItem = new ConsumeItem(DataManager.Instance.itemDatabase.GetCItemByKey(i));
            itemManager.cInventory.Add(cItem);
        }

        FreshSlot();
        FreshConsumeSlot();
    }

    public void FreshSlot() // reload slots & show items
    {
        int i = 0;
        for (; i < itemManager.eInventory.Count && i < slots.Count; i++)
        {
            slots[i].item = itemManager.eInventory[i];
        }
        for (; i < slots.Count; i++)
        {
            slots[i].item = null;
        }
    }

    public void FreshConsumeSlot() // reload slots & show items
    {
        int i = 0;
        for (; i < itemManager.cInventory.Count && i < cslots.Count; i++)
        {
            cslots[i].item = itemManager.cInventory[i];
        }
        for (; i < cslots.Count; i++)
        {
            cslots[i].item = null;
        }
    }
    // have to add methods that adds item & add max slots
}