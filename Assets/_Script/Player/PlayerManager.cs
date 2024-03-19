using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class PlayerManager : Singleton<PlayerManager>
{
    public TownUiManager townUiManager;
    public GameObject townPlayer;

    public GameObject selectedCharacter;
    public EquipItem[] equip = new EquipItem[3];

    public int fieldX;
    public int fieldY;

    public int selectDungeonID;


    private void Start()
    {
        DataManager.Instance.Init();
        EquipItem baseEquip = new EquipItem(DataManager.Instance.itemDatabase.GetItemByKey(0));
        for (int i = 0; i < 3; i++)
        {
            if (equip[i] == null)
                equip[i] = baseEquip;
        }

        ReFreshPlayer();
    }

    public void ReFreshPlayer()
    {
        townPlayer.GetComponent<TownPlayer>().Refresh();
    }

    public void EquipNewItem(int n)
    {
        equip[n].isEquipped = false;
        equip[n] = townUiManager.lastSelectedEquip;
        equip[n].isEquipped = true;
    }
}
