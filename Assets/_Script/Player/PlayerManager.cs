using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    public Character character;
    public List<EquipItem> equip;

    public Transform playerArea;
    public GameObject playerPrefab;

    public int selectDungeonID;


    private void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            equip.Add(ItemManager.Instance.baseItem);
        }
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
        if (equip.Contains(ItemManager.Instance.baseItem))
        {
            equip.RemoveAt(0);
            equip.Add(UIManager.Instance.nowSelectedEquip);
            UIManager.Instance.nowSelectedEquip.isEquipped = true;
        }
        UIManager.Instance.detailArea.ChangeDetailActivation(false);
        UIManager.Instance.nEquipItemSlot.FreshEquippedSlot();
    }

    public void UnEquipItem()
    {
        equip.Remove(UIManager.Instance.nowSelectedEquip);
        equip.Add(ItemManager.Instance.baseItem);
        UIManager.Instance.nowSelectedEquip.isEquipped = false;
        UIManager.Instance.detailArea.ChangeDetailActivation(false);
        UIManager.Instance.nEquipItemSlot.FreshEquippedSlot();
    }
}
