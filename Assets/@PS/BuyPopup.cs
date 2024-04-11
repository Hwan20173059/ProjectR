using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyPopup : MonoBehaviour
{
    private ItemManager itemManager;
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemInfo;
    [SerializeField] private TextMeshProUGUI itemPrice;

    private int itemID;
    private int price;

    private void Awake()
    {
        itemManager = ItemManager.Instance;
    }

    public void ChangePopupInfo(ConsumeItem item)
    {
        price = item.data.price;
        itemID = item.data.id;
        itemImage.sprite = item.consumeSprite;
        itemName.text = item.data.consumeName;
        itemInfo.text = item.data.info;
        itemPrice.text = "<sprite=0>" + item.data.price.ToString();
    }

    public void OpenPopup(ConsumeItem item)
    {
        ChangePopupInfo(item);
        gameObject.SetActive(true);
    }

    public void BuyItem()
    {
        if(PlayerManager.Instance.gold >= price)
        {
            PlayerManager.Instance.gold -= price;
            itemManager.AddConsumeItem(itemID);
            ClosePopup();
        }
    }
    public void ClosePopup()
    {
        gameObject.SetActive(false);
    }
}
