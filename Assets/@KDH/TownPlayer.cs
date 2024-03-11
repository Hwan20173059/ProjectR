using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownPlayer : MonoBehaviour
{
    public CharacterSO characterSO;

    private void Start()
    {
        PlayerManager.Instance.playerPrefab = this.gameObject;

        characterSO = PlayerManager.Instance.characterSO;
    }

    public void Refresh()
    {
        characterSO = PlayerManager.Instance.characterSO;

        //Sprite sprite = GetComponentInChildren<Sprite>();
        //sprite = characterSO.sprite;
    }
}
