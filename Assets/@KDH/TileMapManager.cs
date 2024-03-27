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

public class TileMapManager : MonoBehaviour
{
    [Header("Manager")]
    public PlayerManager playerManager;    
    public Field field;
    public Tile selectedTile;

    [Header("State")]
    public FieldState fieldState;
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
        PlayerTurn();
    }

    protected void MoveTileMonsterState(int monsterIndex, int X, int Y, int MoveX, int MoveY)
    {
        field.tileRaw[Y].fieldTiles[X].SetTile(TileState.empty);        
        field.tileRaw[Y].fieldTiles[X].RefreshTile();

        field.tileRaw[MoveY].fieldTiles[MoveX].battleID = field.tileRaw[Y].fieldTiles[X].battleID;

        fieldMonster[monsterIndex] = field.tileRaw[MoveY].fieldTiles[MoveX];

        MonsterFieldSetting(field.tileRaw[Y].fieldTiles[X].battleID, MoveX, MoveY);

        field.tileRaw[Y].fieldTiles[X].battleID = 0;
    }

    protected void PlayerFieldSetting(int x, int y)
    {
        field.tileRaw[y].fieldTiles[x].tileState = TileState.player;
        field.tileRaw[y].fieldTiles[x].RefreshTile();
    }

    protected void MonsterFieldSetting(int battleID, int x, int y)
    {
        field.tileRaw[y].fieldTiles[x].battleID = battleID;
        field.tileRaw[y].fieldTiles[x].tileState = TileState.monster;
        field.tileRaw[y].fieldTiles[x].RefreshTile();
    }

    public void FieldMoveAfter()
    {
        for (int i =0; i < field.tileRaw.Length; i++)
        {
            for(int j = 0; j < field.tileRaw[i].fieldTiles.Length; j++)
            {
                if (field.tileRaw[i].fieldTiles[j].tileState == TileState.player || field.tileRaw[i].fieldTiles[j].tileState == TileState.canGO)
                {
                    field.tileRaw[i].fieldTiles[j].SetTile(TileState.empty);
                    field.tileRaw[i].fieldTiles[j].RefreshTile();
                }
                else if (field.tileRaw[i].fieldTiles[j].tileState == TileState.canFight)
                {
                    field.tileRaw[i].fieldTiles[j].tileState = TileState.monster;
                    field.tileRaw[i].fieldTiles[j].RefreshTile();
                }
                else if (field.tileRaw[i].fieldTiles[j].tileState == TileState.canTownEnter)
                {
                    field.tileRaw[i].fieldTiles[j].tileState = TileState.town;
                    field.tileRaw[i].fieldTiles[j].RefreshTile();
                }
                else if (field.tileRaw[i].fieldTiles[j].tileState == TileState.canDungeonEnter)
                {
                    field.tileRaw[i].fieldTiles[j].tileState = TileState.dungeon;
                    field.tileRaw[i].fieldTiles[j].RefreshTile();
                }
                else if (field.tileRaw[i].fieldTiles[j].tileState == TileState.canOpenChest)
                {
                    field.tileRaw[i].fieldTiles[j].tileState = TileState.chest;
                    field.tileRaw[i].fieldTiles[j].RefreshTile();
                }
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
        field.tileRaw[y].fieldTiles[x].tileState = TileState.chest;
        field.tileRaw[y].fieldTiles[x].RefreshTile();
    }

    protected void LoadChest()
    {
        chestPosition = field.tileRaw[playerManager.chestPosition[2]].fieldTiles[playerManager.chestPosition[1]];
        ChestSetting(playerManager.chestPosition[0], playerManager.chestPosition[1], playerManager.chestPosition[2]);
    }

    public void SaveChest()
    {
        playerManager.chestPosition.Add(chestPosition.chestID);
        playerManager.chestPosition.Add(chestPosition.indexX);
        playerManager.chestPosition.Add(chestPosition.indexY);
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

    protected virtual void TileOn(int X, int Y) { }
    protected virtual void TileOff(int X, int Y) { }
    protected virtual bool FieldStateEmptyCheck(int X, int Y) { return false; }

    protected bool TileStateCheck(int X, int Y, TileState tileState)
    {
        return field.tileRaw[Y].fieldTiles[X].tileState == tileState;
    }

    protected void TileStateSetting(int X, int Y, TileState tileState)
    {
        field.tileRaw[Y].fieldTiles[X].tileState = tileState;
        field.tileRaw[Y].fieldTiles[X].RefreshTile();
    }

    protected void AllRefreshTile()
    {
        for(int i = 0; i < field.tileRaw.Length; i++)
        {
            for(int j = 0; j < field.tileRaw[i].fieldTiles.Length; j++)
            {
                field.tileRaw[i].fieldTiles[j].RefreshTile();
            }
        }
    }
}
