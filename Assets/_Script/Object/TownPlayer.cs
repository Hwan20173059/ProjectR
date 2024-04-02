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

        //Refresh()
    }

    public void Refresh()
    {
        spriteRenderer = character.spriteRenderer;
    }
}
