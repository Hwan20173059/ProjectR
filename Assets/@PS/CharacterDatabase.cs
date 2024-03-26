using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterDatabase
{
    public List<AssetCharacter> characterDatas;
    public Dictionary<int, AssetCharacter> characterDic = new();

    public void Initialize()
    {
        foreach (AssetCharacter character in characterDatas)
        {
            characterDic.Add(character.id, character);
        }
    }

    public AssetCharacter GetCharacterByKey(int id)
    {
        if (characterDic.ContainsKey(id))
            return characterDic[id];

        return null;
    }
}
