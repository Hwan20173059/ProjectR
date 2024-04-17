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
        switch (i)
        {
            case 0:
                if (playerManager.gold >= 500)
                {
                    playerManager.gold -= 500;
                    CharacterGacha();
                    RefreshGold();
                }
                break;
            case 1:
                if (playerManager.gold >= 5000)
                {
                    playerManager.gold -= 5000;
                    CharacterGacha10();
                    RefreshGold();
                }
                break;
            case 2:
                if (playerManager.gold >= 300)
                {
                    playerManager.gold -= 300;
                    EquipGacha();
                    RefreshGold();
                }
                break;
            case 3:
                if (playerManager.gold >= 3000)
                {
                    playerManager.gold -= 3000;
                    EquipGacha10();
                    RefreshGold();
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
        nowChar = itemManager.GetConsumeItem(randomIndex);

        if (!gacha10)
        {
            gachaResult.Character1UI(nowChar, isNowItemHaving);
        }
    }
    public void EquipGacha()
    {
        isNowItemHaving = true;
        int randomIndex = Random.Range(1, 57);
        if (!itemManager.HaveEquipItem(randomIndex))
        {
            isNowItemHaving = false;
            itemManager.AddEquipItem(randomIndex);
        }
        nowEquip = itemManager.GetEquipItem(randomIndex);

        if (!gacha10)
        {
            gachaResult.Equip1UI(nowEquip, isNowItemHaving);
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
    }


    public void RefreshGold()
    {
        townUiManager.PlayerInfoRefresh();
        if (playerManager == null) playerManager = PlayerManager.Instance;
        gold.text = "<sprite=0> " + playerManager.gold;
    }
}
