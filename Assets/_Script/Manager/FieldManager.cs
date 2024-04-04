using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldManager : TileMapManager
{
    void Start()
    {
        playerManager = PlayerManager.Instance;

        playerPrefab.GetComponent<SpriteRenderer>().sprite = playerManager.characterList[playerManager.selectedCharacterIndex].sprite;

        if (playerManager.isField == false)
        {
            playerTurnIndex = playerManager.playerTurnIndex;
            playerManager.monsterPosition = new List<int>();
            SpawnRandomMonster(4);
            PlayerFieldSetting(playerManager.fieldX, playerManager.fieldY);

            PlayerTurn();
        }
        else
        {
            playerTurnIndex = playerManager.currentTurnIndex;
            LoadMonster();

            PlayerFieldSetting(playerManager.fieldX, playerManager.fieldY);

            if (playerTurnIndex > 0)
                PlayerTurn();
            else
                PlayerFieldSetting(playerManager.fieldX, playerManager.fieldY);
        }

        currentTile = field.tileRaw[playerManager.fieldY].fieldTiles[playerManager.fieldX];

        playerManager.isField = true;
        playerManager.isTown = false;
        playerManager.isDungeon = false;
    }
}

