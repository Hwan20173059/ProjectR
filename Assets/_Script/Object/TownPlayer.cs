using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownPlayer : MonoBehaviour
{
    public Character character;
    public SpriteRenderer spriteRenderer;

    public void init()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        character = PlayerManager.Instance.characterList[PlayerManager.Instance.selectedCharacterIndex];

        Refresh();
    }

    public void Refresh()
    {
        if (character != null)
            spriteRenderer.sprite = character.sprite;
    }
}
