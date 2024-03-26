using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownPlayer : MonoBehaviour
{
    public Character selectedCharacter;

    private void Start()
    {
        PlayerManager.Instance.townPlayer = this.gameObject;

    }

    public void Refresh()
    {
        selectedCharacter = PlayerManager.Instance.selectedCharacter.GetComponent<Character>();

        SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.sprite = selectedCharacter.sprite;
    }
}
