using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;

public enum FieldState
{
    playerTurn,
    fieldTurn
}

public class FieldManager : MonoBehaviour
{
    [Header("Manager")]
    public PlayerManager playerManager;    
    public Field field;
    public Tile selectedTile;

    [Header("State")]
    public FieldState fieldState;
    public bool isSelect = false;
    public bool isPlayerturn = true;

    [Header("Player")]
    public GameObject fieldPlayer;

    [Header("Monster")]
    public List<GameObject> fieldMonster = new List<GameObject>();

    [Header("UI")]
    public TextMeshProUGUI turnState;
    public GameObject selectUI;
    public TextMeshProUGUI infoUI;

    

    void Start()
    {
        playerManager = PlayerManager.Instance;

        //PlayerSpriteSetting(playerManager);
        PlayerFieldSetting(playerManager.fieldX, playerManager.fieldY);
        SpawnRandomMonster(4);

        PlayerTurn();
    }

    void PlayerTurn()
    {
        fieldState = FieldState.playerTurn;
        turnState.text = "ÇÃ·¹ÀÌ¾î Â÷·Ê";
        isPlayerturn = true;
    }

    public void AEnemyTurn()
    {
        StartCoroutine(EnemyTurn());
    }
    
    IEnumerator EnemyTurn()
    {
        fieldState = FieldState.fieldTurn;
        turnState.text = "ÇÊµå Â÷·Ê";
        isPlayerturn = false;

        //EnemyTurn

        yield return new WaitForSeconds(2f);
        PlayerTurn();
    }

    private void PlayerSpriteSetting(PlayerManager playermanager)
    {
        SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.sprite = playermanager.selectedCharacter.GetComponent<Character>().sprite;
    }

    private void PlayerFieldSetting(int x, int y)
    {
        field.tileRaw[y].fieldTiles[x].onObject = fieldPlayer;
        field.tileRaw[y].fieldTiles[x].tileState = TileState.player;
        field.tileRaw[y].fieldTiles[x].RefreshTile();
    }

    public void PlayerMoveTile()
    {
        for (int i =0; i < field.tileRaw.Length; i++)
        {
            for(int j = 0; j < field.tileRaw[i].fieldTiles.Length; j++)
            {
                if (field.tileRaw[i].fieldTiles[j].tileState == TileState.player)
                {
                    field.tileRaw[i].fieldTiles[j].ClearTile();
                    field.tileRaw[i].fieldTiles[j].RefreshTile();
                }
            }
        }
    }

    private void SpawnRandomMonster(int index)
    {

    }
}
