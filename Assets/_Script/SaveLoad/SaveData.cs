using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int id;
    public int playerLevel;
    public int currentExp;
    public int gold;
    public int selectCharacterID;
    public int selectTownID;    
    public string characterListID;
    public string characterListLevel;
    public string characterListExp;
    public string equippingitemListID;
    public string equipitemListID;
    public string itemListID;
    public string itemListCount;
    public bool firstGame;
    public bool firstCharacter;
    public bool firstEquip;
    public bool firstInventory;
    public bool firstGuild;
    public bool firstShop;
    public bool firstGacha;
    public bool firstField;
    public bool firstDungeon;
    public bool firstBattle;
}
