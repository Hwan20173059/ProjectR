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
            playerTurnIndex = 1;
            playerManager.monsterPosition = new List<int>();
            SpawnRandomMonster(4);
        }
        else
        {
            playerTurnIndex = 1;
            LoadMonster();
        }

        playerManager.isField = true;
        playerManager.isTown = false;
        playerManager.isDungeon = false;

        PlayerTurn();
    }
}

