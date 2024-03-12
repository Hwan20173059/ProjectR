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
}
