using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteInventory : Inventory
{
    [SerializeField] TownUiManager townUiManager;
    [SerializeField] GameObject itemSlotPrefab;
    [SerializeField] GameObject consumeSlotPrefab;
    [SerializeField] int maxSlots;
    [SerializeField] int cmaxSlots;
    public int MaxSlots
    {
        get { return maxSlots; }
        set { SetMaxSlots(value); }
    }

    public int cMaxSlots
    {
        get { return cmaxSlots; }
        set { SetConsumeMaxSlots(value); }
    }

    protected override void Start()
    {
        SetMaxSlots(maxSlots);
        SetConsumeMaxSlots(cmaxSlots);
        base.Start();
    }

    private void SetMaxSlots(int value)
    {
        if (value <= 24)
            maxSlots = 24;
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
            slots.Add(itemSlotGameObj.GetComponent<EquipSlot>());
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

    private void SetConsumeMaxSlots(int value)
    {
        if (value <= 36)
            maxSlots = 36;
        else
            maxSlots = value;

        if (maxSlots <= slots.Count)
        {
            DestroyEmptySlotC();
        }
        else if (maxSlots > slots.Count)
        {
            AddNewSlotC();
        }
    }

    private void DestroyEmptySlotC()
    {
        for (int i = maxSlots; i < cslots.Count; i++)
        {
            Destroy(cslots[i].transform.parent.gameObject);
        }
        int diff = cslots.Count - cmaxSlots;
        cslots.RemoveRange(cmaxSlots, diff);
    }

    private void AddNewSlotC()
    {
        int diff = maxSlots - cslots.Count;

        for (int i = 0; i < diff; i++)
        {
            GameObject itemSlotGameObj = Instantiate(consumeSlotPrefab);
            itemSlotGameObj.transform.SetParent(cslotParent, worldPositionStays: false);
            itemSlotGameObj.GetComponent<ConsumeSlot>().townUiManager = townUiManager;
            cslots.Add(itemSlotGameObj.GetComponent<ConsumeSlot>());
        }
    }
}
