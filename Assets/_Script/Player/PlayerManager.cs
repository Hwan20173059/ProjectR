using Assets.PixelFantasy.PixelTileEngine.Scripts;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class PlayerManager : Singleton<PlayerManager>
{
    [Header("Info")]
    public int playerLevel = 1;
    public int needExp = 30;
    public int currentExp = 10;
    public int gold = 1000;
    public int playerTurnIndex = 5;

    [Header("Character")]
    public List<Character> characterList = new List<Character>();
    public int selectedCharacterIndex = 0;

    [Header("Equip")]
    public EquipItem[] equip = new EquipItem[3];

    [Header("Town")]
    public TownUiManager townUiManager;

    [Header("SaveInfo")]
    public int currentTurnIndex;
    public int fieldX;
    public int fieldY;

    public List<int> monsterPosition = new List<int>();
    public List<int> chestPosition = new List<int>();
    public int[] dungeonMap = new int[4];

    [Header("State")]
    public bool isTown;
    public bool isField;
    public bool isDungeon;

    [Header("IDinfo")]
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

        for (int i = 0; i < DataManager.Instance.battleDatabase.characterDic.Count; i++)
            AddCharacter(i);
    }
    
    public void EquipNewItem(int n)
    {
        equip[n].isEquipped = false;
        equip[n] = townUiManager.lastSelectedEquip;
        equip[n].isEquipped = true;
    }

    public void AddCharacter(int id)
    {
        Character character = Instantiate(townUiManager.characterPrefab,this.transform);
        character.spriteRenderer.color = new Color(1, 1, 1, 0);

        character.LoadInit(DataManager.Instance.battleDatabase.GetCharacterByKey(id));

        characterList.Add(character);
    }

    public void ChangeExp(int change)
    {
        currentExp += change;
        currentExp = currentExp >= needExp ? LevelUp() : currentExp;
    }

    public void AddGold(int changeGold)
    {
        gold += changeGold;
    }
    public int LevelUp()
    {
        //currentExp = currentExp - needExp;
        //playerLevel++;

        //todo : Data¿¡ ÀúÀå
        //playerLevel = playerLevel > baseData.maxLevel ? baseData.maxLevel : level;
        //needExp = baseData.needExp * playerLevel;
        //currentExp = currentExp >= needExp ? LevelUp() : currentExp;


        return currentExp;
    }
}
