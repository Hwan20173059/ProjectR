using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UseItemSlot : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemText;
    [SerializeField] private Image slotBG;

    BattleCanvas battleCanvas;
    public ConsumeItem consumeItem;

    private void Awake()
    {
        button.onClick.AddListener(SelectUseItem);
    }

    public void Init(BattleCanvas battleCanvas, ConsumeItem consumeItem)
    {
        this.battleCanvas = battleCanvas;
        this.consumeItem = consumeItem;

        itemImage.sprite = consumeItem.consumeSprite;
        itemText.text = $"{consumeItem.data.consumeName}\n아이템 수량 : {consumeItem.count}";
    }

    public void UpdateUseItemSlot()
    {
        itemText.text = $"{consumeItem.data.consumeName}\n아이템 수량 : {consumeItem.count}";
    }

    void SelectUseItem()
    {
        battleCanvas.SelectUseItem(this);
        slotBG.color = Color.red;
    }

    public void SlotColorClear()
    {
        slotBG.color = Color.clear;
    }
}
