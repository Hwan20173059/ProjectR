using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteInventory : Inventory
{
    [SerializeField] GameObject itemSlotPrefab;
    [SerializeField] int maxSlots;
    public int MaxSlots
    {
        get { return maxSlots; }
        set { SetMaxSlots(value); }
    }

    protected override void Start()
    {
        SetMaxSlots(maxSlots);
        base.Start();
    }

    private void SetMaxSlots(int value)
    {
        if(value <= 0)
            maxSlots = 1;
        else
            maxSlots = value;

        if (maxSlots <= slots.Count)
        {
            DestroyEmptySlot();
        }
        else if (maxSlots > slots.Count)
        {
            AddNewSlot();
        }
    }

    private void AddNewSlot()
    {
        int diff = maxSlots - slots.Count;

        for (int i = 0; i < diff; i++)
        {
            GameObject itemSlotGameObj = Instantiate(itemSlotPrefab);
            itemSlotGameObj.transform.SetParent(slotParent, worldPositionStays: false);
            slots.Add(itemSlotGameObj.GetComponentInChildren<EquipSlot>());
        }
    }

    private void DestroyEmptySlot()
    {
        for (int i = maxSlots; i < slots.Count; i++)
        {
            Destroy(slots[i].transform.parent.gameObject);
        }
        int diff = slots.Count - maxSlots;
        slots.RemoveRange(maxSlots, diff);
    }
}
