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

    [Header("BackGround")]
    public SpriteRenderer background;
    public Sprite firstTown;
    public Sprite secondTown;

    [Header("UI")]
    public CharacterUI characterUI;
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

    private void Start()
    {
        playerManager = PlayerManager.Instance;
        playerManager.townUiManager = this;

        playerManager.isField = false;
        playerManager.isTown = true;

        RefreshBackground();
        PlayerInfoRefresh();
        TownInfoRefresh();
        townPlayer.init();
    }

    public void CharacterUIOn()
    {
        characterUI.CharacterUIon();
        characterUI.GetComponentInChildren<CharacterSelectSlot>().Init();
    }

    public void CharacterUIOff()
    {
        characterUI.CharacterUIoff();
    }

    public void GoField()
    {
        if (playerManager.selectTownID == 0)
        {
            playerManager.fieldX = 2;
            playerManager.fieldY = 1;
        }
        else if (playerManager.selectTownID == 1)
        {
            playerManager.fieldX = 22;
            playerManager.fieldY = 3;
        }

        PlayerManager.Instance.currentState = CurrentState.field;
        SceneManager.LoadScene("FieldScene");
    }

    public void GuildUIOn()
    {
        guildUI.SetActive(true);
    }

    public void GuildUIOff()
    {
        guildUI.SetActive(false);
        //talkUI.SetActive(false);
    }

    public void StoreUIOn()
    {
        storeUI.SetActive(true);
    }

    public void StoreUIOff()
    {
        storeUI.SetActive(false);
        //talkUI.SetActive(false);
    }

    public void HomeUIOn()
    {
        homeUI.SetActive(true);
    }

    public void HomeUIOff()
    {
        homeUI.SetActive(false);
        //talkUI.SetActive(false);
    }

    public void OptionUIOn()
    {
        optionUI.SetActive(true);
    }

    public void OptionUIOff()
    {
        optionUI.SetActive(false);
    }

    public void PlayerInfoRefresh()
    {
        //playerName.text = playerManager.name;
        playerName.text = "모험가";
        playerLevel.text = "Lv. " + playerManager.playerLevel;
        playerGold.text = "<sprite=0> " + playerManager.gold;
        playerExp.value = (float)playerManager.currentExp / (float)playerManager.needExp;
    }

    public void RefreshBackground()
    {
        switch (playerManager.selectTownID)
        {
            case 0:
                background.sprite = firstTown;
                break;
            case 1:
                background.sprite = secondTown;
                break;
        }
    }


    public void TownInfoRefresh()
    {
        if (playerManager.selectTownID == 0)
            townName.text = "초심자의 마을";
        else if (playerManager.selectTownID == 1)
            townName.text = "수도 엘더";
    }
}
