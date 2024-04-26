using Assets.PixelFantasy.PixelTileEngine.Scripts;
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

    [SerializeField] protected Transform cslotParent;
    [SerializeField] protected List<ConsumeSlot> cslots;

    public GameObject equipInventoryUI;
    public NowEquippedItemSlot nEquipItemSlot;
    public DetailArea detailArea;
    public GameObject consumeInventoryUI;

    private ItemManager itemManager;

    /*
    private void OnValidate() // change slots if changed by editor
    {
        slotParent.GetComponentsInChildren<EquipSlot>(includeInactive: true, result: slots);
        cslotParent.GetComponentsInChildren<ConsumeSlot>(includeInactive: true, result: cslots);
    }
    */
    protected virtual void Start()
    {
        itemManager = ItemManager.Instance;

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

    public void OpenInventory()
    {
        AudioManager.Instance.PlayUISelectSFX();

        itemManager.SetEquipMaxSlots();
        FreshSlot();
        detailArea.RefreshGoldUI();
        detailArea.ChangeDetailActivation(false);
        equipInventoryUI.SetActive(true);
        detailArea.gameObject.SetActive(true);
    }
    public void CloseInventory()
    {
        AudioManager.Instance.PlayUISelectSFX();

        detailArea.UnActiveEquippingState();
        equipInventoryUI.SetActive(false);
        consumeInventoryUI.SetActive(false);
        detailArea.gameObject.SetActive(false);
    }

    public void OpenConsumeInventory()
    {
        AudioManager.Instance.PlayUISelectSFX();

        itemManager.SetConsumeMaxSlots();
        FreshConsumeSlot();
        detailArea.RefreshGoldUI();
        detailArea.ChangeDetailActivation(false);
        consumeInventoryUI.SetActive(true);
        detailArea.gameObject.SetActive(true);
    }

    public void FreshAfterEquip()
    {
        detailArea.nowSelectedEquip = null;
        detailArea.lastSelectedEquip = null;
        detailArea.ChangeDetailActivation(false);
        nEquipItemSlot.FreshEquippedSlot();
    }

    public void SortEquipInventory()
    {
        itemManager.SortEquips();
        FreshSlot();
    }    
}