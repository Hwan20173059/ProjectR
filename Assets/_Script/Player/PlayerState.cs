using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerState : Singleton<PlayerState>
{
    public Character character;
    public List<EquipItem> equip;

    public Transform playerArea;
    public GameObject playerPrefab;

    public int selectDungeonID;


    private void Start()
    {
        ReFreshPlayer();
    }

    public void ReFreshPlayer()
    {
        playerPrefab.GetComponent<Player>().Refresh();
    }

    public void SpawnPlayer()
    {
        Instantiate(playerPrefab, playerArea);
    }

    public void EquipNewItem()
    {
        int len = equip.Count;
        if (len == 1 && equip[0] == ItemManager.Instance.baseItem)
        {
            equip.RemoveAt(0);
        }
        if (len < 3)
        {
            equip.Add(UIManager.Instance.nowSelectedEquip);
            UIManager.Instance.nowSelectedEquip.isEquipped = true;
        }
        UIManager.Instance.detailArea.ChangeDetailActivation(false);
        UIManager.Instance.nEquipItemSlot.FreshEquippedSlot();
    }

    public void UnEquipItem()
    {
        if(equip.Count == 1)
        {
            equip.Add(ItemManager.Instance.baseItem);
        }
        equip.Remove(UIManager.Instance.nowSelectedEquip);
        UIManager.Instance.nowSelectedEquip.isEquipped = false;
        UIManager.Instance.detailArea.ChangeDetailActivation(false);
        UIManager.Instance.nEquipItemSlot.FreshEquippedSlot();
    }
}
