using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UseItemPanel : MonoBehaviour
{
    ItemUseButton itemUseButton;

    public void Init(BattleCanvas battleCanvas)
    {
        itemUseButton = GetComponentInChildren<ItemUseButton>();
        itemUseButton.Init(battleCanvas);

        foreach (ConsumeItem consumeItem in ItemManager.Instance.cInventory)
        {
            if (consumeItem.type == Type.AttackBuffPotion ||
                consumeItem.type == Type.SpeedBuffPotion ||
                consumeItem.type == Type.HpPotion)
            {
                battleCanvas.SetUseItemSlot(consumeItem);
            }
        }

        gameObject.SetActive(false);
    }
}
