using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Unity.VisualScripting;

public class EquipSlot : MonoBehaviour
{
    [SerializeField] private Image image;

    private EquipItem _item;
    public EquipItem item {
        get { return _item; }
        set {
            _item = value;
            if (_item != null)
            {
                image.sprite = item.data.equipSprite;
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
        UIManager.Instance.nowSelectedEquip = item;
        UIManager.Instance.detailArea.ChangeSelectedItem(item);
    }
}
