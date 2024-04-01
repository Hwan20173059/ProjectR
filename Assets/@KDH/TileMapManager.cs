using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR;

public enum FieldState
{
    playerTurn,
    fieldTurn
}

public class TileMapManager : MonoBehaviour
{
    [Header("Manager")]
    public PlayerManager playerManager;
    public Field field;
    public Tile currentTile;

    [Header("State")]
    public FieldState fieldState;
    public int fieldSizeX;
    public int fieldSizeY;
    public int playerTurnIndex;
    public int[] playerPosition = new int[2];
    public bool isSelect = false;
    public bool isPlayerturn = true;

    [Header("FieldObject")]
    public GameObject playerPrefab;
    public GameObject slimePrefab;
    public GameObject monsterPrefab;
    public GameObject chest0Prefab;
    public GameObject chest1Prefab;

    [Header("Monster")]
    public List<Tile> fieldMonster = new List<Tile>();
    public Tile chestPosition;

    [Header("UI")]
    public TextMeshProUGUI turnState;
    public GameObject selectUI;
    public TextMeshProUGUI infoUI;
    public GameObject ChestUI;

    [Header("Path Finding")]
    public List<Tile> movableTile;  


    public void PlayerTurn()
    {
        fieldState = FieldState.playerTurn;
        turnState.text = "ÇÃ·¹ÀÌ¾î Â÷·Ê\n" + "³²Àº È½¼ö " + playerTurnIndex;
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

        playerTurnIndex = playerManager.playerTurnIndex;
        PlayerTurn();
    }

    protected void MoveTileMonsterState(int monsterIndex, int X, int Y, int MoveX, int MoveY)
    {
        TileStateSetting(X, Y, TileState.empty);

        field.tileRaw[MoveY].fieldTiles[MoveX].battleID = field.tileRaw[Y].fieldTiles[X].battleID;

        fieldMonster[monsterIndex] = field.tileRaw[MoveY].fieldTiles[MoveX];

        MonsterFieldSetting(field.tileRaw[Y].fieldTiles[X].battleID, MoveX, MoveY);

        field.tileRaw[Y].fieldTiles[X].battleID = 0;
    }

    protected void PlayerFieldSetting(int x, int y)
    {
        field.tileRaw[y].fieldTiles[x].tileState = TileState.player;
        playerPosition[0] = x;
        playerPosition[1] = y;
        field.tileRaw[y].fieldTiles[x].RefreshTile();
    }

    protected void MonsterFieldSetting(int battleID, int x, int y)
    {
        field.tileRaw[y].fieldTiles[x].battleID = battleID;
        TileStateSetting(x, y, TileState.monster);
    }

    public void FieldMoveAfter()
    {
        for (int i = 0; i < field.tileRaw.Length; i++)
        {
            for (int j = 0; j < field.tileRaw[i].fieldTiles.Length; j++)
            {
                ChangeTileState(j, i, TileState.player, TileState.empty);
                ChangeTileState(j, i, TileState.canGo, TileState.empty);
                ChangeTileState(j, i, TileState.canFight, TileState.monster);
                ChangeTileState(j, i, TileState.canTownEnter, TileState.town);
                ChangeTileState(j, i, TileState.canDungeonEnter, TileState.dungeon);
                ChangeTileState(j, i, TileState.canOpenChest, TileState.chest);

                field.tileRaw[i].fieldTiles[j].cost = 0;
            }
        }
    }

    protected void SpawnRandomMonster(int index)
    {
        for (int i = 0; i < index; i++)
        {
            int randomY = UnityEngine.Random.Range(0, field.tileRaw.Length);
            int randomX = UnityEngine.Random.Range(0, field.tileRaw[randomY].fieldTiles.Length);

            if (field.tileRaw[randomY].fieldTiles[randomX].tileState == TileState.empty)
            {
                int randomID = UnityEngine.Random.Range(0, 2);

                fieldMonster.Add(field.tileRaw[randomY].fieldTiles[randomX]);
                field.tileRaw[randomY].fieldTiles[randomX].battleID = randomID;
                MonsterFieldSetting(randomID, randomX, randomY);
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
            playerManager.monsterPosition.Add(fieldMonster[i].battleID);
            playerManager.monsterPosition.Add(fieldMonster[i].indexX);
            playerManager.monsterPosition.Add(fieldMonster[i].indexY);
        }
    }

    protected void LoadMonster()
    {
        for (int i = 0; i < playerManager.monsterPosition.Count; i += 3)
        {
            int Y = playerManager.monsterPosition[i + 2];
            int X = playerManager.monsterPosition[i + 1];

            fieldMonster.Add(field.tileRaw[Y].fieldTiles[X]);
            MonsterFieldSetting(playerManager.monsterPosition[i], X, Y);
        }
    }

    protected void ChestSetting(int chestID, int x, int y)
    {
        field.tileRaw[y].fieldTiles[x].chestID = chestID;
        TileStateSetting(x, y, TileState.chest);
    }

    protected void LoadChest()
    {
        chestPosition = field.tileRaw[playerManager.chestPosition[2]].fieldTiles[playerManager.chestPosition[1]];
        ChestSetting(playerManager.chestPosition[0], playerManager.chestPosition[1], playerManager.chestPosition[2]);
    }

    public void SaveChest()
    {
        playerManager.chestPosition = new List<int>();

        playerManager.chestPosition.Add(chestPosition.chestID);
        playerManager.chestPosition.Add(chestPosition.indexX);
        playerManager.chestPosition.Add(chestPosition.indexY);
    }

    public void MoveTileOn(int index)
    {
        movableTile = new List<Tile>();

        movableTile.Add(currentTile);
        currentTile.cost = 0;

        for (int i = 1; i <= index; i++)
        {
            int count = movableTile.Count;

            for (int j = 0; j < count; j++)
            {
                int X = movableTile[j].indexX;
                int Y = movableTile[j].indexY;

                TileAdd(movableTile, X + 1, Y);
                TileOn(X + 1, Y, i);

                TileAdd(movableTile, X - 1, Y);
                TileOn(X - 1, Y, i);

                TileAdd(movableTile, X, Y + 1);
                TileOn(X, Y + 1, i);

                TileAdd(movableTile, X, Y - 1);
                TileOn(X, Y - 1, i);
            }
        }
    }

    public void MoveTileOff(int index)
    {
        int X = currentTile.indexX;
        int Y = currentTile.indexY;

        for (int i = -index; i <= index; i++)
            for (int j = -index + Math.Abs(i); j <= index - Math.Abs(i); j++) TileOff(X + j, Y + i);
    }

    void TileAdd(List<Tile> tileList, int X, int Y)
    {
        if (X > -1 && X < fieldSizeX && Y > -1 && Y < fieldSizeY && TileStateCheck(X, Y, TileState.empty))
            tileList.Add(field.tileRaw[Y].fieldTiles[X]);
    }

    void TileOn(int X, int Y, int index)
    {
        ChangeTileState(X, Y, TileState.empty, TileState.canGo);
        ChangeTileState(X, Y, TileState.monster, TileState.canFight);
        ChangeTileState(X, Y, TileState.town, TileState.canTownEnter);
        ChangeTileState(X, Y, TileState.dungeon, TileState.canDungeonEnter);
        ChangeTileState(X, Y, TileState.chest, TileState.canOpenChest);

        if (X > -1 && X < fieldSizeX && Y > -1 && Y < fieldSizeY)
        {
            if (field.tileRaw[Y].fieldTiles[X].cost == 0)
                field.tileRaw[Y].fieldTiles[X].cost = index;
        }
    }

    void TileOff(int X, int Y) 
    {
        ChangeTileState(X, Y, TileState.canGo, TileState.empty);
        ChangeTileState(X, Y, TileState.canFight, TileState.monster);
        ChangeTileState(X, Y, TileState.canTownEnter, TileState.town);
        ChangeTileState(X, Y, TileState.canDungeonEnter, TileState.dungeon);
        ChangeTileState(X, Y, TileState.canOpenChest, TileState.chest);

        if (X > -1 && X < fieldSizeX && Y > -1 && Y < fieldSizeY)
            field.tileRaw[Y].fieldTiles[X].cost = 0;
    }

    private void ChangeTileState(int X, int Y, TileState beforeState, TileState changState)
    {
        if (X > -1 && X < fieldSizeX && Y > -1 && Y < fieldSizeY && TileStateCheck(X, Y, beforeState))
            TileStateSetting(X, Y, changState);
    }

    protected virtual bool FieldStateEmptyCheck(int X, int Y) 
    {
        return X > -1 && X < fieldSizeX && Y > -1 && Y < fieldSizeY && field.tileRaw[Y].fieldTiles[X].tileState == TileState.empty;
    }

    protected bool TileStateCheck(int X, int Y, TileState tileState)
    {
        return field.tileRaw[Y].fieldTiles[X].tileState == tileState;
    }

    protected void TileStateSetting(int X, int Y, TileState tileState)
    {
        field.tileRaw[Y].fieldTiles[X].tileState = tileState;
        field.tileRaw[Y].fieldTiles[X].RefreshTile();
    }

    public void ChestUIExitButton()
    {
        ChestUI.SetActive(false);

        if (playerTurnIndex > 0)
            PlayerTurn();
        else
        {
            playerTurnIndex = playerManager.playerTurnIndex;
            AEnemyTurn();
        }
    }

    protected void AllRefreshTile()
    {
        for (int i = 0; i < field.tileRaw.Length; i++)
        {
            for (int j = 0; j < field.tileRaw[i].fieldTiles.Length; j++)
            {
                field.tileRaw[i].fieldTiles[j].RefreshTile();
            }
        }
    }
}
