using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSlot : MonoBehaviour
{
    public CharacterSO characterSO;
    
    public Image[] characterSlotImage;
    public TextMeshProUGUI[] characterSlotText;

    private void Start()
    {
        characterSlotImage = GetComponentsInChildren<Image>();
        characterSlotImage[1].sprite = characterSO.sprite;

        characterSlotText = GetComponentsInChildren<TextMeshProUGUI>();
        characterSlotText[0].text = characterSO.name;

        Refresh();
    }

    public void CharacterSelect()
    {
        characterSO.isSelected = true;

        Refresh();
    }

    public void CharacterUnSelect()
    {
        characterSO.isSelected = false;

        Refresh();
    }

    public void Refresh()
    {
        characterSlotImage[4].gameObject.SetActive(false);

        if (characterSO.isBuy == true)
            characterSlotText[2].text = "장착중";
        else
        {
            characterSlotText[2].text = "미보유";
            characterSlotImage[4].gameObject.SetActive(true);
        }

        if (characterSO.isSelected == true || characterSO.isBuy == false)
            characterSlotImage[3].gameObject.SetActive(true);
        else
            characterSlotImage[3].gameObject.SetActive(false);

    }
}
