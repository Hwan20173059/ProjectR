using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GachaManager : MonoBehaviour
{
    private ItemManager itemManager;
    private PlayerManager playerManager;

    private Character nowChar;
    private EquipItem nowEquip;

    private Character[] charArray = new Character[10];
    private EquipItem[] equipArray = new EquipItem[10];

    private bool gacha10;

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
        // °¡Ã­ ÀÌÆåÆ®(ÇÔ¼ö)
        // °¡Ã­ °á°úÃ¢(ÇÔ¼ö)
    }
    public void EquipGacha()
    {
        playerManager.gold -= 300;
        int randomIndex = Random.Range(1, 57);
        itemManager.AddEquipItem(randomIndex);
        nowEquip = itemManager.GetEquipItem(randomIndex);

        if (!gacha10)
        {
            gachaResult.Equip1UI(nowEquip);
        }
    }

    public void CharacterGacha10()
    {
        for (int i = 0; i < 10; i++)
        {
            CharacterGacha();

            nowChar = null;
            charArray[i] = nowChar;
        }
        
    }

    public void EquipGacha10()
    {
        gacha10 = true;
        for (int i = 0; i < 10; i++)
        {
            EquipGacha();
            equipArray[i] = nowEquip;
        }
        gachaResult.Equip10UI(equipArray);
        System.Array.Clear(equipArray, 0, equipArray.Length);
        gacha10 = false;
    }

    // °¡Ã­ ÀÌÆåÆ® ÇÔ¼ö
}
