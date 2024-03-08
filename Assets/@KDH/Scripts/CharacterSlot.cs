using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSlot : MonoBehaviour
{
    public Character character;
    
    public Image[] characterSlotImage;
    public TextMeshProUGUI[] characterSlotText;

    public bool isSelected;
    public bool isBuy;

    private void Start()
    {
        characterSlotImage = GetComponentsInChildren<Image>();
        characterSlotImage[1].sprite = character.sprite;

        characterSlotText = GetComponentsInChildren<TextMeshProUGUI>();
        characterSlotText[0].text = character.name;

        Refresh();
    }

    public void CharacterSelect()
    {
        character.isSelected = true;

        Refresh();
    }

    public void CharacterUnSelect()
    {
        character.isSelected = false;

        Refresh();
    }

    public void Refresh()
    {
        if (character.isBuy == true)
            characterSlotText[2].text = "������";
        else
            characterSlotText[2].text = "�̺���";

        if (character.isSelected == true || character.isBuy == false)
            characterSlotImage[3].gameObject.SetActive(true);
        else
            characterSlotImage[3].gameObject.SetActive(false);
    }
}
