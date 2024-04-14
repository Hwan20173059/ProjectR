using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldManager : TileMapManager
{
    public FieldTutorialManager fieldTutorialManager;

    void Start()
    {
        playerManager = PlayerManager.Instance;

        playerPrefab.GetComponent<SpriteRenderer>().sprite = playerManager.characterList[playerManager.selectedCharacterIndex].sprite;

        if (playerManager.firstField == true)
        {
            fieldTutorialManager.gameObject.SetActive(true);
            fieldTutorialManager.ActiveTutorial();
            playerManager.firstField = false;
        }

        if (playerManager.isField == false)
        {
            playerTurnIndex = playerManager.playerTurnIndex;
            playerManager.monsterPosition = new List<int>();

            SpawnRandomMonster(3, 0, 10, 0, 5, 0);
            SpawnRandomMonster(2, 10, 17, 0, 5, 3);
            SpawnRandomMonster(2, 23, 35, 0, 2, 2);
            SpawnRandomMonster(2, 23, 35, 3, 5, 1);

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
            {
                PlayerFieldSetting(playerManager.fieldX, playerManager.fieldY);
                AEnemyTurn();
            }
            
        }

        currentTile = field.tileRaw[playerManager.fieldY].fieldTiles[playerManager.fieldX];
        CharacterUIRefresh();

        playerManager.isField = true;
        playerManager.isTown = false;
        playerManager.isDungeon = false;
    }
}

