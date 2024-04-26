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
        // List�� ũ�⿡ �°� Slot ���� ����
        RectTransform rectTransform = GetComponent<RectTransform>();
        int width = 240 * PlayerManager.Instance.characterList.Count;
        rectTransform.sizeDelta = new Vector2(width, 250);

        // List�� ũ�⸸ŭ Slot�� �����ϰ� ������ ���� index �ο�, Slots ����Ʈ�� ����
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

    public void RefreshAll() // ��� ������ ���¸� refresh�ϴ� �Լ�
    {
        for(int i = 0; i < characterSlots.Count; i++)
        {
            characterSlots[i].Refresh();
        }
    }
}
