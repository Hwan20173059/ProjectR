using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Unity.VisualScripting;

public class EquipSlot : MonoBehaviour
{
    public TownUiManager townUiManager;
    [SerializeField] private Image image;

    private EquipItem _item;
    public EquipItem item {
        get { return _item; }
        set {
            _item = value;
            if (_item != null)
            {
                image.sprite = item.equipSprite;
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
        townUiManager.lastSelectedEquip = townUiManager.nowSelectedEquip;
        townUiManager.nowSelectedEquip = item;
        townUiManager.detailArea.ChangeSelectedItem(item);
    }
}
