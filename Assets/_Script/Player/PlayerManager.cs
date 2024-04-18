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
    public int needExp = 100;
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

    [Header("tutorial")]
    public bool firstGame = true;
    public bool firstCharacter = true;
    public bool firstEquip = true;
    public bool firstInventory = true;
    public bool firstGuild = true;
    public bool firstShop = true;
    public bool firstGacha = true;
    public bool firstField = true;
    public bool firstDungeon = true;
    public bool firstBattle = true;


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
        GameEventManager.instance.uiEvent.ChangeEquip(detailArea.lastSelectedEquip.data.id);
        equip[n].isEquipped = false;
        equip[n] = detailArea.lastSelectedEquip;
        equip[n].isEquipped = true;
    }

    public void EquipLoadItem(int n, EquipItem equipItem)
    {
        equip[n].isEquipped = false;
        equip[n] = equipItem;
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
        if (titleManager.character != null)
        {
            Character character = Instantiate(titleManager.character, this.transform);
            character.spriteRenderer.color = new Color(1, 1, 1, 0);

            character.Init(DataManager.Instance.characterDatabase.GetDataByKey(id), level, exp);

            characterList.Add(character);
        }
        else if(townUiManager.characterPrefab != null)
        {
            Character character = Instantiate(townUiManager.characterPrefab, this.transform);
            character.spriteRenderer.color = new Color(1, 1, 1, 0);

            character.Init(DataManager.Instance.characterDatabase.GetDataByKey(id), level, exp);

            characterList.Add(character);
        }
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

        //currentExp = currentExp >= needExp ? LevelUp() : currentExp;

        AudioManager.Instance.PlayLevelUpSFX();

        return currentExp;
    }

    public void LoadCharacterData()
    {
        SaveData saveData = DataManager.Instance.saveData;

        if (saveData != null)
        {
            playerLevel = saveData.playerLevel;
            needExp = 100;
            currentExp = saveData.currentExp;
            gold = DataManager.Instance.saveData.gold;

            selectedCharacterIndex = saveData.selectCharacterID;
            selectTownID = saveData.selectTownID;

            string[] characterList = saveData.characterListID.Split(" ");
            string[] characterLevelList = saveData.characterListLevel.Split(" ");
            string[] characterExpList = saveData.characterListExp.Split(" ");

            for (int i = 0; i < characterList.Length - 1; i++)
                AddCharacter(int.Parse(characterList[i]), int.Parse(characterLevelList[i]), int.Parse(characterExpList[i]));

            firstGame = saveData.firstGame;
            firstCharacter = saveData.firstCharacter;
            firstEquip = saveData.firstEquip;
            firstInventory = saveData.firstInventory;
            firstGuild = saveData.firstGuild;
            firstShop = saveData.firstShop;
            firstGacha = saveData.firstGacha;
            firstField = saveData.firstField;
            firstDungeon = saveData.firstDungeon;
            firstBattle = saveData.firstBattle;
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
        string equippingitemListID = "";

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

        for (int i = 0; i < equip.Length; i++)
        {
            equippingitemListID += equip[i].data.id.ToString();
            equippingitemListID += " ";
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
        saveData.equippingitemListID = equippingitemListID;

        saveData.itemListID = itemListID;
        saveData.itemListCount = itemListCount;

        saveData.firstGame = firstGame;
        saveData.firstCharacter = firstCharacter;
        saveData.firstEquip = firstEquip;
        saveData.firstInventory = firstInventory;
        saveData.firstGuild = firstGuild;
        saveData.firstShop = firstShop;
        saveData.firstGacha = firstGacha;
        saveData.firstField = firstField;
        saveData.firstDungeon = firstDungeon;
        saveData.firstBattle = firstBattle;

        string ToJsonData = JsonUtility.ToJson(saveData, true);
        string filePath = Application.persistentDataPath + "/SaveDatas.json";
        File.WriteAllText(filePath, ToJsonData);

    }

    private void OnApplicationQuit()
    {
        SavePlayerData();
    }
}
