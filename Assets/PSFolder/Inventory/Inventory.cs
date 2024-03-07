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

    private void OnValidate() // change slots if changed by editor
    {
        slotParent.GetComponentsInChildren<EquipSlot>(includeInactive: true, result: slots);
    }

    protected virtual void Start() 
    {
        FreshSlot();
    }

    public void FreshSlot() // reload slots & show items
    {
        int i = 0;
        for(; i < GameManager.instance.eInventory.Count && i < slots.Count; i++)
        {
            slots[i].item = GameManager.instance.eInventory[i];
        }
        for(; i < slots.Count; i++)
        {
            slots[i].item = null;
        }
    }
    // have to add methods that adds item & add max slots
}