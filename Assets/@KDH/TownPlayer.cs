using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownPlayer : MonoBehaviour
{
    public PlayerCharacter selectedCharacter;

    private void Start()
    {
        PlayerManager.Instance.townPlayer = this.gameObject;

        selectedCharacter = PlayerManager.Instance.selectedCharacter;
    }

    public void Refresh()
    {
        selectedCharacter = PlayerManager.Instance.selectedCharacter;

        SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.sprite = selectedCharacter.sprite;
    }
}
