using Assets.PixelFantasy.PixelTileEngine.Scripts;
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
    public int playerTurnIndex;

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
        playerTurnIndex = 5;

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

        character.baseData.id = DataManager.Instance.battleDatabase.GetCharacterByKey(id).id;
        character.baseData.characterName = DataManager.Instance.battleDatabase.GetCharacterByKey(id).characterName;
        character.baseData.spritePath = DataManager.Instance.battleDatabase.GetCharacterByKey(id).spritePath;
        character.baseData.animatorPath = DataManager.Instance.battleDatabase.GetCharacterByKey(id).animatorPath;
        character.baseData.hp = DataManager.Instance.battleDatabase.GetCharacterByKey(id).hp;
        character.baseData.atk = DataManager.Instance.battleDatabase.GetCharacterByKey(id).atk;
        character.baseData.needExp = DataManager.Instance.battleDatabase.GetCharacterByKey(id).needExp;
        character.baseData.maxLevel = DataManager.Instance.battleDatabase.GetCharacterByKey(id).maxLevel;
        character.baseData.actionCoolTime = DataManager.Instance.battleDatabase.GetCharacterByKey(id).actionCoolTime;

        character.level = 1;

        character.Init();

        characterList.Add(character);
    }
}
