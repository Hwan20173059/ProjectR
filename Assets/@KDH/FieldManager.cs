using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldManager : TileMapManager
{
    void Start()
    {
        playerManager = PlayerManager.Instance;

        PlayerFieldSetting(playerManager.fieldX, playerManager.fieldY);

        if (playerManager.isField == false)
        {
            playerTurnIndex = 3;
            playerManager.monsterPosition = new List<int>();
            SpawnRandomMonster(2);
        }
        else
        {
            playerTurnIndex = 3;
            LoadMonster();
        }

        playerManager.isField = true;
        playerManager.isTown = false;
        playerManager.isDungeon = false;

        PlayerTurn();
    }

    protected override void TileOn(int X, int Y)
    {
        if (X > -1 && X < 17 && Y > -1 && Y < 11 && TileStateCheck(X, Y, TileState.empty))
            TileStateSetting(X, Y, TileState.canGO);
        else if (X > -1 && X < 17 && Y > -1 && Y < 11 && TileStateCheck(X, Y, TileState.monster))
            TileStateSetting(X, Y, TileState.canFight);
        else if (X > -1 && X < 17 && Y > -1 && Y < 11 && TileStateCheck(X, Y, TileState.town))
            TileStateSetting(X, Y, TileState.canTownEnter);
        else if (X > -1 && X < 17 && Y > -1 && Y < 11 && TileStateCheck(X, Y, TileState.dungeon))
            TileStateSetting(X, Y, TileState.canDungeonEnter);
        else if (X > -1 && X < 17 && Y > -1 && Y < 11 && TileStateCheck(X, Y, TileState.chest))
            TileStateSetting(X, Y, TileState.canOpenChest);
    }

    protected override void TileOff(int X, int Y)
    {
        if (X > -1 && X < 17 && Y > -1 && Y < 11 && TileStateCheck(X, Y, TileState.canGO))
            TileStateSetting(X, Y, TileState.empty);
        else if (X > -1 && X < 17 && Y > -1 && Y < 11 && TileStateCheck(X, Y, TileState.canFight))
            TileStateSetting(X, Y, TileState.monster);
        else if (X > -1 && X < 17 && Y > -1 && Y < 11 && TileStateCheck(X, Y, TileState.canTownEnter))
            TileStateSetting(X, Y, TileState.town);
        else if (X > -1 && X < 17 && Y > -1 && Y < 11 && TileStateCheck(X, Y, TileState.canDungeonEnter))
            TileStateSetting(X, Y, TileState.dungeon);
        else if (X > -1 && X < 17 && Y > -1 && Y < 11 && TileStateCheck(X, Y, TileState.canOpenChest))
            TileStateSetting(X, Y, TileState.chest);
    }

    protected override bool FieldStateEmptyCheck(int X, int Y)
    {
        return X > -1 && X < 17 && Y > -1 && Y < 11 && field.tileRaw[Y].fieldTiles[X].tileState == TileState.empty;
    }
}
