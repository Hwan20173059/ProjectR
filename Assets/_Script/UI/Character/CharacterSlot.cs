using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSlot : MonoBehaviour
{
    public CharacterData characterData;
    
    public Image[] characterSlotImage;
    public TextMeshProUGUI[] characterSlotText;

    private void Start()
    {
        characterSlotImage = GetComponentsInChildren<Image>();
        characterSlotImage[1].sprite = characterData.character.sprite;

        characterSlotText = GetComponentsInChildren<TextMeshProUGUI>();
        characterSlotText[0].text = characterData.character.name;

        Refresh();
    }

    public void CharacterSelect()
    {
        characterData.isSelected = true;

        Refresh();
    }

    public void CharacterUnSelect()
    {
        characterData.isSelected = false;

        Refresh();
    }

    public void Refresh()
    {
        characterSlotImage[4].gameObject.SetActive(false);

        if (characterData.isBuy == true)
            characterSlotText[2].text = "장착중";
        else
        {
            characterSlotText[2].text = "미보유";
            characterSlotImage[4].gameObject.SetActive(true);
        }

        if (characterData.isSelected == true || characterData.isBuy == false)
            characterSlotImage[3].gameObject.SetActive(true);
        else
            characterSlotImage[3].gameObject.SetActive(false);

    }
}
