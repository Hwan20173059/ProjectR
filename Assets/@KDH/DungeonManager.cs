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

        PlayerFieldSetting(0, 0);

        if (playerManager.isDungeon == false)
        {
            playerTurnIndex = 3;
            playerManager.monsterPosition = new List<int>();
            SpawnRandomMonster(3);
        }
        else
        {
            if (playerManager.monsterPosition.Count > 0)
            {
                playerTurnIndex = 3;
                LoadMonster();
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
        else if(playerManager.selectDungeonID == 1)
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
        if (X > -1 && X < 7 && Y > -1 && Y < 5 && TileStateCheck(X, Y, TileState.empty))
            TileStateSetting(X, Y, TileState.canGO);
        else if (X > -1 && X < 7 && Y > -1 && Y < 5 && TileStateCheck(X, Y, TileState.monster))
            TileStateSetting(X, Y, TileState.canFight);
        else if (X > -1 && X < 7 && Y > -1 && Y < 5 && TileStateCheck(X, Y, TileState.town))
            TileStateSetting(X, Y, TileState.canTownEnter);
        else if (X > -1 && X < 7 && Y > -1 && Y < 5 && TileStateCheck(X, Y, TileState.dungeon))
            TileStateSetting(X, Y, TileState.canDungeonEnter);
        else if (X > -1 && X < 7 && Y > -1 && Y < 5 && TileStateCheck(X, Y, TileState.chest))
            TileStateSetting(X, Y, TileState.canOpenChest);
    }

    protected override void TileOff(int X, int Y)
    {
        if (X > -1 && X < 7 && Y > -1 && Y < 5 && TileStateCheck(X, Y, TileState.canGO))
            TileStateSetting(X, Y, TileState.empty);
        else if (X > -1 && X < 7 && Y > -1 && Y < 5 && TileStateCheck(X, Y, TileState.canFight))
            TileStateSetting(X, Y, TileState.monster);
        else if (X > -1 && X < 7 && Y > -1 && Y < 5 && TileStateCheck(X, Y, TileState.canTownEnter))
            TileStateSetting(X, Y, TileState.town);
        else if (X > -1 && X < 7 && Y > -1 && Y < 5 && TileStateCheck(X, Y, TileState.canDungeonEnter))
            TileStateSetting(X, Y, TileState.dungeon);
        else if (X > -1 && X < 7 && Y > -1 && Y < 5 && TileStateCheck(X, Y, TileState.canOpenChest))
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
}
