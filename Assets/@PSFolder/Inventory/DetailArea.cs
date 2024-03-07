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

    public void ChangeSelectedItem(EquipItem e)
    {
        ChangeDetailActivation(false);
        _image.sprite = e.data.equipSprite;
        _text.text = e.data.info;
        if(e.isEquipped)
        {
            _unEquipButton.gameObject.SetActive(true);
        }
        else
        {
            _equipButton.gameObject.SetActive(true);
        }
        ChangeDetailActivation(true);
    }

    public void ChangeDetailActivation(bool detailActive)
    {
        detailObject.SetActive(detailActive);
        if(!detailActive)
        {
            _equipButton.gameObject.SetActive(false);
            _unEquipButton.gameObject.SetActive(false);
        }
    }
}