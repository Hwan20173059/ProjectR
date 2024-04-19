using System;
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
    public TownTutorialManager tutorialUI;
    public BuyPopup buyPopup;
    public GameObject resetUI;

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

        if(playerManager.firstGame == true)
        {
            tutorialUI.gameObject.SetActive(true);
            tutorialUI.ActiveTutorial(0);

            playerManager.firstGame = false;
        }

        RefreshBackground();
        PlayerInfoRefresh();
        TownInfoRefresh();
        townPlayer.init();
    }

    public void CharacterUIOn()
    {
        AudioManager.Instance.PlayUISelectSFX();
        if (playerManager.firstCharacter == true)
        {
            tutorialUI.gameObject.SetActive(true);
            tutorialUI.ActiveTutorial(1);
            characterUI.CharacterUIon();

            playerManager.firstCharacter = false;
        }
        else
        {
            characterUI.CharacterUIon();
        }
    }

    public void CharacterUIOff()
    {
        AudioManager.Instance.PlayUISelectSFX();
        characterUI.CharacterUIoff();
    }

    public void GoField()
    {
        AudioManager.Instance.PlayUISelectSFX();
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
        AudioManager.Instance.SetState();
        SceneManager.LoadScene("FieldScene");
    }

    public void GuildUIOn()
    {
        AudioManager.Instance.PlayUISelectSFX();
        if (playerManager.firstGuild == true)
        {
            tutorialUI.gameObject.SetActive(true);
            tutorialUI.ActiveTutorial(4);
            guildUI.SetActive(true);

            playerManager.firstGuild = false;
        }
        else
        {
            guildUI.SetActive(true);
        }
    }

    public void GuildUIOff()
    {
        AudioManager.Instance.PlayUISelectSFX();
        guildUI.SetActive(false);
        //talkUI.SetActive(false);
    }

    public void StoreUIOn()
    {
        AudioManager.Instance.PlayUISelectSFX();
        buyPopup.RefreshGoldUI();
        if (playerManager.firstShop == true)
        {
            tutorialUI.gameObject.SetActive(true);
            tutorialUI.ActiveTutorial(5);
            storeUI.SetActive(true);

            playerManager.firstShop = false;
        }
        else
        {
            storeUI.SetActive(true);
        }
    }

    public void StoreUIOff()
    {
        AudioManager.Instance.PlayUISelectSFX();
        storeUI.SetActive(false);
        //talkUI.SetActive(false);
    }

    public void HomeUIOn()
    {
        AudioManager.Instance.PlayUISelectSFX();
        if (playerManager.firstGacha == true)
        {
            tutorialUI.gameObject.SetActive(true);
            tutorialUI.ActiveTutorial(6);
            homeUI.SetActive(true);

            playerManager.firstGacha = false;
        }
        else
        {
            homeUI.SetActive(true);
        }
        homeUI.GetComponent<GachaManager>().RefreshGold();
    }

    public void HomeUIOff()
    {
        AudioManager.Instance.PlayUISelectSFX();
        homeUI.SetActive(false);
        //talkUI.SetActive(false);
    }

    public void InventoryUIOn()
    {
        if (playerManager.firstEquip == true)
        {
            tutorialUI.gameObject.SetActive(true);
            tutorialUI.ActiveTutorial(2);
            inventory.OpenInventory();

            playerManager.firstEquip = false;
        }
        else
        {
            inventory.OpenInventory();
        }
        detailArea.RefreshGoldUI();
    }

    public void CInventoryUIOn()
    {
        if (playerManager.firstInventory == true)
        {
            tutorialUI.gameObject.SetActive(true);
            tutorialUI.ActiveTutorial(3);
            inventory.OpenConsumeInventory();

            playerManager.firstInventory = false;
        }
        else
        {
            inventory.OpenConsumeInventory();
        }
        detailArea.RefreshGoldUI();
    }

    public void OptionUIOn()
    {
        AudioManager.Instance.PlayUISelectSFX();
        optionUI.SetActive(true);
    }

    public void OptionUIOff()
    {
        AudioManager.Instance.PlayUISelectSFX();
        optionUI.SetActive(false);
    }

    public void PlayerInfoRefresh()
    {
        //playerName.text = playerManager.name;
        if (playerManager.playerLevel < 10)
            playerName.text = "풋내기 모험가";
        else if (playerManager.playerLevel < 20)
            playerName.text = "숙련된 모험가";
        else if (playerManager.playerLevel < 25)
            playerName.text = "베테랑 모험가";
        else if (playerManager.playerLevel < 30)
            playerName.text = "전설의 모험가";
        else if (playerManager.playerLevel == 30)
            playerName.text = "살아있는 신화";

        playerLevel.text = "Lv." + playerManager.playerLevel;
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

    public void ResetTutorial()
    {
        AudioManager.Instance.PlayUISelectSFX();
        playerManager.firstBattle = true;
        playerManager.firstCharacter = true;
        playerManager.firstDungeon = true;
        playerManager.firstEquip = true;
        playerManager.firstField = true;
        playerManager.firstGacha = true;
        playerManager.firstGame = true;
        playerManager.firstGuild = true;
        playerManager.firstInventory = true;
        playerManager.firstShop = true;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ResetPanelOn()
    {
        AudioManager.Instance.PlayUISelectSFX();
        resetUI.SetActive(true);
    }

    public void ResetPanelOff()
    {
        AudioManager.Instance.PlayUISelectSFX();
        resetUI.SetActive(true);
    }

    public void DataReset()
    {
        System.IO.File.Delete(Application.persistentDataPath + "/SaveDatas.json");
        System.IO.File.Delete(Application.persistentDataPath + "/QuestSaveData.json");

        playerManager.isReset = true;

        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
