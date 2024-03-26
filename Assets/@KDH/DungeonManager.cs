using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonManager : TileMapManager
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
}
