using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterHpBar : MonoBehaviour
{
    Character character;

    RectTransform characterMaxHpBar;
    RectTransform characterCurHpBar;

    private void Update()
    {
        SetHpBarPosition();
    }

    public void Init(Character character)
    {
        this.character = character;
        characterMaxHpBar = transform.GetChild(0).GetComponent<RectTransform>();
        characterCurHpBar = transform.GetChild(0).GetChild(0).GetComponent<RectTransform>();

        characterMaxHpBar.localScale = new Vector3(character.maxHP / 100f, characterMaxHpBar.localScale.y);
    }

    public void SetHpBar()
    {
        characterCurHpBar.localScale = new Vector3((float)character.curHP / character.maxHP, characterCurHpBar.localScale.y);
    }

    void SetHpBarPosition()
    {
        if (character != null)
        {
            Vector2 screenPos = Camera.main.WorldToScreenPoint(character.transform.position);
            transform.position = new Vector2(screenPos.x, screenPos.y + 130f);
        }
    }
}
