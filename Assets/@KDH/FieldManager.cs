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
    public int[] playerPosition = new int[2];
    public bool isSelect = false;
    public bool isPlayerturn = true;

    [Header("Player")]
    public GameObject playerPrefab;
    public GameObject monsterPrefab;

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

        if (playerManager.isEnterTown == true)
        {
            playerManager.monsterPosition = new List<int>();
            SpawnRandomMonster(4);
        }
        else
        {
            LoadMonster();
        }

        PlayerTurn();
    }

    void PlayerTurn()
    {
        fieldState = FieldState.playerTurn;
        turnState.text = "�÷��̾� ����";
        isPlayerturn = true;
    }

    public void AEnemyTurn()
    {
        StartCoroutine(EnemyTurn());
    }
    
    IEnumerator EnemyTurn()
    {
        fieldState = FieldState.fieldTurn;
        turnState.text = "�ʵ� ����";
        isPlayerturn = false;

        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < fieldMonster.Count; i++)
        {
            int randomMove = UnityEngine.Random.Range(0, 4);
            int X = fieldMonster[i].indexX;
            int Y = fieldMonster[i].indexY;

            switch (randomMove)
            {
                case 0:
                    if (FieldStateEmptyCheck(X - 1, Y))
                        MoveTileMonsterState(i, X, Y, X - 1, Y);
                    break;

                case 1:
                    if (FieldStateEmptyCheck(X + 1, Y))
                        MoveTileMonsterState(i, X, Y, X + 1, Y);
                    break;

                case 2:
                    if (FieldStateEmptyCheck(X, Y - 1))
                        MoveTileMonsterState(i, X, Y, X, Y - 1);
                    break;

                case 3:
                    if (FieldStateEmptyCheck(X, Y + 1))
                        MoveTileMonsterState(i, X, Y, X, Y + 1);
                    break;
            }

            yield return new WaitForSeconds(0.2f);
        }

        yield return new WaitForSeconds(0.5f);
        PlayerTurn();
    }

    private void MoveTileMonsterState(int monsterIndex, int X, int Y, int MoveX, int MoveY)
    {
        field.tileRaw[Y].fieldTiles[X].SetTile(TileState.empty);
        field.tileRaw[Y].fieldTiles[X].RefreshTile();

        fieldMonster[monsterIndex] = field.tileRaw[MoveY].fieldTiles[MoveX];
        MonsterFieldSetting(MoveX, MoveY);
    }

    private bool FieldStateEmptyCheck(int X, int Y)
    {
        return X > -1 && X < 9 && Y > -1 && Y < 9 && field.tileRaw[Y].fieldTiles[X].tileState == TileState.empty;
    }

    private void PlayerFieldSetting(int x, int y)
    {
        field.tileRaw[y].fieldTiles[x].tileState = TileState.player;
        field.tileRaw[y].fieldTiles[x].RefreshTile();
    }

    private void MonsterFieldSetting(int x, int y)
    {
        field.tileRaw[y].fieldTiles[x].tileState = TileState.monster;
        field.tileRaw[y].fieldTiles[x].RefreshTile();
    }

    public void FieldMoveAfter()
    {
        for (int i =0; i < field.tileRaw.Length; i++)
        {
            for(int j = 0; j < field.tileRaw[i].fieldTiles.Length; j++)
            {
                if (field.tileRaw[i].fieldTiles[j].tileState == TileState.player || field.tileRaw[i].fieldTiles[j].tileState == TileState.cango)
                {
                    field.tileRaw[i].fieldTiles[j].SetTile(TileState.empty);
                    field.tileRaw[i].fieldTiles[j].RefreshTile();
                }
                else if (field.tileRaw[i].fieldTiles[j].tileState == TileState.canfight)
                {
                    field.tileRaw[i].fieldTiles[j].tileState = TileState.monster;
                    field.tileRaw[i].fieldTiles[j].RefreshTile();
                }
                else if (field.tileRaw[i].fieldTiles[j].tileState == TileState.canEnter)
                {
                    field.tileRaw[i].fieldTiles[j].tileState = TileState.town;
                    field.tileRaw[i].fieldTiles[j].RefreshTile();
                }
            }
        }
    }

    private void SpawnRandomMonster(int index)
    {
        for (int i = 0; i < index; i++)
        {
            int randomY = UnityEngine.Random.Range(0, field.tileRaw.Length);
            int randomX = UnityEngine.Random.Range(0, field.tileRaw[randomY].fieldTiles.Length);

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

    public void SaveMonster()
    {
        playerManager.monsterPosition = new List<int>();

        for (int i = 0; i < fieldMonster.Count; i++)
        {
            playerManager.monsterPosition.Add(fieldMonster[i].indexX);
            playerManager.monsterPosition.Add(fieldMonster[i].indexY);
        }
    }

    private void LoadMonster()
    {
        for (int i = 0; i < playerManager.monsterPosition.Count; i += 2)
        {
            int Y = playerManager.monsterPosition[i + 1];
            int X = playerManager.monsterPosition[i];

            fieldMonster.Add(field.tileRaw[Y].fieldTiles[X]);
            MonsterFieldSetting(X, Y);
        }
    }

    public void MoveTileOn()
    {
        int X = selectedTile.indexX;
        int Y = selectedTile.indexY;

        TileOn(X - 1, Y);
        TileOn(X + 1, Y);
        TileOn(X, Y - 1);
        TileOn(X, Y + 1);
    }

    public void MoveTileOff()
    {
        int X = selectedTile.indexX;
        int Y = selectedTile.indexY;

        TileOff(X - 1, Y);
        TileOff(X + 1, Y);
        TileOff(X, Y - 1);
        TileOff(X, Y + 1);
    }

    private void TileOn(int X, int Y)
    {
        if (X > -1 && X < 9 && Y > -1 && Y < 9 && TileStateCheck(X, Y, TileState.empty))
            TileStateSetting(X, Y, TileState.cango);
        else if (X > -1 && X < 9 && Y > -1 && Y < 9 && TileStateCheck(X, Y, TileState.monster))
            TileStateSetting(X, Y, TileState.canfight);
        else if (X > -1 && X < 9 && Y > -1 && Y < 9 && TileStateCheck(X, Y, TileState.town))
            TileStateSetting(X, Y, TileState.canEnter);
    }

    private void TileOff(int X, int Y)
    {
        if (X > -1 && X < 9 && Y > -1 && Y < 9 && TileStateCheck(X, Y, TileState.cango))
            TileStateSetting(X, Y, TileState.empty);
        else if (X > -1 && X < 9 && Y > -1 && Y < 9 && TileStateCheck(X, Y, TileState.canfight))
            TileStateSetting(X, Y, TileState.monster);
        else if (X > -1 && X < 9 && Y > -1 && Y < 9 && TileStateCheck(X, Y, TileState.canEnter))
            TileStateSetting(X, Y, TileState.town);
    }

    private bool TileStateCheck(int X, int Y, TileState tileState)
    {
        return field.tileRaw[Y].fieldTiles[X].tileState == tileState;
    }

    private void TileStateSetting(int X, int Y, TileState tileState)
    {
        field.tileRaw[Y].fieldTiles[X].tileState = tileState;
        field.tileRaw[Y].fieldTiles[X].RefreshTile();
    }
}
