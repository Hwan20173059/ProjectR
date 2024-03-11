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


    [Header("EquipUI")]
    public GameObject equipInventoryUI;
    public NowEquippedItemSlot nEquipItemSlot;
    public DetailArea detailArea;
    public EquipItem nowSelectedEquip;

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

    public void DungeonUIOn()
    {
        dungeonUI.SetActive(true);
    }

    public void DungeonUIOff()
    {
        dungeonUI.SetActive(false);
    }

    public void CharacterUIOn()
    {
        characterUI.SetActive(true);
    }

    public void CharacterUIOff()
    {
        characterUI.SetActive(false);
    }

    public void OpenInventory()
    {
        equipInventoryUI.SetActive(true);
    }
    public void CloseInventory()
    {
        equipInventoryUI.SetActive(false);
    }
}
