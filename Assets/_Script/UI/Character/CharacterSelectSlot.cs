using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterSelectSlot : MonoBehaviour
{
    public CharacterSlot slotPrefab;
    public List<CharacterSlot> characterSlots = new List<CharacterSlot>();

    public void Init()
    {
        // List�� ũ�⿡ �°� Slot ���� ����
        RectTransform rectTransform = GetComponent<RectTransform>();
        int width = 325 * PlayerManager.Instance.characterList.Count;
        rectTransform.sizeDelta = new Vector2(width, 410);

        // List�� ũ�⸸ŭ Slot�� �����ϰ� ������ ���� index �ο�, Slots ����Ʈ�� ����
        for (int i = 0; i < PlayerManager.Instance.characterList.Count; i++)
        {
            slotPrefab.index = i;
            slotPrefab.characterData = PlayerManager.Instance.characterList[i];
            slotPrefab.characterSelectSlot = this;
            characterSlots.Add(Instantiate(slotPrefab, this.transform));
            characterSlots[i].Init();
        }
    }

    public void RefreshAll() // ��� ������ ���¸� refresh�ϴ� �Լ�
    {
        for(int i = 0; i < characterSlots.Count; i++)
        {
            characterSlots[i].Refresh();
        }
    }
}
