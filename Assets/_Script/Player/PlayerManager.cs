using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class PlayerManager : Singleton<PlayerManager>
{
    public ItemManager itemManager;

    public TownUiManager townUiManager;
    public GameObject townPlayer;

    public Character selectedCharacter;
    public EquipItem[] equip = new EquipItem[3];

    public int selectDungeonID;


    private void Start()
    {
        //for (int i = 0; i < 3; i++)
        //{
        //    equip[i] = itemManager.baseItem;
        //}
        //ReFreshPlayer();
    }

    public void ReFreshPlayer()
    {
        townPlayer.GetComponent<TownPlayer>().Refresh();
    }

    public void EquipNewItem(int n)
    {
        if(townUiManager.detailArea.isEquipping)
        {
            equip[n].isEquipped = false;
            equip[n] = townUiManager.lastSelectedEquip;
            equip[n].isEquipped = true;

            townUiManager.detailArea.isEquipping = false;
            townUiManager.detailArea.UnActiveEquippingState();
            townUiManager.FreshAfterEquip();
        }
    }

    public void UnEquipItem()
    {
        for(int i = 0; i < 3; i++)
        {
            if(equip[i] == townUiManager.nowSelectedEquip)
            {
                equip[i].isEquipped = false;
                equip[i] = itemManager.baseItem;
                break;
            }
        }
        townUiManager.FreshAfterEquip();
    }
}
