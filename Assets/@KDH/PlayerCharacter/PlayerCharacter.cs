using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    public CharacterSO characterSO;

    [Header("Info")]
    public Sprite sprite;
    public string name;
    public string desc;

    [Header("Level")]
    public int maxLevel;
    public int currentLevel;
    public int needExp;
    public int currentExp;

    [Header("Stat")]
    public int maxHp;
    public int currentHp;
    public int Atk;
    public int actionCoolTime;

    [Header("State")]
    public bool isSelected;
    public bool isBuy;

    private void Start()
    {
       // 초기값 설정
    }
}
