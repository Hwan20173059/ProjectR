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
    public PlayerManager playerManager;    

    public FieldState fieldState;
    public Field field;
    public Tile selectedTile;

    public bool isSelect = false;
    public bool isPlayerturn = true;

    public GameObject selectUI;
    public GameObject fieldPlayer;
    public TextMeshProUGUI turnState;
    public TextMeshProUGUI infoUI;

    void Start()
    {
        playerManager = PlayerManager.Instance;

        field.tileRaw[playerManager.fieldY].fieldTiles[playerManager.fieldX].onObject = fieldPlayer;
        field.tileRaw[playerManager.fieldY].fieldTiles[playerManager.fieldX].tileState = TileState.player;
        field.tileRaw[playerManager.fieldY].fieldTiles[playerManager.fieldX].RefreshTile();

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
}
