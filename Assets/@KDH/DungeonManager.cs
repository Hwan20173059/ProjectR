using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonManager : TileMapManager
{
    public GameObject dungeonClearUI;

    void Start()
    {
        playerManager = PlayerManager.Instance;
        playerTurnIndex = 3;

        if (playerManager.isDungeon == false)
        {
            CreateRandomMap();
            AllRefreshTile();

            PlayerFieldSetting(0, 0);

            playerManager.monsterPosition = new List<int>();
            SpawnRandomMonster(3);
            RandomSpawnChest();
        }
        else
        {
            if (playerManager.monsterPosition.Count > 0)
            {
                LoadRandomMap(playerManager.dungeonMap);
                AllRefreshTile();

                PlayerFieldSetting(playerManager.fieldX, playerManager.fieldY);

                LoadMonster();
                LoadChest();
            }
            else
            {
                ClearDungeon();
            }
        }

        playerManager.isField = false;
        playerManager.isTown = false;
        playerManager.isDungeon = true;

        PlayerTurn();
    }


    public void ExitButton()
    {
        if (playerManager.selectDungeonID == 0)
        {
            playerManager.fieldX = 5;
            playerManager.fieldY = 1;
        }
        else if (playerManager.selectDungeonID == 1)
        {
            playerManager.fieldX = 2;
            playerManager.fieldY = 7;
        }
        else if (playerManager.selectDungeonID == 2)
        {
            playerManager.fieldX = 8;
            playerManager.fieldY = 7;
        }

        SceneManager.LoadScene("FieldScene");
    }

    protected override void TileOn(int X, int Y)
    {
        if (X > -1 && X < 8 && Y > -1 && Y < 8 && TileStateCheck(X, Y, TileState.empty))
            TileStateSetting(X, Y, TileState.canGO);
        else if (X > -1 && X < 8 && Y > -1 && Y < 8 && TileStateCheck(X, Y, TileState.monster))
            TileStateSetting(X, Y, TileState.canFight);
        else if (X > -1 && X < 8 && Y > -1 && Y < 8 && TileStateCheck(X, Y, TileState.town))
            TileStateSetting(X, Y, TileState.canTownEnter);
        else if (X > -1 && X < 8 && Y > -1 && Y < 8 && TileStateCheck(X, Y, TileState.dungeon))
            TileStateSetting(X, Y, TileState.canDungeonEnter);
        else if (X > -1 && X < 8 && Y > -1 && Y < 8 && TileStateCheck(X, Y, TileState.chest))
            TileStateSetting(X, Y, TileState.canOpenChest);
    }

    protected override void TileOff(int X, int Y)
    {
        if (X > -1 && X < 8 && Y > -1 && Y < 8 && TileStateCheck(X, Y, TileState.canGO))
            TileStateSetting(X, Y, TileState.empty);
        else if (X > -1 && X < 8 && Y > -1 && Y < 8 && TileStateCheck(X, Y, TileState.canFight))
            TileStateSetting(X, Y, TileState.monster);
        else if (X > -1 && X < 8 && Y > -1 && Y < 8 && TileStateCheck(X, Y, TileState.canTownEnter))
            TileStateSetting(X, Y, TileState.town);
        else if (X > -1 && X < 8 && Y > -1 && Y < 8 && TileStateCheck(X, Y, TileState.canDungeonEnter))
            TileStateSetting(X, Y, TileState.dungeon);
        else if (X > -1 && X < 8 && Y > -1 && Y < 8 && TileStateCheck(X, Y, TileState.canOpenChest))
            TileStateSetting(X, Y, TileState.chest);
    }

    protected override bool FieldStateEmptyCheck(int X, int Y)
    {
        return X > -1 && X < 7 && Y > -1 && Y < 5 && field.tileRaw[Y].fieldTiles[X].tileState == TileState.empty;
    }

    protected void ClearDungeon()
    {
        dungeonClearUI.SetActive(true);
    }

    protected void RandomSpawnChest()
    {
        int randomY = UnityEngine.Random.Range(0, field.tileRaw.Length);
        int randomX = UnityEngine.Random.Range(0, field.tileRaw[randomY].fieldTiles.Length);
        int randomID = UnityEngine.Random.Range(0, 2);

        if (field.tileRaw[randomY].fieldTiles[randomX].tileState == TileState.empty)
        {
            chestPosition = field.tileRaw[randomY].fieldTiles[randomX];
            ChestSetting(randomID, randomX, randomY);
        }
        else
            RandomSpawnChest();
    }

    protected void LoadRandomMap(int[] index)
    {
        RandomTile(index[0], 0, 0);
        RandomTile(index[1], 1, 0);
        RandomTile(index[2], 0, 1);
        RandomTile(index[3], 1, 1);
    }

    protected void SaveRandomMap(int a,int b, int c, int d) 
    {
        playerManager.dungeonMap[0] = a;
        playerManager.dungeonMap[1] = b;
        playerManager.dungeonMap[2] = c;
        playerManager.dungeonMap[3] = d;
    }

    protected void CreateRandomMap()
    {
        int[] random = new int[4];

        for(int i = 0; i < random.Length; i++)
            random[i] = UnityEngine.Random.Range(0, 6);

        SaveRandomMap(random[0], random[1], random[2], random[3]);
        RandomTile(random[0], 0, 0);
        RandomTile(random[1], 1, 0);
        RandomTile(random[2], 0, 1);
        RandomTile(random[3], 1, 1);
    }

    protected void RandomTile(int index, int X, int Y)
    {
        switch (index) 
        {
            case 0:
                {
                    field.tileRaw[Y * 4].fieldTiles[X * 4].tileState = TileState.empty;
                    field.tileRaw[Y * 4].fieldTiles[X * 4 + 1].tileState = TileState.cantGo;
                    field.tileRaw[Y * 4].fieldTiles[X * 4 + 2].tileState = TileState.empty;
                    field.tileRaw[Y * 4].fieldTiles[X * 4 + 3].tileState = TileState.empty;

                    field.tileRaw[Y * 4 + 1].fieldTiles[X * 4].tileState = TileState.empty;
                    field.tileRaw[Y * 4 + 1].fieldTiles[X * 4 + 1].tileState = TileState.empty;
                    field.tileRaw[Y * 4 + 1].fieldTiles[X * 4 + 2].tileState = TileState.empty;
                    field.tileRaw[Y * 4 + 1].fieldTiles[X * 4 + 3].tileState = TileState.cantGo;

                    field.tileRaw[Y * 4 + 2].fieldTiles[X * 4].tileState = TileState.cantGo;
                    field.tileRaw[Y * 4 + 2].fieldTiles[X * 4 + 1].tileState = TileState.empty;
                    field.tileRaw[Y * 4 + 2].fieldTiles[X * 4 + 2].tileState = TileState.empty;
                    field.tileRaw[Y * 4 + 2].fieldTiles[X * 4 + 3].tileState = TileState.empty;

                    field.tileRaw[Y * 4 + 3].fieldTiles[X * 4].tileState = TileState.empty;
                    field.tileRaw[Y * 4 + 3].fieldTiles[X * 4 + 1].tileState = TileState.empty;
                    field.tileRaw[Y * 4 + 3].fieldTiles[X * 4 + 2].tileState = TileState.cantGo;
                    field.tileRaw[Y * 4 + 3].fieldTiles[X * 4 + 3].tileState = TileState.empty;
                    break;
                }
            case 1:
                {
                    field.tileRaw[Y * 4].fieldTiles[X * 4].tileState = TileState.empty;
                    field.tileRaw[Y * 4].fieldTiles[X * 4 + 1].tileState = TileState.cantGo;
                    field.tileRaw[Y * 4].fieldTiles[X * 4 + 2].tileState = TileState.empty;
                    field.tileRaw[Y * 4].fieldTiles[X * 4 + 3].tileState = TileState.cantGo;

                    field.tileRaw[Y * 4 + 1].fieldTiles[X * 4].tileState = TileState.empty;
                    field.tileRaw[Y * 4 + 1].fieldTiles[X * 4 + 1].tileState = TileState.cantGo;
                    field.tileRaw[Y * 4 + 1].fieldTiles[X * 4 + 2].tileState = TileState.empty;
                    field.tileRaw[Y * 4 + 1].fieldTiles[X * 4 + 3].tileState = TileState.empty;

                    field.tileRaw[Y * 4 + 2].fieldTiles[X * 4].tileState = TileState.empty;
                    field.tileRaw[Y * 4 + 2].fieldTiles[X * 4 + 1].tileState = TileState.empty;
                    field.tileRaw[Y * 4 + 2].fieldTiles[X * 4 + 2].tileState = TileState.empty;
                    field.tileRaw[Y * 4 + 2].fieldTiles[X * 4 + 3].tileState = TileState.empty;

                    field.tileRaw[Y * 4 + 3].fieldTiles[X * 4].tileState = TileState.empty;
                    field.tileRaw[Y * 4 + 3].fieldTiles[X * 4 + 1].tileState = TileState.cantGo;
                    field.tileRaw[Y * 4 + 3].fieldTiles[X * 4 + 2].tileState = TileState.empty;
                    field.tileRaw[Y * 4 + 3].fieldTiles[X * 4 + 3].tileState = TileState.cantGo;
                    break;
                }
            case 2:
                {
                    field.tileRaw[Y * 4].fieldTiles[X * 4].tileState = TileState.empty;
                    field.tileRaw[Y * 4].fieldTiles[X * 4 + 1].tileState = TileState.empty;
                    field.tileRaw[Y * 4].fieldTiles[X * 4 + 2].tileState = TileState.empty;
                    field.tileRaw[Y * 4].fieldTiles[X * 4 + 3].tileState = TileState.empty;

                    field.tileRaw[Y * 4 + 1].fieldTiles[X * 4].tileState = TileState.empty;
                    field.tileRaw[Y * 4 + 1].fieldTiles[X * 4 + 1].tileState = TileState.cantGo;
                    field.tileRaw[Y * 4 + 1].fieldTiles[X * 4 + 2].tileState = TileState.cantGo;
                    field.tileRaw[Y * 4 + 1].fieldTiles[X * 4 + 3].tileState = TileState.cantGo;

                    field.tileRaw[Y * 4 + 2].fieldTiles[X * 4].tileState = TileState.empty;
                    field.tileRaw[Y * 4 + 2].fieldTiles[X * 4 + 1].tileState = TileState.cantGo;
                    field.tileRaw[Y * 4 + 2].fieldTiles[X * 4 + 2].tileState = TileState.empty;
                    field.tileRaw[Y * 4 + 2].fieldTiles[X * 4 + 3].tileState = TileState.empty;

                    field.tileRaw[Y * 4 + 3].fieldTiles[X * 4].tileState = TileState.empty;
                    field.tileRaw[Y * 4 + 3].fieldTiles[X * 4 + 1].tileState = TileState.empty;
                    field.tileRaw[Y * 4 + 3].fieldTiles[X * 4 + 2].tileState = TileState.empty;
                    field.tileRaw[Y * 4 + 3].fieldTiles[X * 4 + 3].tileState = TileState.empty;
                    break;
                }
            case 3:
                {
                    field.tileRaw[Y * 4].fieldTiles[X * 4].tileState = TileState.empty;
                    field.tileRaw[Y * 4].fieldTiles[X * 4 + 1].tileState = TileState.empty;
                    field.tileRaw[Y * 4].fieldTiles[X * 4 + 2].tileState = TileState.cantGo;
                    field.tileRaw[Y * 4].fieldTiles[X * 4 + 3].tileState = TileState.cantGo;

                    field.tileRaw[Y * 4 + 1].fieldTiles[X * 4].tileState = TileState.empty;
                    field.tileRaw[Y * 4 + 1].fieldTiles[X * 4 + 1].tileState = TileState.empty;
                    field.tileRaw[Y * 4 + 1].fieldTiles[X * 4 + 2].tileState = TileState.empty;
                    field.tileRaw[Y * 4 + 1].fieldTiles[X * 4 + 3].tileState = TileState.empty;

                    field.tileRaw[Y * 4 + 2].fieldTiles[X * 4].tileState = TileState.empty;
                    field.tileRaw[Y * 4 + 2].fieldTiles[X * 4 + 1].tileState = TileState.cantGo;
                    field.tileRaw[Y * 4 + 2].fieldTiles[X * 4 + 2].tileState = TileState.empty;
                    field.tileRaw[Y * 4 + 2].fieldTiles[X * 4 + 3].tileState = TileState.empty;

                    field.tileRaw[Y * 4 + 3].fieldTiles[X * 4].tileState = TileState.cantGo;
                    field.tileRaw[Y * 4 + 3].fieldTiles[X * 4 + 1].tileState = TileState.empty;
                    field.tileRaw[Y * 4 + 3].fieldTiles[X * 4 + 2].tileState = TileState.empty;
                    field.tileRaw[Y * 4 + 3].fieldTiles[X * 4 + 3].tileState = TileState.cantGo;
                    break;
                }
            case 4:
                {
                    field.tileRaw[Y * 4].fieldTiles[X * 4].tileState = TileState.cantGo;
                    field.tileRaw[Y * 4].fieldTiles[X * 4 + 1].tileState = TileState.empty;
                    field.tileRaw[Y * 4].fieldTiles[X * 4 + 2].tileState = TileState.empty;
                    field.tileRaw[Y * 4].fieldTiles[X * 4 + 3].tileState = TileState.cantGo;

                    field.tileRaw[Y * 4 + 1].fieldTiles[X * 4].tileState = TileState.empty;
                    field.tileRaw[Y * 4 + 1].fieldTiles[X * 4 + 1].tileState = TileState.empty;
                    field.tileRaw[Y * 4 + 1].fieldTiles[X * 4 + 2].tileState = TileState.empty;
                    field.tileRaw[Y * 4 + 1].fieldTiles[X * 4 + 3].tileState = TileState.empty;

                    field.tileRaw[Y * 4 + 2].fieldTiles[X * 4].tileState = TileState.cantGo;
                    field.tileRaw[Y * 4 + 2].fieldTiles[X * 4 + 1].tileState = TileState.cantGo;
                    field.tileRaw[Y * 4 + 2].fieldTiles[X * 4 + 2].tileState = TileState.empty;
                    field.tileRaw[Y * 4 + 2].fieldTiles[X * 4 + 3].tileState = TileState.cantGo;

                    field.tileRaw[Y * 4 + 3].fieldTiles[X * 4].tileState = TileState.empty;
                    field.tileRaw[Y * 4 + 3].fieldTiles[X * 4 + 1].tileState = TileState.cantGo;
                    field.tileRaw[Y * 4 + 3].fieldTiles[X * 4 + 2].tileState = TileState.empty;
                    field.tileRaw[Y * 4 + 3].fieldTiles[X * 4 + 3].tileState = TileState.empty;
                    break;
                }
            case 5:
                {
                    field.tileRaw[Y * 4].fieldTiles[X * 4].tileState = TileState.empty;
                    field.tileRaw[Y * 4].fieldTiles[X * 4 + 1].tileState = TileState.empty;
                    field.tileRaw[Y * 4].fieldTiles[X * 4 + 2].tileState = TileState.empty;
                    field.tileRaw[Y * 4].fieldTiles[X * 4 + 3].tileState = TileState.empty;

                    field.tileRaw[Y * 4 + 1].fieldTiles[X * 4].tileState = TileState.empty;
                    field.tileRaw[Y * 4 + 1].fieldTiles[X * 4 + 1].tileState = TileState.cantGo;
                    field.tileRaw[Y * 4 + 1].fieldTiles[X * 4 + 2].tileState = TileState.cantGo;
                    field.tileRaw[Y * 4 + 1].fieldTiles[X * 4 + 3].tileState = TileState.empty;

                    field.tileRaw[Y * 4 + 2].fieldTiles[X * 4].tileState = TileState.empty;
                    field.tileRaw[Y * 4 + 2].fieldTiles[X * 4 + 1].tileState = TileState.cantGo;
                    field.tileRaw[Y * 4 + 2].fieldTiles[X * 4 + 2].tileState = TileState.cantGo;
                    field.tileRaw[Y * 4 + 2].fieldTiles[X * 4 + 3].tileState = TileState.empty;

                    field.tileRaw[Y * 4 + 3].fieldTiles[X * 4].tileState = TileState.empty;
                    field.tileRaw[Y * 4 + 3].fieldTiles[X * 4 + 1].tileState = TileState.empty;
                    field.tileRaw[Y * 4 + 3].fieldTiles[X * 4 + 2].tileState = TileState.empty;
                    field.tileRaw[Y * 4 + 3].fieldTiles[X * 4 + 3].tileState = TileState.empty;
                    break;
                }
        }
    } 
}
