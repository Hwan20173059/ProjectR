using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldManager : TileMapManager
{
    void Start()
    {
        playerManager = PlayerManager.Instance;

        PlayerFieldSetting(playerManager.fieldX, playerManager.fieldY);

        if (playerManager.isEnterTown == true)
        {
            playerTurnIndex = 3;
            playerManager.monsterPosition = new List<int>();
            SpawnRandomMonster(4);
        }
        else
        {
            playerTurnIndex = 3;
            LoadMonster();
        }

        PlayerTurn();
    }
}
