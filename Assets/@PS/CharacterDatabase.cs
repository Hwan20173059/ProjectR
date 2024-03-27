using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterDatabase
{
    public List<CharacterBase> characterDatas;
    public Dictionary<int, CharacterBase> characterDic = new();

    public void Initialize()
    {
        foreach (CharacterBase characterBase in characterDatas)
        {
            characterDic.Add(characterBase.id, characterBase);
        }
    }

    public CharacterBase GetCharacterByKey(int id)
    {
        if (characterDic.ContainsKey(id))
            return characterDic[id];

        return null;
    }
}
