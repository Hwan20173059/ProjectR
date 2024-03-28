using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterBase
{
    public Character character;
    public bool isSelected;
    public bool isBuy;
}

public class CharacterManager : MonoBehaviour
{
    public PlayerManager playerManager;
    public List<CharacterBase> characterList = new List<CharacterBase>();
    public CharacterSelectSlot characterSelectslot;    

    private void Start()
    {
        playerManager = PlayerManager.Instance;

        for (int i = 0; i < characterList.Count; i++)
        {
            if (characterList[i].isSelected == true)
            {
                playerManager.selectedCharacter = characterList[i].character.gameObject;
                break;
            }
        }
    }

    public void SelectCharacter(int index)
    {
        SelectInCharacterList(index); // CharacterList에서 캐릭터 선택
        SelectInCharacterSlot(index); // CharacterSlot에서 캐릭터 선택

        playerManager.ReFreshPlayer(); // TownCharacter도 refresh
        characterSelectslot.RefreshAll(); // Refresh를 통해 UI 상태 조절
    }

    private void SelectInCharacterList(int index)
    {
        for (int i = 0; i < characterList.Count; i++)
            characterList[i].isSelected = false;

        characterList[index].isSelected = true;

        // PlayerManager에 선택한 캐릭터 전달
        playerManager.selectedCharacter = characterList[index].character.gameObject;
    }

    private void SelectInCharacterSlot(int index)
    {
        for (int i = 0; i < characterSelectslot.characterSlots.Count; i++)
            characterSelectslot.characterSlots[i].characterData.isSelected = false;

        characterSelectslot.characterSlots[index].characterData.isSelected = true;
    }
}
