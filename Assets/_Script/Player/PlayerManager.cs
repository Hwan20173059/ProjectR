using Assets.PixelFantasy.PixelTileEngine.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Unity.VisualScripting;
using Unity.VisualScripting.AssemblyQualifiedNameParser;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class PlayerManager : Singleton<PlayerManager>
{
    [Header("Info")]
    public int playerLevel = 1;
    public int needExp = 10;
    public int currentExp = 0;
    public int gold = 500;
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

    [Header("BattleSetting")]
    public bool autoBattle;
    public int battleSpeed;

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
        townUiManager.TownInfoRefresh();
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
        if (DataManager.Instance.saveData != null)
        {
            playerLevel = DataManager.Instance.saveData.playerLevel;
            needExp = playerLevel * 10;
            currentExp = DataManager.Instance.saveData.currentExp;
            gold = DataManager.Instance.saveData.gold;

            selectTownID = DataManager.Instance.saveData.selectTownID;

            string[] characterList = DataManager.Instance.saveData.characterListID.Split(" ");
            string[] characterLevelList = DataManager.Instance.saveData.characterListLevel.Split(" ");
            string[] characterExpList = DataManager.Instance.saveData.characterListExp.Split(" ");

            for (int i = 0; i < characterList.Length - 1; i++)
                AddCharacter(int.Parse(characterList[i]), int.Parse(characterLevelList[i]), int.Parse(characterExpList[i]));
        }
        else
        {
            AddCharacter(0, 1, 0);
        }
    }

    public void SavePlayerData(int index)
    {
        SaveData saveData = new SaveData();

        saveData.id = index;
        saveData.playerLevel = playerLevel;
        saveData.currentExp = currentExp;
        saveData.gold = gold;
        
        saveData.selectTownID = selectTownID;

        string characterList = "";
        string characterLevelList = "";
        string characterExpList = "";

        for(int i = 0; i < this.characterList.Count; i++) 
        {
            characterList += this.characterList[i].baseData.id.ToString();
            characterList += " ";

            characterLevelList += this.characterList[i].level.ToString();
            characterLevelList += " ";

            characterExpList += this.characterList[i].curExp.ToString();
            characterExpList += " ";
        }

        saveData.characterListID = characterList;
        saveData.characterListLevel = characterLevelList;
        saveData.characterListExp = characterExpList;        

        saveData.equipitemListID = "";
        saveData.itemListID = "";
        saveData.itemListCount = "";

        string ToJsonData = JsonUtility.ToJson(saveData, true);
        string filePath = Application.persistentDataPath + "/SaveDatas.json";
        File.WriteAllText(filePath, ToJsonData);

    }

    private void OnApplicationQuit()
    {
        SavePlayerData(0);
    }
}
