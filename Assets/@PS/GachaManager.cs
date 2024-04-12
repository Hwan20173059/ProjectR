using System.Collections;
using System.Collections.Generic;
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

    private void Start()
    {
        itemManager = ItemManager.Instance;
        playerManager = PlayerManager.Instance;
    }
    public void CharacterGacha()
    {
        // °ñµå °¨¼Ò
        playerManager.gold -= 500;
        // Ä³¸¯ÅÍ Ãß°¡
        int randomIndex = Random.Range(32, 45);
        itemManager.AddConsumeItem(randomIndex);
        nowChar = itemManager.GetConsumeItem(randomIndex);

        if (!gacha10)
        {
            gachaResult.Character1UI(nowChar);
        }
        // °¡Ã­ ÀÌÆåÆ®(ÇÔ¼ö)
        // °¡Ã­ °á°úÃ¢(ÇÔ¼ö)
    }
    public void EquipGacha()
    {
        isNowItemHaving = true;
        playerManager.gold -= 300;
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
        }
        gachaResult.Character10UI(charArray);
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

    // °¡Ã­ ÀÌÆåÆ® ÇÔ¼ö
}
