using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    public ItemManager itemManager;

    public TownUiManager townUiManager;
    public GameObject townPlayer;

    public CharacterSO selectedCharacter;
    public List<EquipItem> equip;

    public int selectDungeonID;


    private void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            equip.Add(itemManager.baseItem);
        }

        ReFreshPlayer();
    }

    public void ReFreshPlayer()
    {
        townPlayer.GetComponent<TownPlayer>().Refresh();
    }

    public void EquipNewItem()
    {
        if (equip.Contains(itemManager.baseItem))
        {
            equip.RemoveAt(0);
            equip.Add(townUiManager.nowSelectedEquip);
            townUiManager.nowSelectedEquip.isEquipped = true;
        }
        townUiManager.detailArea.ChangeDetailActivation(false);
        townUiManager.nEquipItemSlot.FreshEquippedSlot();
    }

    public void UnEquipItem()
    {
        equip.Remove(townUiManager.nowSelectedEquip);
        equip.Add(itemManager.baseItem);
        townUiManager.nowSelectedEquip.isEquipped = false;
        townUiManager.detailArea.ChangeDetailActivation(false);
        townUiManager.nEquipItemSlot.FreshEquippedSlot();
    }
}
