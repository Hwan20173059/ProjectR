using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterData
{
    public Character character;
    public bool isSelected;
    public bool isBuy;
}

public class CharacterManager : MonoBehaviour
{
    public PlayerManager playerManager;
    public List<CharacterData> characterList = new List<CharacterData>();
    public CharacterSelectSlot characterSelectslot;    

    private void Start()
    {
        playerManager = PlayerManager.Instance;
    }

    public void SelectCharacter(int index)
    {
        SelectInCharacterList(index);
        SelectInCharacterSlot(index);

        characterSelectslot.RefreshAll();
    }

    private void SelectInCharacterList(int index)
    {
        for (int i = 0; i < characterList.Count; i++)
            characterList[i].isSelected = false;

        characterList[index].isSelected = true;

        playerManager.selectedCharacter = characterList[index].character;
    }

    private void SelectInCharacterSlot(int index)
    {
        for (int i = 0; i < characterSelectslot.characterSlots.Count; i++)
            characterSelectslot.characterSlots[i].characterData.isSelected = false;

        characterSelectslot.characterSlots[index].characterData.isSelected = true;
    }
}
