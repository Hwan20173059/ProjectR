using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class NowEquippedItemSlot : MonoBehaviour
{
    private PlayerManager playerManager;
    [SerializeField] private DetailArea detailArea;
    [SerializeField] private Inventory inventory;
    private ItemManager itemManager;

    [SerializeField] protected Transform nowEquipParent; 
    [SerializeField] protected List<EquipSlot> nowEquipSlots;

    private void OnValidate()
    {
        nowEquipParent.GetComponentsInChildren<EquipSlot>(includeInactive: true, result: nowEquipSlots);
    }

    private void Awake()
    {
        itemManager = ItemManager.Instance;
    }
    private void Start()
    {
        playerManager = PlayerManager.Instance.GetComponent<PlayerManager>();
        Invoke("FreshEquippedSlot", 0.1f);
    }

    public void FreshEquippedSlot() // reload slots & show items
    {
        for (int i = 0; i < 3; i++)
        {
            nowEquipSlots[i].item = playerManager.equip[i];
        }
    }

    public void ChangeSelectedEquip(int n)
    {
        if (detailArea.isEquipping)
        {
            playerManager.EquipNewItem(n);

            detailArea.isEquipping = false;
            detailArea.UnActiveEquippingState();
            inventory.FreshAfterEquip();

            AudioManager.Instance.PlayEquipSFX();
        }
    }

    public void UnEquipSelectedItem()
    {
        for (int i = 0; i < 3; i++)
        {
            if (playerManager.equip[i] == detailArea.nowSelectedEquip)
            {
                playerManager.equip[i].isEquipped = false;
                playerManager.equip[i] = itemManager.baseItem;

                AudioManager.Instance.PlayUnEquipSFX();
                break;
            }
        }
        inventory.FreshAfterEquip();
    }
}
