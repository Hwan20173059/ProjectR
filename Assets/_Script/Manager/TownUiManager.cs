using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TownUiManager : MonoBehaviour
{
    public PlayerManager playerManager;

    [Header("Character")]
    public Character characterPrefab;
    public TownPlayer townPlayer;

    [Header("PlayerInfo")]
    public TextMeshProUGUI playerName;
    public TextMeshProUGUI playerLevel;
    public Slider playerExp;
    public TextMeshProUGUI playerGold;
    public TextMeshProUGUI townName;

    [Header("UI")]
    public GameObject characterUI;
    public GameObject inventoryUI;
    public GameObject detailArea1;
    public GameObject dungeonUI;
    public GameObject guildUI;
    public GameObject storeUI;
    public GameObject homeUI;
    public GameObject optionUI;
    public GameObject talkUI;

    [Header("EquipUI")]
    public Inventory inventory;
    public GameObject equipInventoryUI;
    public NowEquippedItemSlot nEquipItemSlot;
    public DetailArea detailArea;
    public EquipItem nowSelectedEquip;
    public EquipItem lastSelectedEquip;

    public GameObject consumeInventoryUI;
    public ConsumeItem nowSelectedConsume;

    private void Start()
    {
        playerManager = PlayerManager.Instance;
        playerManager.townUiManager = this;

        playerManager.isField = false;
        playerManager.isTown = true;

        PlayerInfoRefresh();
        TownInfoRefresh();
    }

    public void CharacterUIOn()
    {
        characterUI.SetActive(true);
        characterUI.GetComponentInChildren<CharacterSelectSlot>().Init();
    }

    public void CharacterUIOff()
    {
        characterUI.SetActive(false);
    }

    public void DungeonUIOn()
    {
        if (playerManager.selectTownID == 0)
        {
            playerManager.fieldX = 2;
            playerManager.fieldY = 1;
        }
        else if (playerManager.selectTownID == 1)
        {
            playerManager.fieldX = 8;
            playerManager.fieldY = 6;
        }

        SceneManager.LoadScene("FieldScene");
    }

    public void DungeonUIOff()
    {
        dungeonUI.SetActive(false);
    }

    public void GuildUIOn()
    {
        guildUI.SetActive(true);
    }

    public void GuildUIOff()
    {
        guildUI.SetActive(false);
        talkUI.SetActive(false);
    }

    public void InventoryUIOn()
    {
        inventoryUI.SetActive(true);
        detailArea1.SetActive(true);
    }

    public void StoreUIOn()
    {
        storeUI.SetActive(true);
    }

    public void StoreUIOff()
    {
        storeUI.SetActive(false);
        talkUI.SetActive(false);
    }

    public void HomeUIOn()
    {
        homeUI.SetActive(true);
    }

    public void HomeUIOff()
    {
        homeUI.SetActive(false);
        talkUI.SetActive(false);
    }

    public void OptionUIOn()
    {
        optionUI.SetActive(true);
    }

    public void OptionUIOff()
    {
        optionUI.SetActive(false);
    }

    public void OpenInventory()
    {
        inventory.FreshSlot();
        detailArea.ChangeDetailActivation(false);
        equipInventoryUI.SetActive(true);
        detailArea.gameObject.SetActive(true);
    }
    public void CloseInventory()
    {
        equipInventoryUI.SetActive(false);
        consumeInventoryUI.SetActive(false);
        detailArea.gameObject.SetActive(false);
    }

    public void OpenConsumeInventory()
    {
        inventory.FreshConsumeSlot();
        detailArea.ChangeDetailActivation(false);
        consumeInventoryUI.SetActive(true);
        detailArea.gameObject.SetActive(true);
    }

    public void FreshAfterEquip()
    {
        nowSelectedEquip = null;
        lastSelectedEquip = null;
        detailArea.ChangeDetailActivation(false);
        nEquipItemSlot.FreshEquippedSlot();
    }

    public void PlayerInfoRefresh()
    {
        //playerName.text = playerManager.name;
        playerName.text = "���谡";
        playerLevel.text = "Lv. " + playerManager.playerLevel;
        playerGold.text = "��� : " + playerManager.gold.ToString();
        playerExp.value = (float)playerManager.currentExp / (float)playerManager.needExp;
    }

    public void TownInfoRefresh()
    {
        switch (playerManager.selectTownID)
        {
            case 0:
                townName.text = "�ʽ����� ����";
                break;
            case 1:
                townName.text = "���� ����";
                break;
        }
    }
}
