using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownPlayer : MonoBehaviour
{
    public Character character;
    public SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        Refresh();
    }

    public void Refresh()
    {
        if (character != null)
            spriteRenderer.sprite = character.sprite;
    }
}
