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

        isBuy = character.isBuy;

        if (isBuy == true)
            characterSlotImage[3].gameObject.SetActive(false);
        else
            characterSlotImage[3].gameObject.SetActive(true);
    }

    public void CharacterSelect()
    {
        character.isSelected = true;
    }

    public void Refresh()
    {
        if (isSelected == true)



        if (isBuy == true)
            characterSlotImage[3].gameObject.SetActive(false);
        else
            characterSlotImage[3].gameObject.SetActive(true);
    }
}
