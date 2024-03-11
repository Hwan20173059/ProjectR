using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Scriptable Object/CharacterData")]
public class CharacterSO : ScriptableObject
{
    public Sprite sprite;
    public string name;
    public int hp;
    public int atk;
    public int needExp;
    public int maxLevel;
    public float actionCoolTime;

    public bool isSelected;
    public bool isBuy;
}
