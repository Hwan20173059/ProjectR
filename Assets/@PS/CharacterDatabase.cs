using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterDatabase
{
    public List<Character> characterDatas;
    public Dictionary<int, Character> characterDic = new();

    public void Initialize()
    {
        foreach (Character character in characterDatas)
        {
            characterDic.Add(character.id, character);
        }
    }

    public Character GetCharacterByKey(int id)
    {
        if (characterDic.ContainsKey(id))
            return characterDic[id];

        return null;
    }
}
