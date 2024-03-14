using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterHpBar : MonoBehaviour
{
    private Character character;

    private RectTransform characterMaxHpBar;
    private RectTransform characterCurHpBar;

    public void Init()
    {
        character = GetComponentInParent<Character>();

        characterMaxHpBar = transform.GetChild(0).GetComponent<RectTransform>();
        characterCurHpBar = transform.GetChild(0).GetChild(0).GetComponent<RectTransform>();

        characterMaxHpBar.localScale = new Vector3(character.maxHP / 100f, characterMaxHpBar.localScale.y);
    }

    public void SetHpBar()
    {
        characterCurHpBar.localScale = new Vector3((float)character.curHP / character.maxHP, characterCurHpBar.localScale.y);
    }
}
