using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GachaManager : MonoBehaviour
{
    private ItemManager itemManager;
    private PlayerManager playerManager;

    private ConsumeItem nowChar;
    private EquipItem nowEquip;

    private ConsumeItem[] charArray = new ConsumeItem[10];
    private EquipItem[] equipArray = new EquipItem[10];
    private bool[] isHaving = new bool[10];

    private bool gacha10;
    private bool isNowItemHaving;

    public GachaResult gachaResult;

    [SerializeField] private TextMeshProUGUI gold;
    [SerializeField] private TownUiManager townUiManager;

    private void Start()
    {
        itemManager = ItemManager.Instance;
        playerManager = PlayerManager.Instance;
    }

    public void StartGacha(int i)
    {
        AudioManager.Instance.PlayUISelectSFX();

        switch (i)
        {
            case 0:
                if (playerManager.gold >= 300)
                {
                    playerManager.gold -= 300;
                    CharacterGacha();
                }
                break;
            case 1:
                if (playerManager.gold >= 3000)
                {
                    playerManager.gold -= 3000;
                    CharacterGacha10();
                }
                break;
            case 2:
                if (playerManager.gold >= 300)
                {
                    playerManager.gold -= 300;
                    EquipGacha();
                }
                break;
            case 3:
                if (playerManager.gold >= 3000)
                {
                    playerManager.gold -= 3000;
                    EquipGacha10();
                }
                break;
        }
    }
    public void CharacterGacha()
    {
        isNowItemHaving = true;
        int randomIndex = Random.Range(32, 45);
        if (!playerManager.HaveCharacter(randomIndex - 32))
        {
            isNowItemHaving = false;
            itemManager.AddConsumeItem(randomIndex);
        }
        else
        {
            CharacterToScroll(randomIndex - 32);
        }
        nowChar = itemManager.GetConsumeItem(randomIndex);

        if (!gacha10)
        {
            gachaResult.Character1UI(nowChar, isNowItemHaving);
            RefreshGold();
        }
    }
    public void EquipGacha()
    {
        GameEventManager.instance.uiEvent.Gacha();
        isNowItemHaving = true;
        int randomIndex = Random.Range(1, 57);
        if (!itemManager.HaveEquipItem(randomIndex))
        {
            isNowItemHaving = false;
            itemManager.AddEquipItem(randomIndex);
        }
        else
        {
            ItemToGold(randomIndex);
        }
        nowEquip = itemManager.GetEquipItem(randomIndex);

        if (!gacha10)
        {
            gachaResult.Equip1UI(nowEquip, isNowItemHaving);
            RefreshGold();
        }
    }


    public void CharacterGacha10()
    {
        gacha10 = true;
        for (int i = 0; i < 10; i++)
        {
            CharacterGacha();
            charArray[i] = nowChar;
            isHaving[i] = isNowItemHaving;
        }
        gachaResult.Character10UI(charArray, isHaving);
        System.Array.Clear(equipArray, 0, equipArray.Length);
        gacha10 = false;
        RefreshGold();
    }

    public void EquipGacha10()
    {
        gacha10 = true;
        for (int i = 0; i < 10; i++)
        {
            EquipGacha();
            equipArray[i] = nowEquip;
            isHaving[i] = isNowItemHaving;
        }
        gachaResult.Equip10UI(equipArray, isHaving);
        System.Array.Clear(equipArray, 0, equipArray.Length);
        gacha10 = false;
        RefreshGold();
    }

    private void ItemToGold(int index)
    {
        if (index < 22) playerManager.gold += 100;
        else if (index < 38) playerManager.gold += 150;
        else if (index < 51) playerManager.gold += 200;
        else playerManager.gold += 250;
    }

    private void CharacterToScroll(int index)
    {
        if (index < 5) itemManager.AddConsumeItem(100);
        else if (index < 9) itemManager.AddConsumeItem(101);
        else itemManager.AddConsumeItem(102);
    }

    public void RefreshGold()
    {
        townUiManager.PlayerInfoRefresh();
        if (playerManager == null) playerManager = PlayerManager.Instance;
        gold.text = "<sprite=0> " + playerManager.gold;
    }
}
