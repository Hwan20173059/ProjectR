using Assets.PixelFantasy.PixelTileEngine.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.AssemblyQualifiedNameParser;
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

        LoadPlayerData(0);

        townUiManager.PlayerInfoRefresh();
    }
    
    public void EquipNewItem(int n)
    {
        equip[n].isEquipped = false;
        equip[n] = townUiManager.lastSelectedEquip;
        equip[n].isEquipped = true;
    }

    public void AddCharacter(int id, int level, int exp)
    {
        Character character = Instantiate(townUiManager.characterPrefab,this.transform);
        character.spriteRenderer.color = new Color(1, 1, 1, 0);

        character.LoadInit(DataManager.Instance.battleDatabase.GetCharacterByKey(id), level, exp);

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

    public void LoadPlayerData(int index)
    {
        playerLevel = DataManager.Instance.saveDatabase.saveDic[index].playerLevel;
        needExp = playerLevel * 10;
        currentExp = DataManager.Instance.saveDatabase.saveDic[index].currentExp;
        gold = DataManager.Instance.saveDatabase.saveDic[index].gold;

        selectTownID = DataManager.Instance.saveDatabase.saveDic[index].selectTownID;

        string[] characterList = DataManager.Instance.saveDatabase.saveDic[index].characterListID.Split(" ");
        string[] characterLevelList = DataManager.Instance.saveDatabase.saveDic[index].characterListLevel.Split(" ");
        string[] characterExpList = DataManager.Instance.saveDatabase.saveDic[index].characterListExp.Split(" ");

        for (int i = 0; i < characterList.Length; i++)
            AddCharacter(int.Parse(characterList[i]), int.Parse(characterLevelList[i]), int.Parse(characterExpList[i]));

    }
}
