using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSlot : MonoBehaviour
{
    public int index;
    public CharacterSelectSlot characterSelectSlot;
    public CharacterData characterData;
    
    public Image[] characterSlotImage;
    public TextMeshProUGUI[] characterSlotText;

    private void Start()
    {
        // Slot의 UI에 CharacterData의 Sprite를 적용
        characterSlotImage = GetComponentsInChildren<Image>();
        characterSlotImage[1].sprite = characterData.character.sprite;

        // Slot의 UI에 CharacterData의 Name을 적용
        characterSlotText = GetComponentsInChildren<TextMeshProUGUI>();
        characterSlotText[0].text = characterData.character.name;

        Refresh();
    }

    public void SelectButtonClick() // 버튼 할당용 함수
    {
        characterSelectSlot.characterManager.SelectCharacter(index);
    }

    public void Refresh() // 상태에 따라 UI를 켜고 끈다.
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
