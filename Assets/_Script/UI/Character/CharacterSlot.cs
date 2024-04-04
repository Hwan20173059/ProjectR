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
    public Character characterData;

    public CharacterSelectSlot characterSelectSlot;
        
    public Image[] characterSlotImage;
    public TextMeshProUGUI[] characterSlotText;

    private void Start()
    {
        characterSlotImage = GetComponentsInChildren<Image>();
        characterSlotImage[1].sprite = characterData.sprite;

        // Slot�� UI�� CharacterData�� Name�� ����
        characterSlotText = GetComponentsInChildren<TextMeshProUGUI>();
        characterSlotText[0].text = characterData.characterName;

        Refresh();
    }

    public void SelectButtonClick() // ��ư �Ҵ�� �Լ�
    {
        PlayerManager.Instance.selectedCharacterIndex = index;
        characterSelectSlot.RefreshAll();
        PlayerManager.Instance.townUiManager.townPlayer.character = characterData;
        PlayerManager.Instance.townUiManager.townPlayer.Refresh();
        PlayerManager.Instance.townUiManager.characterUI.CharacterInfoUIRefresh();
    }

    public void Refresh() // ���¿� ���� UI�� �Ѱ� ����.
    {
        if (PlayerManager.Instance.selectedCharacterIndex == index)
            characterSlotImage[3].gameObject.SetActive(true);
        else
            characterSlotImage[3].gameObject.SetActive(false);
    }
}
