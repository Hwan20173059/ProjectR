using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public Character character;
    public List<EquipItem> equip;

    public Transform playerArea;
    public GameObject characterPrefab;

    public int selectDungeonID;

    private void Start()
    {
        SpawnPlayer();
    }

    public void SpawnPlayer()
    {
        Instantiate(characterPrefab, playerArea);
    }

    public void EquipNewItem()
    {
        int len = equip.Count;
        if (len < 3)
        {
            equip.Add(UIManager.instance.nowSelectedEquip);
            UIManager.instance.nowSelectedEquip.isEquipped = true;
        }
        UIManager.instance.detailArea.ChangeDetailActivation(false);
        UIManager.instance.nEquipItemSlot.FreshEquippedSlot();
    }

    public void UnEquipItem()
    {
        equip.Remove(UIManager.instance.nowSelectedEquip);
        UIManager.instance.nowSelectedEquip.isEquipped = false;
        UIManager.instance.detailArea.ChangeDetailActivation(false);
        UIManager.instance.nEquipItemSlot.FreshEquippedSlot();
    }
}
