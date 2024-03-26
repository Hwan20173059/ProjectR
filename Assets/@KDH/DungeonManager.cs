using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonManager : TileMapManager
{
    public void ExitButton()
    {
        if (playerManager.selectBattleID == 0)
        {
            playerManager.fieldX = 5;
            playerManager.fieldY = 1;
        }
        else if(playerManager.selectBattleID == 1)
        {
            playerManager.fieldX = 2;
            playerManager.fieldY = 7;
        }
        else if (playerManager.selectBattleID == 2)
        {
            playerManager.fieldX = 8;
            playerManager.fieldY = 7;
        }

        SceneManager.LoadScene("FieldScene");
    }
}
