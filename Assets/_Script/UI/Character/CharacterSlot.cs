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
        // Slot�� UI�� CharacterData�� Sprite�� ����
        characterSlotImage = GetComponentsInChildren<Image>();
        characterSlotImage[1].sprite = characterData.character.sprite;

        // Slot�� UI�� CharacterData�� Name�� ����
        characterSlotText = GetComponentsInChildren<TextMeshProUGUI>();
        characterSlotText[0].text = characterData.character.name;

        Refresh();
    }

    public void SelectButtonClick() // ��ư �Ҵ�� �Լ�
    {
        characterSelectSlot.characterManager.SelectCharacter(index);
    }

    public void Refresh() // ���¿� ���� UI�� �Ѱ� ����.
    {
        characterSlotImage[4].gameObject.SetActive(false);

        if (characterData.isBuy == true)
            characterSlotText[2].text = "������";
        else
        {
            characterSlotText[2].text = "�̺���";
            characterSlotImage[4].gameObject.SetActive(true);
        }

        if (characterData.isSelected == true || characterData.isBuy == false)
            characterSlotImage[3].gameObject.SetActive(true);
        else
            characterSlotImage[3].gameObject.SetActive(false);

    }
}
