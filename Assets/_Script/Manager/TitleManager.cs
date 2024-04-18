using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public Character character;

    void Start()
    {
        DataManager dataManager = DataManager.Instance;
        PlayerManager playerManager = PlayerManager.Instance;
        ItemManager itemManager = ItemManager.Instance;

        playerManager.titleManager = this;
        playerManager.currentState = CurrentState.title;
    }

    public void GameStart()
    {
        PlayerManager playerManager = PlayerManager.Instance;

        AudioManager.Instance.PlayUISelectSFX();

        if (playerManager.selectTownID == 0)
            playerManager.currentState = CurrentState.town1;
        else if(playerManager.selectTownID == 1)
            playerManager.currentState = CurrentState.town2;

        AudioManager.Instance.SetState();
        SceneManager.LoadScene("TownScene");
    }
}
