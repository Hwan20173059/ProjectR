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

        // Slot의 UI에 CharacterData의 Name을 적용
        characterSlotText = GetComponentsInChildren<TextMeshProUGUI>();
        characterSlotText[0].text = characterData.characterName;

        Refresh();
    }

    public void SelectButtonClick() // 버튼 할당용 함수
    {
        PlayerManager.Instance.selectedCharacterIndex = index;
        characterSelectSlot.RefreshAll();
        PlayerManager.Instance.townUiManager.townPlayer.character = characterData;
        PlayerManager.Instance.townUiManager.townPlayer.Refresh();
        PlayerManager.Instance.townUiManager.characterUI.CharacterInfoUIRefresh();
    }

    public void Refresh() // 상태에 따라 UI를 켜고 끈다.
    {
        if (PlayerManager.Instance.selectedCharacterIndex == index)
            characterSlotImage[3].gameObject.SetActive(true);
        else
            characterSlotImage[3].gameObject.SetActive(false);
    }
}
