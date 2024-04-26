using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterSelectSlot : MonoBehaviour
{
    public CharacterSlot slotPrefab;
    public List<CharacterSlot> characterSlots = new List<CharacterSlot>();

    void OnEnable()
    {
        // List의 크기에 맞게 Slot 넓이 조절
        RectTransform rectTransform = GetComponent<RectTransform>();
        int width = 240 * PlayerManager.Instance.characterList.Count;
        rectTransform.sizeDelta = new Vector2(width, 250);

        // List의 크기만큼 Slot을 생성하고 관리를 위해 index 부여, Slots 리스트에 저장
        for (int i = 0; i < PlayerManager.Instance.characterList.Count; i++)
        {
            slotPrefab.index = i;
            slotPrefab.characterData = PlayerManager.Instance.characterList[i];
            slotPrefab.characterSelectSlot = this;
            characterSlots.Add(Instantiate(slotPrefab, this.transform));
        }
    }

    void OnDisable()
    {
        for (int i = 0; i < characterSlots.Count; i++)
        {
            Destroy(characterSlots[i].gameObject);
        }
        characterSlots.Clear();
    }

    public void RefreshAll() // 모든 슬롯의 상태를 refresh하는 함수
    {
        for(int i = 0; i < characterSlots.Count; i++)
        {
            characterSlots[i].Refresh();
        }
    }
}
