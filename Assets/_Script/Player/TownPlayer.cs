using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownPlayer : MonoBehaviour
{
    public AssetCharacter selectedCharacter;

    private void Start()
    {
        PlayerManager.Instance.townPlayer = this.gameObject;

    }

    public void Refresh()
    {
        selectedCharacter = PlayerManager.Instance.selectedCharacter.GetComponent<AssetCharacter>();

        SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.sprite = selectedCharacter.sprite;
    }
}
