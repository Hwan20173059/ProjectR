using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterSelectSlot : MonoBehaviour
{
    public CharacterManager characterManager;

    public CharacterSlot slotPrefab;
    public List<CharacterSlot> characterSlots = new List<CharacterSlot>();

    private void Start()
    {
        // List의 크기에 맞게 Slot 넓이 조절
        RectTransform rectTransform = GetComponent<RectTransform>();
        int width = 325 * characterManager.characterList.Count;
        rectTransform.sizeDelta = new Vector2(width, 410);

        // List의 크기만큼 Slot을 생성하고 관리를 위해 index 부여, Slots 리스트에 저장
        for (int i = 0; i < characterManager.characterList.Count; i++)
        {
            slotPrefab.index = i;
            slotPrefab.characterData = characterManager.characterList[i];
            slotPrefab.characterSelectSlot = this;
            characterSlots.Add(Instantiate(slotPrefab, this.transform));
        }
    }

    public void RefreshAll() // 모든 슬롯의 상태를 refresh하는 함수
    {
        for(int i = 0; i < characterSlots.Count; i++)
        {
            characterSlots[i].Refresh();
        }
    }
}
