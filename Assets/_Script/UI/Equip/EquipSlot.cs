using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Unity.VisualScripting;

public class EquipSlot : MonoBehaviour
{
    public DetailArea detailArea;
    [SerializeField] private Image image;
    [SerializeField] private Image panelImage;

    private EquipItem _item;
    public EquipItem item {
        get { return _item; }
        set {
            _item = value;
            if (_item != null)
            {
                image.sprite = item.equipSprite;
                image.color = new Color(1, 1, 1, 1);
                if (panelImage != null) panelImage.color = ItemGradeColor(_item.data.grade);
            }
            else
            {
                image.color = new Color(1, 1, 1, 0);
            }
        }
    }

    public void OnClickItem()
    {
        if (item != null) detailArea.ChangeSelectedItem(item);
    }

    public Color ItemGradeColor(int grade)
    {
        if (grade == 0)
        {
            return new Color(225f / 255f, 225f / 255f, 225f / 255f);
        }
        else if (grade == 1)
        {
            return new Color(180f / 255f, 206f / 255f, 1f);
        }
        else if (grade == 2)
        {
            return new Color(212f / 255f, 194f / 255f, 1f);
        }
        else if (grade == 3)
        {
            return new Color(1f, 212f / 255f, 117f / 255f);
        }
        else return new Color(1, 1, 1, 1);
    }
}
