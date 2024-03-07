using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class NowEquippedItemSlot : MonoBehaviour
{
    [SerializeField] private PlayerState _playerState;
    [SerializeField] protected Transform nowEquipParent; 
    [SerializeField] protected List<EquipSlot> nowEquipSlots;

    private void OnValidate()
    {
        nowEquipParent.GetComponentsInChildren<EquipSlot>(includeInactive: true, result: nowEquipSlots);
    }
    private void Start()
    {
        FreshEquippedSlot();
    }

    public void FreshEquippedSlot() // reload slots & show items
    {
        int i = 0;
        for (; i < _playerState.equip.Count && i < nowEquipSlots.Count; i++)
        {
            nowEquipSlots[i].item = _playerState.equip[i];
        }
        for (; i < nowEquipSlots.Count; i++)
        {
            nowEquipSlots[i].item = null;
        }
    }
}
