using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DetailArea : MonoBehaviour
{
    [SerializeField] private GameObject detailObject;
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private Button _equipButton;
    [SerializeField] private Button _unEquipButton;
    [SerializeField] private Button _useButton;
    [SerializeField] private GameObject _activateFalseObj;
    public ConsumeItem nowConsumeItem;
    public EquipItem nowSelectedEquip;
    public EquipItem lastSelectedEquip;
    public bool isEquipping;

    private void Start()
    {
        PlayerManager.Instance.detailArea = this;
    }
    public void ChangeSelectedItem(EquipItem e)
    {
        ChangeDetailActivation(false);
        lastSelectedEquip = nowSelectedEquip;
        nowSelectedEquip = e;
        _image.sprite = e.equipSprite;
        _text.text = e.data.info;
        itemName.text = e.data.equipName;
        if (e.data.id != 0)
        {
            if (e.isEquipped)
            {
                _unEquipButton.gameObject.SetActive(true);
            }
            else
            {
                _equipButton.gameObject.SetActive(true);
            }
        }
        ChangeDetailActivation(true);
    }

    public void ChangeSelectedItem(ConsumeItem c)
    {
        ChangeDetailActivation(false);
        nowConsumeItem = c;
        _image.sprite = c.consumeSprite;
        _text.text = c.data.info;
        itemName.text = c.data.consumeName;
        if(c.type != Type.Drop && c.type != Type.DungeonItem)
        {
            _useButton.gameObject.SetActive(true);
        }
        ChangeDetailActivation(true);
    }

    public void ChangeDetailActivation(bool detailActive)
    {
        detailObject.SetActive(detailActive);
        if (!detailActive)
        {
            _equipButton.gameObject.SetActive(false);
            _unEquipButton.gameObject.SetActive(false);
            _useButton.gameObject.SetActive(false);
        }
    }

    public void ActiveEquippingState()
    {
        _activateFalseObj.SetActive(true);
        isEquipping = true;
    }

    public void UnActiveEquippingState()
    {
        _activateFalseObj.SetActive(false);
        isEquipping = false;
    }
}