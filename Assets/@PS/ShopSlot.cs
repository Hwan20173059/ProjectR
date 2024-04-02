using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class ShopSlot : MonoBehaviour
{
    [SerializeField] private BuyPopup buyPopup;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI priceText;

    private ConsumeItem _item;
    public ConsumeItem item
    {
        get { return _item; }
        set
        {
            _item = value;
            if (_item != null)
            {
                priceText.text = "<sprite=0>" + item.data.price.ToString();
                itemName.text = item.data.consumeName;
                image.sprite = item.consumeSprite;
                image.color = new Color(1, 1, 1, 1);
            }
            else
            {
                image.color = new Color(1, 1, 1, 0);
            }
        }
    }

    public void OnClickItem()
    {
        buyPopup.OpenPopup(item);
    }
}
