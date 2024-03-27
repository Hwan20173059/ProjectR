using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class PlayerManager : Singleton<PlayerManager>
{
    [Header("Info")]
    public int playerLevel;
    public int needExp;
    public int currentExp;
    public int gold;

    [Header("Equip")]
    public GameObject selectedCharacter;
    public EquipItem[] equip = new EquipItem[3];

    [Header("State")]
    public bool isTown;
    public bool isField;
    public bool isDungeon;

    [Header("Town")]
    public TownUiManager townUiManager;
    public GameObject townPlayer;    

    [Header("SaveInfo")]
    public int fieldX;
    public int fieldY;

    public List<int> monsterPosition = new List<int>();
    public List<int> chestPosition = new List<int>();
    public int[] dungeonMap = new int[4];

    [Header("Dungeon")]
    public int selectTownID;
    public int selectDungeonID;
    public int selectBattleID;

    private void Start()
    {
        DataManager.Instance.Init();
        EquipItem baseEquip = new EquipItem(DataManager.Instance.itemDatabase.GetItemByKey(0));
        for (int i = 0; i < 3; i++)
        {
            if (equip[i] == null)
                equip[i] = baseEquip;
        }
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
