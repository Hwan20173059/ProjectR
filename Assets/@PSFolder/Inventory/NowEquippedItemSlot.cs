using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class NowEquippedItemSlot : MonoBehaviour
{
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] protected Transform nowEquipParent; 
    [SerializeField] protected List<EquipSlot> nowEquipSlots;

    private void OnValidate()
    {
        nowEquipParent.GetComponentsInChildren<EquipSlot>(includeInactive: true, result: nowEquipSlots);
    }
    private void Start()
    {
        playerManager = PlayerManager.Instance.GetComponent<PlayerManager>();

        FreshEquippedSlot();
    }

    public void FreshEquippedSlot() // reload slots & show items
    {
        int i = 0;
        if (playerManager.equip.Count == 0)
        {
            playerManager.equip.Add(ItemManager.Instance.baseItem);
        }
        for (; i < playerManager.equip.Count && i < nowEquipSlots.Count; i++)
        {
            nowEquipSlots[i].item = playerManager.equip[i];
        }
        for (; i < nowEquipSlots.Count; i++)
        {
            nowEquipSlots[i].item = null;
        }
    }
}
