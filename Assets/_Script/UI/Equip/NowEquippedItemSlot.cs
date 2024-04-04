using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class NowEquippedItemSlot : MonoBehaviour
{
    private PlayerManager playerManager;
    [SerializeField] private TownUiManager townUiManager;
    [SerializeField] private ItemManager itemManager;

    [SerializeField] protected Transform nowEquipParent; 
    [SerializeField] protected List<EquipSlot> nowEquipSlots;

    private void OnValidate()
    {
        nowEquipParent.GetComponentsInChildren<EquipSlot>(includeInactive: true, result: nowEquipSlots);
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
        if (townUiManager.detailArea.isEquipping)
        {
            playerManager.equip[n].isEquipped = false;
            playerManager.equip[n] = townUiManager.lastSelectedEquip;
            playerManager.equip[n].isEquipped = true;
            //playerManager.EquipNewItem(n);

            townUiManager.detailArea.isEquipping = false;
            townUiManager.detailArea.UnActiveEquippingState();
            townUiManager.FreshAfterEquip();
        }
    }

    public void UnEquipSelectedItem()
    {
        for (int i = 0; i < 3; i++)
        {
            if (playerManager.equip[i] == townUiManager.nowSelectedEquip)
            {
                playerManager.equip[i].isEquipped = false;
                playerManager.equip[i] = itemManager.baseItem;
                break;
            }
        }
        townUiManager.FreshAfterEquip();
    }
}
