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
    [SerializeField] private Button _equipButton;
    [SerializeField] private Button _unEquipButton;
    [SerializeField] private Button _useButton;
    [SerializeField] private GameObject _activateFalseObj;
    public bool isEquipping;

    public void ChangeSelectedItem(EquipItem e)
    {
        ChangeDetailActivation(false);
        _image.sprite = e.equipSprite;
        _text.text = e.data.info;
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
        _image.sprite = c.consumeSprite;
        _text.text = c.data.info;
        if(c.type == Type.Potion)
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