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


    [Header("EquipUI")]
    public GameObject equipInventoryUI;
    public NowEquippedItemSlot nEquipItemSlot;
    public DetailArea detailArea;
    public EquipItem nowSelectedEquip;
    public EquipItem lastSelectedEquip;

    private void Start()
    {
        playerManager = PlayerManager.Instance.GetComponent<PlayerManager>();
        playerManager.townUiManager = this;
    }

    public void GoDungeon(int index) 
    {
        playerManager.selectDungeonID = index;
        SceneManager.LoadScene("DungeonScene");
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
        dungeonUI.SetActive(true);
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

    public void OpenInventory()
    {
        equipInventoryUI.SetActive(true);
    }
    public void CloseInventory()
    {
        equipInventoryUI.SetActive(false);
    }

    public void FreshAfterEquip()
    {
        nowSelectedEquip = null;
        lastSelectedEquip = null;
        detailArea.ChangeDetailActivation(false);
        nEquipItemSlot.FreshEquippedSlot();
    }
}
