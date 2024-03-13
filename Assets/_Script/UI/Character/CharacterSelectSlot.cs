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
        RectTransform rectTransform = GetComponent<RectTransform>();
        int width = 325 * characterManager.characterList.Count;
        rectTransform.sizeDelta = new Vector2(width, 410);

        for (int i = 0; i < characterManager.characterList.Count; i++)
        {
            slotPrefab.index = i;
            slotPrefab.characterData = characterManager.characterList[i];
            slotPrefab.characterSelectSlot = this;
            characterSlots.Add(Instantiate(slotPrefab, this.transform));
        }
    }

    public void RefreshAll()
    {
        for(int i = 0; i < characterSlots.Count; i++)
        {
            characterSlots[i].Refresh();
        }
    }
}
