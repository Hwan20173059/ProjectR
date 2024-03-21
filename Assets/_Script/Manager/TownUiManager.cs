using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class TownUiManager : MonoBehaviour
{
    public PlayerManager playerManager;

    [Header("UI")]
    public GameObject characterUI;
    public GameObject dungeonUI;
    public GameObject guildUI;
    public GameObject storeUI;
    public GameObject homeUI;
    public GameObject optionUI;


    [Header("EquipUI")]
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
    }

    public void GoField(int index) 
    {
        SceneManager.LoadScene("FieldScene");
    }

    public void CharacterUIOn()
    {
        characterUI.SetActive(true);
    }

    public void CharacterUIOff()
    {
        characterUI.SetActive(false);
    }

    public void DungeonUIOn()
    {
        if (playerManager.selectTownID == 0)
        {
            playerManager.fieldX = 0;
            playerManager.fieldY = 1;
        }
        else if (playerManager.selectTownID == 1) 
        {
            playerManager.fieldX = 4;
            playerManager.fieldY = 7;
        }
        SceneManager.LoadScene("FieldScene");
        //dungeonUI.SetActive(true);
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
    }

    public void StoreUIOn()
    {
        storeUI.SetActive(true);
    }

    public void StoreUIOff()
    {
        storeUI.SetActive(false);
    }

    public void HomeUIOn()
    {
        homeUI.SetActive(true);
    }

    public void HomeUIOff()
    {
        homeUI.SetActive(false);
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

    public void FreshAfterEquip()
    {
        nowSelectedEquip = null;
        lastSelectedEquip = null;
        detailArea.ChangeDetailActivation(false);
        nEquipItemSlot.FreshEquippedSlot();
    }
}
