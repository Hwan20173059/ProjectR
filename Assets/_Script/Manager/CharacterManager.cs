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
    public List<CharacterData> characterList = new List<CharacterData>();
    public Character selectedCharacter;

    public void SelectCharacter(int index)
    {
        for(int i = 0; i < characterList.Count; i++)
            characterList[i].isSelected = false;

        selectedCharacter = characterList[index].character;
        characterList[index].isSelected = true;
    }

    public void BuyCharacter(int index)
    {
        characterList[index].isBuy = true;
    }
}
