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
    public List<Tile> fieldMonster = new List<Tile>();

    [Header("UI")]
    public TextMeshProUGUI turnState;
    public GameObject selectUI;
    public TextMeshProUGUI infoUI;

    

    void Start()
    {
        playerManager = PlayerManager.Instance;

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

        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < fieldMonster.Count; i++)
        {
            int randomMove = Random.Range(0, 4);
            int X = fieldMonster[i].indexX;
            int Y = fieldMonster[i].indexY;

            switch (randomMove)
            {
                case 0:
                    if (X - 1 >= 0 && field.tileRaw[Y].fieldTiles[X - 1].tileState == TileState.empty)
                    {
                        field.tileRaw[Y].fieldTiles[X].ClearTile();
                        field.tileRaw[Y].fieldTiles[X].RefreshTile();
                        fieldMonster[i] = field.tileRaw[Y].fieldTiles[X - 1];

                        MonsterFieldSetting(X - 1, Y);
                        field.tileRaw[Y].fieldTiles[X - 1].RefreshTile();
                    }
                    break;

                case 1:
                    if (X + 1 < 9 && field.tileRaw[Y].fieldTiles[X + 1].tileState == TileState.empty)
                    {
                        field.tileRaw[Y].fieldTiles[X].ClearTile();
                        field.tileRaw[Y].fieldTiles[X].RefreshTile();
                        fieldMonster[i] = field.tileRaw[Y].fieldTiles[X + 1];

                        MonsterFieldSetting(X + 1, Y);
                        field.tileRaw[Y].fieldTiles[X + 1].RefreshTile();
                    }
                    break;

                case 2:
                    if (Y - 1 >= 0 && field.tileRaw[Y - 1].fieldTiles[X].tileState == TileState.empty)
                    {
                        field.tileRaw[Y].fieldTiles[X].ClearTile();
                        field.tileRaw[Y].fieldTiles[X].RefreshTile();
                        fieldMonster[i] = field.tileRaw[Y - 1].fieldTiles[X];

                        MonsterFieldSetting(X, Y - 1);
                        field.tileRaw[Y - 1].fieldTiles[X].RefreshTile();
                    }
                    break;

                case 3:
                    if (Y + 1 < 9 && field.tileRaw[Y + 1].fieldTiles[X].tileState == TileState.empty)
                    {
                        field.tileRaw[Y].fieldTiles[X].ClearTile();
                        field.tileRaw[Y].fieldTiles[X].RefreshTile();
                        fieldMonster[i] = field.tileRaw[Y + 1].fieldTiles[X];

                        MonsterFieldSetting(X, Y + 1);
                        field.tileRaw[Y + 1].fieldTiles[X].RefreshTile();
                    }
                    break;
            }

            yield return new WaitForSeconds(0.2f);
        }

        yield return new WaitForSeconds(0.5f);
        PlayerTurn();
    }

    private void PlayerFieldSetting(int x, int y)
    {
        field.tileRaw[y].fieldTiles[x].onObject = fieldPlayer;
        field.tileRaw[y].fieldTiles[x].tileState = TileState.player;
        field.tileRaw[y].fieldTiles[x].RefreshTile();
    }

    private void MonsterFieldSetting(int x, int y)
    {
        field.tileRaw[y].fieldTiles[x].tileState = TileState.monster;
        field.tileRaw[y].fieldTiles[x].RefreshTile();
    }

    public void PlayerMoveTile()
    {
        for (int i =0; i < field.tileRaw.Length; i++)
        {
            for(int j = 0; j < field.tileRaw[i].fieldTiles.Length; j++)
            {
                if (field.tileRaw[i].fieldTiles[j].tileState == TileState.player || field.tileRaw[i].fieldTiles[j].tileState == TileState.cango)
                {
                    field.tileRaw[i].fieldTiles[j].ClearTile();
                    field.tileRaw[i].fieldTiles[j].RefreshTile();
                }
                else if (field.tileRaw[i].fieldTiles[j].tileState == TileState.canfight)
                {
                    field.tileRaw[i].fieldTiles[j].tileState = TileState.monster;
                    field.tileRaw[i].fieldTiles[j].RefreshTile();
                }
            }
        }
    }

    private void SpawnRandomMonster(int index)
    {
        for (int i = 0; i < index; i++)
        {
            int randomY = Random.Range(0, field.tileRaw.Length);
            int randomX = Random.Range(0, field.tileRaw[randomY].fieldTiles.Length);

            if (field.tileRaw[randomY].fieldTiles[randomX].tileState == TileState.empty)
            {
                fieldMonster.Add(field.tileRaw[randomY].fieldTiles[randomX]);
                
                MonsterFieldSetting(randomX, randomY);
            }
            else
            {
                SpawnRandomMonster(1);
            }
        }
    }

    public void MoveTileOn()
    {
        int X = selectedTile.indexX;
        int Y = selectedTile.indexY;

        if (X - 1 >= 0 && field.tileRaw[Y].fieldTiles[X - 1].tileState == TileState.empty)
        {
            field.tileRaw[Y].fieldTiles[X - 1].tileState = TileState.cango;
            field.tileRaw[Y].fieldTiles[X - 1].RefreshTile();
        }
        else if (X - 1 >= 0 && field.tileRaw[Y].fieldTiles[X - 1].tileState == TileState.monster)
        {
            field.tileRaw[Y].fieldTiles[X - 1].tileState = TileState.canfight;
            field.tileRaw[Y].fieldTiles[X - 1].RefreshTile();
        }

        if (X + 1 < 9 && field.tileRaw[Y].fieldTiles[X + 1].tileState == TileState.empty)
        {
            field.tileRaw[Y].fieldTiles[X + 1].tileState = TileState.cango;
            field.tileRaw[Y].fieldTiles[X + 1].RefreshTile();
        }
        else if (X + 1 < 9 && field.tileRaw[Y].fieldTiles[X + 1].tileState == TileState.monster)
        {
            field.tileRaw[Y].fieldTiles[X + 1].tileState = TileState.canfight;
            field.tileRaw[Y].fieldTiles[X + 1].RefreshTile();
        }

        if (Y - 1 >= 0 && field.tileRaw[Y - 1].fieldTiles[X].tileState == TileState.empty)
        {
            field.tileRaw[Y - 1].fieldTiles[X].tileState = TileState.cango;
            field.tileRaw[Y - 1].fieldTiles[X].RefreshTile();
        }
        else if (Y - 1 >= 0 && field.tileRaw[Y - 1].fieldTiles[X].tileState == TileState.monster)
        {
            field.tileRaw[Y - 1].fieldTiles[X].tileState = TileState.canfight;
            field.tileRaw[Y - 1].fieldTiles[X].RefreshTile();
        }

        if (Y + 1 < 9 && field.tileRaw[Y + 1].fieldTiles[X].tileState == TileState.empty)
        {
            field.tileRaw[Y + 1].fieldTiles[X].tileState = TileState.cango;
            field.tileRaw[Y + 1].fieldTiles[X].RefreshTile();
        }
        else if (Y + 1 < 9 && field.tileRaw[Y + 1].fieldTiles[X].tileState == TileState.monster)
        {
            field.tileRaw[Y + 1].fieldTiles[X].tileState = TileState.canfight;
            field.tileRaw[Y + 1].fieldTiles[X].RefreshTile();
        }
    }

    public void MoveTileOff()
    {
        int X = selectedTile.indexX;
        int Y = selectedTile.indexY;

        if (X - 1 >= 0 && field.tileRaw[Y].fieldTiles[X - 1].tileState == TileState.cango)
        {
            field.tileRaw[Y].fieldTiles[X - 1].tileState = TileState.empty;
            field.tileRaw[Y].fieldTiles[X - 1].RefreshTile();
        }
        else if (X - 1 >= 0 && field.tileRaw[Y].fieldTiles[X - 1].tileState == TileState.canfight)
        {
            field.tileRaw[Y].fieldTiles[X - 1].tileState = TileState.monster;
            field.tileRaw[Y].fieldTiles[X - 1].RefreshTile();
        }

        if (X + 1 < 9 && field.tileRaw[Y].fieldTiles[X + 1].tileState == TileState.cango)
        {
            field.tileRaw[Y].fieldTiles[X + 1].tileState = TileState.empty;
            field.tileRaw[Y].fieldTiles[X + 1].RefreshTile();
        }
        else if (X + 1 < 9 && field.tileRaw[Y].fieldTiles[X + 1].tileState == TileState.canfight)
        {
            field.tileRaw[Y].fieldTiles[X + 1].tileState = TileState.monster;
            field.tileRaw[Y].fieldTiles[X + 1].RefreshTile();
        }

        if (Y - 1 >= 0 && field.tileRaw[Y - 1].fieldTiles[X].tileState == TileState.cango)
        {
            field.tileRaw[Y - 1].fieldTiles[X].tileState = TileState.empty;
            field.tileRaw[Y - 1].fieldTiles[X].RefreshTile();
        }
        else if (Y - 1 >= 0 && field.tileRaw[Y - 1].fieldTiles[X].tileState == TileState.canfight)
        {
            field.tileRaw[Y - 1].fieldTiles[X].tileState = TileState.monster;
            field.tileRaw[Y - 1].fieldTiles[X].RefreshTile();
        }

        if (Y + 1 < 9 && field.tileRaw[Y + 1].fieldTiles[X].tileState == TileState.cango)
        {
            field.tileRaw[Y + 1].fieldTiles[X].tileState = TileState.empty;
            field.tileRaw[Y + 1].fieldTiles[X].RefreshTile();
        }
        else if (Y + 1 < 9 && field.tileRaw[Y + 1].fieldTiles[X].tileState == TileState.canfight)
        {
            field.tileRaw[Y + 1].fieldTiles[X].tileState = TileState.monster;
            field.tileRaw[Y + 1].fieldTiles[X].RefreshTile();
        }
    }
}
