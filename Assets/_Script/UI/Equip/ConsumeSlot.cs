using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Unity.VisualScripting;
using TMPro;

public class ConsumeSlot : MonoBehaviour
{
    public DetailArea detailArea;
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI countTxt;

    private ConsumeItem _item;
    public ConsumeItem item
    {
        get { return _item; }
        set
        {
            _item = value;
            if (_item != null)
            {
                image.sprite = item.consumeSprite;
                image.color = new Color(1, 1, 1, 1);
                countTxt.text = item.count.ToString();
                countTxt.gameObject.SetActive(true);
            }
            else
            {
                image.color = new Color(1, 1, 1, 0);
                countTxt.gameObject.SetActive(false);
            }
        }
    }

    public void OnClickItem()
    {
        detailArea.ChangeSelectedItem(item);
    }
}
