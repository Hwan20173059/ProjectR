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

public enum CurrentState
{
    title,
    town1,
    town2,
    field,
    dungeon1,
    dungeon2,
    dungeon3,
    dungeon4
}

public class PlayerManager : Singleton<PlayerManager>
{
    [Header("Info")]
    public int playerLevel = 1;
    public int needExp = 10;
    public int currentExp = 0;
    public int gold = 500;
    public int playerTurnIndex = 5;

    public CurrentState currentState;

    [Header("Character")]
    public List<Character> characterList = new List<Character>();
    public int selectedCharacterIndex = 0;

    [Header("Equip")]
    public EquipItem[] equip = new EquipItem[3];

    [Header("Town")]
    public TownUiManager townUiManager;
    public DetailArea detailArea;
    public TitleManager titleManager;

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
        EquipItem baseEquip = new EquipItem(DataManager.Instance.itemDatabase.GetItemByKey(0));

        for (int i = 0; i < 3; i++)
        {
            if (equip[i] == null)
                equip[i] = baseEquip;
        }

        LoadCharacterData();
    }
    
    public void EquipNewItem(int n)
    {
        equip[n].isEquipped = false;
        equip[n] = detailArea.lastSelectedEquip;
        equip[n].isEquipped = true;
    }

    public bool HaveCharacter(int id)
    {
        int index = characterList.FindIndex(c => c.baseData.id == id);
        if (index != -1) return true;
        else return false;
    }

    public void AddCharacter(int id, int level, int exp)
    {
        Character character = Instantiate(titleManager.character,this.transform);
        character.spriteRenderer.color = new Color(1, 1, 1, 0);

        character.Init(DataManager.Instance.characterDatabase.GetDataByKey(id), level, exp);

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
        currentExp = currentExp - needExp;
        playerLevel++;

        //todo : Data¿¡ ÀúÀå
        needExp = needExp * playerLevel;
        currentExp = currentExp >= needExp ? LevelUp() : currentExp;

        return currentExp;
    }

    public void LoadCharacterData()
    {
        SaveData saveData = DataManager.Instance.saveData;

        if (saveData != null)
        {
            playerLevel = saveData.playerLevel;
            needExp = playerLevel * 10;
            currentExp = saveData.currentExp;
            gold = DataManager.Instance.saveData.gold;

            selectedCharacterIndex = saveData.selectCharacterID;
            selectTownID = saveData.selectTownID;

            string[] characterList = saveData.characterListID.Split(" ");
            string[] characterLevelList = saveData.characterListLevel.Split(" ");
            string[] characterExpList = saveData.characterListExp.Split(" ");

            for (int i = 0; i < characterList.Length - 1; i++)
                AddCharacter(int.Parse(characterList[i]), int.Parse(characterLevelList[i]), int.Parse(characterExpList[i]));
        }
        else
        {
            AddCharacter(0, 1, 0);
        }
    }

    public void SavePlayerData()
    {
        SaveData saveData = new SaveData();
        ItemManager itemManager = ItemManager.Instance;

        saveData.playerLevel = playerLevel;
        saveData.currentExp = currentExp;
        saveData.gold = gold;

        saveData.selectCharacterID = selectedCharacterIndex;
        saveData.selectTownID = selectTownID;

        string characterList = "";
        string characterLevelList = "";
        string characterExpList = "";

        string equipitemListID = "";

        string itemListID = "";
        string itemListCount = "";

        for (int i = 0; i < this.characterList.Count; i++) 
        {
            characterList += this.characterList[i].baseData.id.ToString();
            characterList += " ";

            characterLevelList += this.characterList[i].level.ToString();
            characterLevelList += " ";

            characterExpList += this.characterList[i].curExp.ToString();
            characterExpList += " ";
        }

        for (int i = 0; i < itemManager.eInventory.Count; i++)
        {
            equipitemListID += itemManager.eInventory[i].data.id.ToString();
            equipitemListID += " ";
        }


        for (int i = 0; i < itemManager.cInventory.Count; i++)
        {
            itemListID += itemManager.cInventory[i].data.id.ToString();
            itemListID += " ";

            itemListCount += itemManager.cInventory[i].count.ToString();
            itemListCount += " ";
        }

        saveData.characterListID = characterList;
        saveData.characterListLevel = characterLevelList;
        saveData.characterListExp = characterExpList;

        saveData.equipitemListID = equipitemListID;

        saveData.itemListID = itemListID;
        saveData.itemListCount = itemListCount;

        string ToJsonData = JsonUtility.ToJson(saveData, true);
        string filePath = Application.persistentDataPath + "/SaveDatas.json";
        File.WriteAllText(filePath, ToJsonData);

    }

    private void OnApplicationQuit()
    {
       // SavePlayerData();
    }
}
