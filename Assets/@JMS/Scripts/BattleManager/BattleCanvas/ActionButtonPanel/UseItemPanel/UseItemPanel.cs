using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UseItemPanel : MonoBehaviour
{
    BattleCanvas battleCanvas;

    ItemUseButton itemUseButton;

    public void Init(BattleCanvas battleCanvas)
    {
        this.battleCanvas = battleCanvas;

        itemUseButton = GetComponentInChildren<ItemUseButton>();

        itemUseButton.button.onClick.AddListener(OnClickItemUseButton);

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

    private void OnClickItemUseButton()
    {
        battleCanvas.OnClickItemUseButton();
    }
}
