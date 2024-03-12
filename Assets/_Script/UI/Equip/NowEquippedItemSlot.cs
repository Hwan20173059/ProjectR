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
        Invoke("FreshEquippedSlot", 0.1f);
    }

    public void FreshEquippedSlot() // reload slots & show items
    {
        for (int i = 0; i < 3; i++)
        {
            nowEquipSlots[i].item = playerManager.equip[i];
        }
    }
}
